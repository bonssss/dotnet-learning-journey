namespace AddisEats.Exercises;

public static class Exercise3
{
    public static void Run()
    {
        Console.WriteLine("=== Exercise 3: Primitive Types in Addis Eats ===");

        // Primitive types with realistic Addis Eats domain values
        string dishName = "Doro Wet with Injera";
        char deliveryZone = 'B';
        byte spiceLevel = 3;
        sbyte refrigerationTempCelsius = 4;
        short prepTimeMinutes = 25;
        ushort estimatedCalories = 850;
        int orderId = 1042;
        uint customerPoints = 1500;
        long transactionId = 202611090001042L;
        ulong fiscalReceiptNumber = 9876543210123456789UL;
        float deliveryDistanceKm = 3.8f;
        double restaurantRating = 4.85;
        decimal unitPriceEtb = 450.50m;
        bool isFastingFriendly = false;

        // Print each variable with its type name using $ interpolated strings
        Console.WriteLine($"Type: {dishName.GetType().Name,-7} | {nameof(dishName)}: {dishName}");
        Console.WriteLine($"Type: {deliveryZone.GetType().Name,-7} | {nameof(deliveryZone)}: '{deliveryZone}'");
        Console.WriteLine($"Type: {spiceLevel.GetType().Name,-7} | {nameof(spiceLevel)}: {spiceLevel} (scale 1-5)");
        Console.WriteLine($"Type: {refrigerationTempCelsius.GetType().Name,-7} | {nameof(refrigerationTempCelsius)}: {refrigerationTempCelsius} °C");
        Console.WriteLine($"Type: {prepTimeMinutes.GetType().Name,-7} | {nameof(prepTimeMinutes)}: {prepTimeMinutes} mins");
        Console.WriteLine($"Type: {estimatedCalories.GetType().Name,-7} | {nameof(estimatedCalories)}: {estimatedCalories} kcal");
        Console.WriteLine($"Type: {orderId.GetType().Name,-7} | {nameof(orderId)}: #{orderId}");
        Console.WriteLine($"Type: {customerPoints.GetType().Name,-7} | {nameof(customerPoints)}: {customerPoints} pts");
        Console.WriteLine($"Type: {transactionId.GetType().Name,-7} | {nameof(transactionId)}: {transactionId}");
        Console.WriteLine($"Type: {fiscalReceiptNumber.GetType().Name,-7} | {nameof(fiscalReceiptNumber)}: {fiscalReceiptNumber}");
        Console.WriteLine($"Type: {deliveryDistanceKm.GetType().Name,-7} | {nameof(deliveryDistanceKm)}: {deliveryDistanceKm} km");
        Console.WriteLine($"Type: {restaurantRating.GetType().Name,-7} | {nameof(restaurantRating)}: {restaurantRating} / 5.0");
        Console.WriteLine($"Type: {unitPriceEtb.GetType().Name,-7} | {nameof(unitPriceEtb)}: {unitPriceEtb:N2} ETB");
        Console.WriteLine($"Type: {isFastingFriendly.GetType().Name,-7} | {nameof(isFastingFriendly)}: {isFastingFriendly}");
    }
}

