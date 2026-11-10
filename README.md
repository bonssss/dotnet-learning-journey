# Addis Eats — .NET Learning Journey

Welcome to the **Addis Eats** .NET backend learning repository. This project contains practical C# and .NET exercises exploring core programming concepts, data modeling, type systems, and backend logic designed around an Ethiopian food delivery domain (Addis Eats).

---

## 📋 Table of Contents

- [Overview](#overview)
- [Prerequisites & Environment](#prerequisites--environment)
- [Project Structure](#project-structure)
- [Exercises & Modules](#exercises--modules)
- [Getting Started](#getting-started)
- [Running the Exercises](#running-the-exercises)

---

## 🎯 Overview

- **Track**: CodeOps Module 4 — .NET Track
- **Language**: C# (.NET 10 / Cross-platform)
- **Domain**: Addis Eats (food ordering and delivery management)

---

## 💻 Prerequisites & Environment

- **.NET SDK**: .NET 10.0 (or .NET 8+)
- Check your local .NET installation:
  ```bash
  dotnet --info
  ```

---

## 📂 Project Structure

```text
dotnet-backend/
├── AddisEats.Exercises/
│   ├── Models/
│   │   └── MenuItem.cs          # Domain model with VAT calculation
│   ├── Exercise2.cs             # Author and module intro
│   ├── Exercise3.cs             # Primitive types and domain representation
│   ├── Exercise4.cs             # Receipt formatting & currency calculations
│   ├── Exercise5.cs             # Integer vs. decimal division
│   ├── Exercise6.cs             # Value vs. reference semantics
│   ├── Exercise7.cs             # Safe null handling & nullable reference types
│   ├── Exercise8.cs             # Safe parsing with TryParse and CultureInfo
│   ├── Method.cs                # Methods and method overloading
│   ├── Program.cs               # Main entry point
│   └── AddisEats.Exercises.csproj
├── environment.txt              # Environment diagnostics & SDK versioning
├── .gitignore
└── README.md
```

---

## 📚 Exercises & Modules

| File | Topic | Description |
|---|---|---|
| **`Exercise2.cs`** | Developer Profile | Basic console output with date formatting and metadata. |
| **`Exercise3.cs`** | Primitive Types | Working with `string`, `decimal`, `int`, and `bool` in domain modeling. |
| **`Exercise4.cs`** | Financial Formatting | Receipt calculation with subtotals, delivery fees, and formatting. |
| **`Exercise5.cs`** | Division Semantics | Differences between integer truncation and decimal precision. |
| **`Exercise6.cs`** | Memory & Semantics | Value types (stack/copy) vs. Reference types (heap/reference). |
| **`Exercise7.cs`** | Null Safety | Safe null navigation (`?.`), ternary checks, and null-coalescing (`??`). |
| **`Exercise8.cs`** | Input Parsing | Defensive input parsing with `decimal.TryParse` and `CultureInfo`. |
| **`Method.cs`** | Method Overloading | Basic method signatures and polymorphic method overloading. |
| **`Models/MenuItem.cs`** | Object-Oriented Domain | `MenuItem` entity modeling with auto-properties and VAT methods. |

---

## 🚀 Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/bonssss/dotnet-learning-journey.git
cd dotnet-backend
```

### 2. Build the project
```bash
dotnet build
```

---

## ▶️ Running the Exercises

To run the application entry point:
```bash
dotnet run --project AddisEats.Exercises
```
