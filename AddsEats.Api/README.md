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
├── Mappers/                  # Extension mappers
│   └── RestaurantMapper.cs   # Maps Restaurant -> RestaurantDTO
├── Common/                   # Cross-cutting concerns & patterns
│   └── Result.cs             # Result<T> and Error contracts
├── Validation/               # Custom validation attributes
│   ├── EthiopianMobileAttribute.cs # Ethiopian phone regex validator
│   └── ETBAmountAttribute.cs       # ETB currency validator
├── Data/                     # In-memory storage & seed data
│   └── SeedData.cs           # Seed restaurants and menu items
├── Program.cs                # Application entry point, middleware & DI registration
├── AddsEats.Api.http         # Ready-to-run HTTP test requests
└── appsettings.json          # Application configuration
```

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
