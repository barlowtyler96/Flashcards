using ConsoleTableExt;
using Flashcards.Controller;
using Flashcards.Models;
using Flashcards.View;
using System.Security.AccessControl;

namespace Flashcards.Helpers;

internal class Helper
{
  
    internal static string GetStackId(string message)
    {
        DbAccess.DisplayAllStacks();
        Console.WriteLine(message);
        var idInput = Console.ReadLine();

        if (idInput == "0")
            Menus.MainMenu();
        return idInput;
    }
}
