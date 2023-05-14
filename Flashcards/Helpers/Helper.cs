using Flashcards.Controller;
using Flashcards.View;

namespace Flashcards.Helpers;

internal class Helper
{
    internal static string GetStackId()
    {
        DbAccess.DisplayAllStacks();
        Console.WriteLine("Enter the ID of the stack you would like to view\n" +
                           " or type 0 to return to Main Menu");
        var idInput = Console.ReadLine();

        if (idInput == "0")
            Menus.MainMenu();
        return idInput;
    }
}
