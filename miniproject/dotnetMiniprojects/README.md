# AddisEats.TopUp — TeleBirr Top-Up Calculator

A C# console application that processes **five TeleBirr top-ups** in one run and prints a per-customer summary with a grand total.

## Features

- Captures 5 customers with: **name**, **network** (`Ethio Telecom` or `Safaricom`), and **amount (ETB)**.
- Stores entries in arrays (names, networks, amounts, bonuses, totals).
- Validates amount input:
  - rejects non-numeric values
  - rejects zero or negative amounts
  - rejects amounts greater than **5,000.00 ETB**
  - re-prompts until a valid amount is provided (no crashes on invalid input)
- Applies bonus using a **`switch` expression**:
  - **5%** for Ethio Telecom
  - **3%** for Safaricom
  - **0%** otherwise
- Adds an extra **20.00 ETB** bonus for any top-up of **1,000.00 ETB or more**.
- Prints aligned columns per customer and a **GRAND TOTAL** row.
- Uses `decimal` with `m` suffix for all money values (no `double`).

## Project structure

- `AddisEats.TopUp.csproj` — .NET console project
- `Program.cs` — application logic and input/output

## Requirements

- .NET SDK (project targets `net10.0`)

## Run

From this directory:

```bash
dotnet run --project AddisEats.TopUp.csproj
```

## Example flow

1. Enter 5 customer records when prompted.
2. For each record, provide a valid name, network, and amount.
3. View the formatted table with amount, bonus, total per customer, and grand total.
