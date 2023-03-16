namespace PremiumIdentity.Helpers;

public class StartupMessageHelper
{
    public static void PrintWelcomeMessage()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Hello ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("!!");

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"You are using premium identity as your identity provider. Please visit our docs for guidance");
        Console.WriteLine();
    }
}
