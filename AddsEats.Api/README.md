# AddsEats API 🇪🇹🍽️

**AddsEats API** is a RESTful backend service built with **ASP.NET Core (.NET 10)** for an Ethiopian food delivery and restaurant discovery platform. It provides endpoints for discovering restaurants, browsing menus with VAT calculation, and placing orders with domain validations and robust error handling.

---

## 🚀 Key Features

* **Restaurant Discovery & Filtering**:
  * Paginated restaurant catalog with configurable `Page` and `PageSize`.
  * Address/location filtering (e.g., Bole, Ayat, Mexico, Megenagna).
  * Filter active vs. inactive restaurants.
  * Direct retrieval of restaurant details and their specific menus.
* **Menu Management**:
  * Explore authentic Ethiopian cuisine and popular dishes.
  * Automatic VAT calculation (`PriceWithVat = Price * 1.15m`).
  * Nested resource routing (`GET /api/restaurant/{id}/menu`).
* **Order Processing & Business Validation**:
  * Server-side pricing computation (`TotalAmount = Price * Quantity`) preventing client-side price tampering.
  * Restaurant status verification (rejects orders for inactive restaurants).
  * Restaurant menu item ownership check (prevents ordering items from the wrong restaurant).
  * Thread-safe in-memory order tracking.
* **Custom Ethiopian Validations**:
  * `[EthiopianMobile]`: Validates local phone number formats (`+2519...`, `+2517...`, `09...`, `07...`).
  * `[ETBAmount]`: Validates positive currency amount constraints.
* **Enterprise Error Handling & Architecture**:
  * **Result Pattern (`Result<T>`)**: Railway-oriented error handling returning clean `Error` models and appropriate HTTP status codes (`400`, `404`, `500`) without throwing unhandled exceptions.
  * **DTO & Mapper Separation**: Shields sensitive internal entity fields (like `OwnerAddress`) via `RestaurantDTO` and extension method mappers (`ToRestaurantDTO()`).
  * **Dependency Injection**: Scoped service registration across all domain modules.

---

## 🛠️ Tech Stack

* **Framework:** ASP.NET Core Web API (.NET 10.0)
* **Language:** C# 12 / 13
* **API Documentation:** Swagger / OpenAPI via Swashbuckle
* **Data Storage:** In-Memory Seeded Repository (`SeedData.cs`)

---

## 📁 Project Architecture

```text
AddsEats.Api/
├── Controllers/              # API Controllers handling HTTP requests
│   ├── RestaurantController.cs # Restaurant listing, filtering, pagination, and menu
│   ├── MenuItemController.cs   # Menu item lookups
│   └── OrderController.cs      # Order submission and status retrieval
├── Services/                 # Business logic layer (Dependency Injection)
│   ├── RestaurantServices/   # IRestaurantServices & RestaurantServices
│   ├── MenuItemServices/     # IMenuItemServices & MenuItemServices
│   └── OrderServices/        # IOrderServices & OrderServices
├── Models/                   # Domain entities
│   ├── Restaurant.cs         # Restaurant entity
│   ├── MenuItem.cs           # Menu item entity (with computed VAT)
│   └── Order.cs              # Order entity
├── DTOs/                     # Data Transfer Objects (Request / Response contracts)
│   ├── RestaurantDTO.cs      # Public restaurant representation
│   ├── RestaurantQuery.cs    # Pagination & filter query parameters
│   ├── PaginatedResult.cs    # Generic pagination envelope (Items, Page, TotalPages, TotalCount)
│   └── CreateOrderDTO.cs     # Input model with Data Annotations validation
├── Middleware/               # Custom pipeline middleware
│   ├── CorrelationIdMiddleware.cs        # X-Correlation-Id generation & TraceIdentifier setup
│   ├── RequestTimingMiddleware.cs        # Execution timing & slow request warning logger
│   ├── ApiKeyMiddleware.cs               # Admin API key authentication & short-circuiting
│   └── ScopedServiceExperimentMiddleware.cs # Concurrency & scoped service resolution demo
├── Filters/                  # MVC action & result filters
│   └── AuditActionFilter.cs  # Action name & bound arguments audit logger
├── Mappers/                  # Extension mappers
│   └── RestaurantMapper.cs   # Maps Restaurant -> RestaurantDTO
├── Common/                   # Cross-cutting concerns & patterns
│   └── Result.cs             # Result<T> and Error contracts
├── Validation/               # Custom validation attributes
│   ├── EthiopianMobileAttribute.cs # Ethiopian phone regex validator
│   └── ETBAmountAttribute.cs       # ETB currency validator
├── Data/                     # In-memory storage & seed data
│   └── SeedData.cs           # Seed restaurants and menu items
├── Program.cs                # Application entry point, middleware pipeline & DI registration
├── MIDDLEWARE_EXERCISES.md   # Complete documentation & experiments for the 8 exercises
├── AddsEats.Api.http         # Ready-to-run HTTP test requests
└── appsettings.json          # Application configuration
```

---

## 🧱 Middleware Pipeline Architecture & Line-by-Line Breakdown

The ASP.NET Core pipeline in [`Program.cs`](file:///home/bonsa/Documents/dotnet-backend/AddsEats.Api/Program.cs) processes requests sequentially as they travel down to the endpoint and back up through the response chain. The exact order is deliberate and critical:

```text
Incoming Request
      │
      ▼
1.  app.UseCorrelationId()            ──► Extracts/generates X-Correlation-Id & sets TraceIdentifier
      │
      ▼
2.  app.UseRequestTiming()            ──► Starts Stopwatch; logs method, path, status, and duration
      │
      ▼
3.  app.UseScopedServiceExperiment()  ──► Demonstrates per-request scoped service resolution (InvokeAsync)
      │
      ▼
4.  app.UseSwagger() & UseSwaggerUI() ──► Serves OpenAPI JSON and interactive UI in Development
      │
      ▼
5.  app.UseHttpsRedirection()         ──► Redirects HTTP to HTTPS before reaching auth or business logic
      │
      ▼
6.  app.Map("/admin", adminApp => ...)──► Branches pipeline; ApiKeyMiddleware protects admin branch
      │
      ▼
7.  app.UseAuthorization()            ──► Evaluates user identity and endpoint authorization policies
      │
      ▼
8.  app.MapControllers()              ──► Executes matched controller action & Action Filters (AuditFilter)
      │
      ▼
Outgoing Response (traverses back up through OnStarting headers & timing calculations)
```

### Detailed Line-by-Line Pipeline Analysis

| Pipeline Code Line | Position & Function | Reason for this Position |
| :--- | :--- | :--- |
| `1. app.UseCorrelationId();` | **Top of pipeline (1st)** | Must execute before any logging or downstream middleware so that `context.TraceIdentifier` is populated early. Sets response header via `OnStarting()`. |
| `2. app.UseRequestTiming();` | **Directly after Correlation ID (2nd)** | Measures total request duration including all downstream execution, routing, and filters. Because it runs after `UseCorrelationId()`, its logs automatically include `TraceIdentifier`. |
| `3. app.UseScopedServiceExperiment();` | **Diagnostic/Utility (3rd)** | Demonstrates safe per-request scoped service resolution via `InvokeAsync` rather than constructor injection, preventing captive dependencies under concurrency. |
| `4. app.UseSwagger(); & app.UseSwaggerUI();` | **Development Tools (4th)** | Exposes API schema and interactive testing UI early in development without needing database or auth processing for raw doc assets. |
| `5. app.UseHttpsRedirection();` | **Security / Transport (5th)** | Ensures all communication is encrypted over HTTPS before headers, API keys, or credentials pass into sensitive branches or controllers. |
| `6. app.Map("/admin", adminApp => ...);` | **Branching Pipeline (6th)** | Creates a branched sub-pipeline specifically for the `/admin` path where `ApiKeyMiddleware` verifies the `X-Api-Key` header and short-circuits unauthorized calls without polluting public routes. |
| `7. app.UseAuthorization();` | **Security Policy (7th)** | Checks whether the caller has permission to access the target resource. *(Must always be placed after `UseAuthentication()` when authentication handlers are configured).* |
| `8. app.MapControllers();` | **Endpoint Execution (8th)** | Matches routes to Controller actions, executes Model Binding, and triggers Action Filters such as `AuditActionFilter`. |
| `9. app.Run();` | **Pipeline Terminator** | Begins listening for incoming connections and executes the compiled pipeline. |

For detailed experimental observations (including concurrency bug replication and ordering gotchas), see **[MIDDLEWARE_EXERCISES.md](file:///home/bonsa/Documents/dotnet-backend/AddsEats.Api/MIDDLEWARE_EXERCISES.md)**.

---

## 📡 API Endpoints

### 1. Restaurants (`/api/restaurant`)

| Method | Endpoint | Description | Query Parameters / Body |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/restaurant` | List restaurants (Paginated) | `address` (string), `page` (int, default 1), `pageSize` (int, default 10) |
| `GET` | `/api/restaurant/{id}` | Get restaurant by ID | `{id}` (int) |
| `GET` | `/api/restaurant/{id}/menu`| Get menu items for specific restaurant | `{id}` (int) |
| `GET` | `/api/restaurant/active` | Get all currently active restaurants | None |

#### Example Response: `GET /api/restaurant?page=1&pageSize=2`
```json
{
  "items": [
    {
      "id": 8,
      "name": "Abay Restaurant",
      "address": "Megenagna",
      "phone": "555-0123",
      "isActive": true
    },
    {
      "id": 1,
      "name": "Addis Ababa Restaurant",
      "address": "Bole",
      "phone": "555-1234",
      "isActive": true
    }
  ],
  "page": 1,
  "currentPage": 1,
  "pageSize": 2,
  "totalCount": 12,
  "totalPages": 6
}
```

---

### 2. Menu Items (`/api/v1/menuitem`)

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/v1/menuitem` | Get all menu items across restaurants |
| `GET` | `/api/v1/menuitem/{id}` | Get single menu item by ID |

---

### 3. Orders (`/api/order`)

| Method | Endpoint | Description | Status Codes |
| :--- | :--- | :--- | :--- |
| `POST` | `/api/order` | Place a new order | `201 Created`, `400 Bad Request`, `404 Not Found` |
| `GET` | `/api/order/{id}` | Retrieve order details by ID | `200 OK`, `404 Not Found` |

#### Example Request: `POST /api/order`
```json
{
  "restaurantId": 1,
  "menuItemId": 1,
  "customerName": "Abebe Bikila",
  "customerPhone": "+251911223344",
  "deliveryAddress": "Bole, Addis Ababa",
  "quantity": 2
}
```

#### Example Success Response (`201 Created`):
```json
{
  "id": 1,
  "restaurantId": 1,
  "menuItemId": 1,
  "customerName": "Abebe Bikila",
  "customerPhone": "+251911223344",
  "deliveryAddress": "Bole, Addis Ababa",
  "quantity": 2,
  "totalAmount": 500.0,
  "orderDate": "2026-11-24T08:30:00Z"
}
```

#### Example Error Response via `Result<T>` (`400 Bad Request`):
```json
{
  "code": "InvalidMenuItem",
  "message": "Menu item 'Fish Dullet' (ID 4) does not belong to restaurant 'Addis Ababa Restaurant' (ID 1).",
  "statusCode": 400
}
```

---

### 4. Authentication (`/api/auth`)

| Method | Endpoint | Description | Status Codes |
| :--- | :--- | :--- | :--- |
| `POST` | `/api/auth/login` | Log in a demo user and receive a JWT | `200 OK`, `400 Bad Request` |

#### Example Request: `POST /api/auth/login`
```json
{
  "phoneNumber": "0911111111",
  "password": "123456"
}
```

#### Example Success Response (`200 OK`)
```json
{
  "accessToken": "<jwt>",
  "tokenType": "Bearer",
  "expiresAtUtc": "2026-11-26T11:02:19Z"
}
```

Use the token in Swagger UI with the **Authorize** button, or send it as `Authorization: Bearer <accessToken>`.

> Swagger descriptions come from XML doc comments on controllers, DTOs, and models. The JWT signing key is configured via `Jwt:SigningKey` in `appsettings.json`; override it with user-secrets or environment variables outside development.

---

## 🏃 Running the Application

### Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/download) installed on your machine.

### Start the Server
Navigate to the project root and run:

```bash
# Restore dependencies and run the API
dotnet run
```

Or run with live reload during development:

```bash
dotnet watch
```

The application will start on:
* **HTTP:** `http://localhost:5187`
* **Swagger UI:** `http://localhost:5187/swagger`

---

## 🧪 Testing the API

### 1. Swagger UI
Open your browser and navigate to:
```
http://localhost:5187/swagger
```
Explore the interactive documentation and execute requests directly from the UI.

### 2. VS Code / JetBrains HTTP Client
Open `AddsEats.Api.http` in your editor to execute pre-configured test requests covering:
* Successful restaurant pagination and filtering.
* Fetching restaurant menus.
* Valid order creation.
* Validation failure triggers (empty payload, invalid phone, non-existent IDs, cross-restaurant mismatches).
