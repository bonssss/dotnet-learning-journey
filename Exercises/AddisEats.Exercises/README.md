# AddisEats.Exercises

This project contains C# and .NET exercises exploring core programming concepts, data modeling, type systems, and backend logic designed around the Addis Eats food delivery domain.

## Project Structure

* **`Models/MenuItem.cs`**: Domain entity representing menu items, prices, and VAT calculations (`PriceWithVat()`).
* **`Exercise2.cs`**: Author metadata and project introduction.
* **`Exercise3.cs`**: C# primitive types (`string`, `decimal`, `int`, `bool`).
* **`Exercise4.cs`**: Receipt formatting, subtotals, and delivery fee calculation.
* **`Exercise5.cs`**: Comparison of integer division truncation vs. decimal precision.
* **`Exercise6.cs`**: Value type (stack copy) vs. Reference type (heap reference) semantics.
* **`Exercise7.cs`**: Safe null handling with null-conditional (`?.`) and null-coalescing (`??`) operators.
* **`Exercise8.cs`**: Defensive parsing with `decimal.TryParse` and `CultureInfo`.
* **`Method.cs`**: Methods and method overloading.
* **`Program.cs`**: Entry point executing exercises and testing domain models.

## How to Run

From the root directory:
```bash
dotnet run --project AddisEats.Exercises
```

Or from inside this folder:
```bash
dotnet run
```

