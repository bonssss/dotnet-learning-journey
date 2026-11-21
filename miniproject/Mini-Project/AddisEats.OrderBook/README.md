# AddisEats.OrderBook

A C# console mini-project that models an Addis Eats order book: menu items, order lines, orders, and reporting queries.

## What it includes

- `MenuItem` (record): `Id`, `Name`, `Category`, `Price` (ETB, `decimal`)
- `OrderLine` (record): pairs a `MenuItem` with a positive `Quantity`; computes line total
- `Order` (class): holds order lines, customer, area, and computed `Total`
- `OrderBook` (class): stores orders and provides LINQ-based queries:
  - orders for an area by total descending
  - total ETB revenue
  - busiest category (safe on empty order book)
  - page N at five per page (stable ordering, no repeated rows across pages)

## Rules satisfied

- All money values use `decimal` with the `m` suffix
- No `foreach` used for querying (LINQ only in query methods)
- `ArgumentException` thrown for non-positive quantity or price
- Each type lives in its own file inside the `AddisEats.OrderBook` namespace

## Run

From this directory:

```bash
dotnet run --project AddisEats.OrderBook.csproj
```

The program prints a worked start (one hard-coded order end-to-end with `MenuItem` and `OrderLine`) followed by `OrderBook` query demos.
