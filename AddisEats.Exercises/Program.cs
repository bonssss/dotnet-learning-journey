Console.WriteLine("Hello World");

// using AddisEats.Exercises;

// if (args.Length > 0 && (args[0].Equals("Method.cs", StringComparison.OrdinalIgnoreCase) || args[0].Equals("Method", StringComparison.OrdinalIgnoreCase)))
// {
//     Method.Run();
//     return;
// }

// if (args.Length > 0 && int.TryParse(args[0], out int exerciseNumber))
// {
//     switch (exerciseNumber)
//     {
//         case 2: Exercise2.Run(); break;
//         case 3: Exercise3.Run(); break;
//         case 4: Exercise4.Run(); break;
//         case 5: Exercise5.Run(); break;
//         case 6: Exercise6.Run(); break;
//         case 7: Exercise7.Run(); break;
//         case 8: Exercise8.Run(); break;
//         case 9: Method.Run(); break;
//         default:
//             Console.WriteLine($"Unknown exercise number: {exerciseNumber}. Valid numbers are 2 to 9.");
//             break;
//     }
//     return;
// }

// Console.WriteLine("========================================");
// Console.WriteLine("--- Exercise 2 ---");
// Exercise2.Run();

// Console.WriteLine();
// Console.WriteLine("========================================");
// Console.WriteLine("--- Exercise 3 ---");
// Exercise3.Run();

// Console.WriteLine();
// Console.WriteLine("========================================");
// Console.WriteLine("--- Exercise 4 ---");
// Exercise4.Run();

// Console.WriteLine();
// Console.WriteLine("========================================");
// Console.WriteLine("--- Exercise 5 ---");
// Exercise5.Run();

// Console.WriteLine();
// Console.WriteLine("========================================");
// Console.WriteLine("--- Exercise 6 ---");
// Exercise6.Run();

// Console.WriteLine();
// Console.WriteLine("========================================");
// Console.WriteLine("--- Exercise 7 ---");
// Exercise7.Run();

// Console.WriteLine();
// Console.WriteLine("========================================");
// Console.WriteLine("--- Exercise 8 ---");
// Exercise8.Run();

// Console.WriteLine();
// Console.WriteLine("========================================");
// Console.WriteLine("--- Method ---");
// Method.Run();
// Console.WriteLine("========================================");
// Method.Run();

using AddisEats.Exercises.Models;

var item = new MenuItem(1, "Shiro", "Main Course", 10.0m);
Console.WriteLine(item.Id + " " + item.Name + " " + item.Category + " " + item.Price);
Console.WriteLine(item.PriceWithVat());

List<MenuItem>menuItems=new List<MenuItem>
{
    new MenuItem(1, "Shiro", "Main Course", 10.0m),
    new MenuItem(2, "Doro Wat", "Main Course", 20.0m),
    new MenuItem(3, "Beyaynetu", "Main Course", 30.0m)
};

for(int i=0;i<menuItems.Count;i++)
{
    Console.WriteLine(menuItems[i].Id + " " + menuItems[i].Name + " " + menuItems[i].Category + " " + menuItems[i].Price);
}