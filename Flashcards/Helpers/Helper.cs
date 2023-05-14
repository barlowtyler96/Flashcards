using Flashcards.Controller;

namespace Flashcards.Helpers;

internal class Helper
{
    internal static string GetStackId()
    {
        DbAccess.DisplayAllStacks();
        Console.WriteLine("Enter the ID of the stack you would like to view: ");
        var idInput = Console.ReadLine();
        return idInput;
    }
}
