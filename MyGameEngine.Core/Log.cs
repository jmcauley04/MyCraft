namespace MyGameEngine.Core;

public static class Log
{
    public static void Normal(string msg) => Message(msg, ConsoleColor.White, "MSG");
    public static void Info(string msg) => Message(msg, ConsoleColor.Cyan, "INFO");
    public static void Warning(string msg) => Message(msg, ConsoleColor.Yellow, "WARNING");
    public static void Error(string msg) => Message(msg, ConsoleColor.Red, "ERROR");

    public static void Message(string msg, ConsoleColor color, string tag)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"[{tag}] - {msg}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
