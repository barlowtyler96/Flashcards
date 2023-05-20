using ConsoleTableExt;
using Flashcards.Controller;
using Flashcards.Models;
using Flashcards.View;

namespace Flashcards.Helpers;

internal class Helper
{
    internal static void DisplayData(List<Stack> tableData)
    {
        ConsoleTableBuilder
            .From(tableData)
            .WithTitle($"Total Stacks: {tableData.Count}")
            .ExportAndWriteLine();
        ContinueMessage();
    }
    internal static void DisplayData(List<StudySession> tableData)
    {
        Console.Clear();
        ConsoleTableBuilder
            .From(tableData)
            .WithTitle($"Entries: {tableData.Count}")
            .ExportAndWriteLine();
        ContinueMessage();
    }
    internal static void DisplayData(List<FlashCard> tableData)
    {
        Console.Clear();
        ConsoleTableBuilder
            .From(tableData)
            .WithTitle($"Total Flashcards: {tableData.Count}")
            .ExportAndWriteLine();
        ContinueMessage();
    }

    internal static void ContinueMessage()
    {
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
        Console.Clear();
    }

    internal static void ConfirmStackDelete(string stackId)
    {
        bool result = false;
        Console.WriteLine($"Are you sure you want to delete the following stack: {stackId}?\n" +
                          $"(enter yes or no)");
        var userInput = Console.ReadLine();

        while(result == false)
        {
            if (userInput == "yes")
                result = true;
            else if (userInput == "no")
                Menus.StacksMenu();
        }
        Console.Clear();
    }

    internal static string GetStackId(string message)
    {
        Console.Clear ();
        string stackId = "";
        DbAccess.DisplayAllStacks(stackId);
        Console.WriteLine(message);
        var idInput = Console.ReadLine();

        if (idInput == "0")
            Menus.MainMenu();
        return idInput;
    }

    internal static (string, string) GetFlashcardValues()
    {
        Console.WriteLine("Enter the Front of the card: ");
        var userInputFront = Console.ReadLine().Trim().ToLower();

        Console.WriteLine("Enter the Back of the card: ");
        var userInputBack = Console.ReadLine().Trim().ToLower();

        return (userInputFront, userInputBack);
    }
}
