using System.Globalization;

const int TopUpCount = 5;
const decimal MaxAmount = 5000m;
const decimal ExtraThreshold = 1000m;
const decimal ExtraBonus = 20m;

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

var names = new string[TopUpCount];
var networks = new string[TopUpCount];
var amounts = new decimal[TopUpCount];
var bonuses = new decimal[TopUpCount];
var totals = new decimal[TopUpCount];

Console.WriteLine("TeleBirr Top-Up Calculator");
Console.WriteLine("==========================");
Console.WriteLine($"Enter details for {TopUpCount} top-ups.");

for (int i = 0; i < TopUpCount; i++)
{
    Console.WriteLine();
    Console.WriteLine($"Customer {i + 1}:");

    names[i] = ReadNonEmpty("  Name: ");
    networks[i] = ReadNetwork("  Network (Ethio Telecom/Safaricom): ");
    amounts[i] = ReadAmount("  Amount (ETB): ");

    decimal bonusRate = NormalizeNetwork(networks[i]) switch
    {
        "ETHIO TELECOM" => 0.05m,
        "SAFARICOM" => 0.03m,
        _ => 0.00m
    };

    decimal bonusAmount = amounts[i] * bonusRate;
    decimal extraAmount = amounts[i] >= ExtraThreshold ? ExtraBonus : 0m;

    bonuses[i] = bonusAmount + extraAmount;
    totals[i] = amounts[i] + bonuses[i];
}

Console.WriteLine();
Console.WriteLine(new string('-', 86));
Console.WriteLine($"{"Name",-18} {"Network",-16} {"Amount (ETB)",14} {"Bonus (ETB)",14} {"Total (ETB)",14}");
Console.WriteLine(new string('-', 86));

decimal grandTotal = 0m;
for (int i = 0; i < TopUpCount; i++)
{
    grandTotal += totals[i];
    Console.WriteLine(
        $"{names[i],-18} {networks[i],-16} {amounts[i],14:N2} {bonuses[i],14:N2} {totals[i],14:N2}");
}

Console.WriteLine(new string('-', 86));
Console.WriteLine($"{"GRAND TOTAL",-35} {"",14} {"",14} {grandTotal,14:N2}");

static string ReadNonEmpty(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
        {
            return input.Trim();
        }

        Console.WriteLine("  Please enter a non-empty value.");
    }
}

static string ReadNetwork(string prompt)
{
    while (true)
    {
        string input = ReadNonEmpty(prompt);
        string normalized = NormalizeNetwork(input);

        if (normalized == "ETHIO TELECOM")
        {
            return "Ethio Telecom";
        }

        if (normalized == "SAFARICOM")
        {
            return "Safaricom";
        }

        Console.WriteLine("  Please enter 'Ethio Telecom' or 'Safaricom'.");
    }
}

static decimal ReadAmount(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();

        if (!decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount) &&
            !decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out amount))
        {
            Console.WriteLine("  Invalid amount. Enter a numeric value (e.g. 100 or 100.50).");
            continue;
        }

        if (amount <= 0m)
        {
            Console.WriteLine("  Amount must be greater than 0 ETB.");
            continue;
        }

        if (amount > MaxAmount)
        {
            Console.WriteLine($"  Amount must not exceed {MaxAmount:N2} ETB.");
            continue;
        }

        return amount;
    }
}

static string NormalizeNetwork(string network)
{
    return string.Join(' ', network.Trim().ToUpperInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries));
}
