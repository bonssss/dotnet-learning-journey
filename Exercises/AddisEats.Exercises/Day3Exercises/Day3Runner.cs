namespace AddisEats.Exercises.Day3Exercises;

public static class Day3Runner
{
    public static void Run(int? exerciseNumber = null)
    {
        void RunAll()
        {
            Exercise1.Run();
            Console.WriteLine();
            Exercise2.Run();
            Console.WriteLine();
            AddisEats.Catalog.Exercise3.Run();
            Console.WriteLine();
            AddisEats.Catalog.Exercise4.Run();
            Console.WriteLine();
            AddisEats.Catalog.Exercise5.Run();
            Console.WriteLine();
            AddisEats.Catalog.Exercise6.Run();
            Console.WriteLine();
            AddisEats.Catalog.Exercise7.Run();
            Console.WriteLine();
            Console.WriteLine("Exercise 8: exercises 3-7 organized into namespace AddisEats.Catalog, one type per file. Build and run confirmed.");
        }

        if (exerciseNumber is null)
        {
            RunAll();
            return;
        }

        switch (exerciseNumber)
        {
            case 1:
                Exercise1.Run();
                break;
            case 2:
                Exercise2.Run();
                break;
            case 3:
                AddisEats.Catalog.Exercise3.Run();
                break;
            case 4:
                AddisEats.Catalog.Exercise4.Run();
                break;
            case 5:
                AddisEats.Catalog.Exercise5.Run();
                break;
            case 6:
                AddisEats.Catalog.Exercise6.Run();
                break;
            case 7:
                AddisEats.Catalog.Exercise7.Run();
                break;
            case 8:
                RunAll();
                break;
            default:
                Console.WriteLine($"Unknown Day 3 exercise number: {exerciseNumber}. Available: 1-8");
                break;
        }
    }
}
