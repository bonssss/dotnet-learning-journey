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
            Exercise3.Run();
            Console.WriteLine();
            Exercise4.Run();
            Console.WriteLine();
            if (exerciseNumber is null)
            {
                Console.WriteLine("(Day 3 exercises 5-8 not yet added)");
            }
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
                Exercise3.Run();
                break;
            case 4:
                Exercise4.Run();
                break;
            default:
                Console.WriteLine($"Unknown Day 3 exercise number: {exerciseNumber}. Available: 1-4");
                break;
        }
    }
}
