using AddisEats.Exercises;

if (args.Length > 0 && int.TryParse(args[0], out int exerciseNumber))
{
    switch (exerciseNumber)
    {
        case 2: Exercise2.Run(); break;
        case 3: Exercise3.Run(); break;
        case 4: Exercise4.Run(); break;
        case 5: Exercise5.Run(); break;
        case 6: Exercise6.Run(); break;
        case 7: Exercise7.Run(); break;
        case 8: Exercise8.Run(); break;
        default:
            Console.WriteLine($"Unknown exercise number: {exerciseNumber}. Valid numbers are 2 to 8.");
            break;
    }
    return;
}

Console.WriteLine("========================================");
Console.WriteLine("--- Exercise 2 ---");
Exercise2.Run();

Console.WriteLine();
Console.WriteLine("========================================");
Console.WriteLine("--- Exercise 3 ---");
Exercise3.Run();

Console.WriteLine();
Console.WriteLine("========================================");
Console.WriteLine("--- Exercise 4 ---");
Exercise4.Run();

Console.WriteLine();
Console.WriteLine("========================================");
Console.WriteLine("--- Exercise 5 ---");
Exercise5.Run();

Console.WriteLine();
Console.WriteLine("========================================");
Console.WriteLine("--- Exercise 6 ---");
Exercise6.Run();

Console.WriteLine();
Console.WriteLine("========================================");
Console.WriteLine("--- Exercise 7 ---");
Exercise7.Run();

Console.WriteLine();
Console.WriteLine("========================================");
Console.WriteLine("--- Exercise 8 ---");
Exercise8.Run();
Console.WriteLine("========================================");
