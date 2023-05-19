using ConsoleTableExt;
using Flashcards.Controller;
using Flashcards.Models;
using Flashcards.View;
using System.Security.AccessControl;

namespace Flashcards.Helpers;

internal class Helper
{
    internal static void DisplayData(List<Stack> tableData)
    {
        ConsoleTableBuilder
            .From(tableData)
            .ExportAndWriteLine();
    }
    internal static void DisplayData(List<StudySessionDto> tableData)
    {
        ConsoleTableBuilder
            .From(tableData)
            .ExportAndWriteLine();
    }
    internal static void DisplayData(List<FlashCard> tableData)
    {
        ConsoleTableBuilder
            .From(tableData)
            .ExportAndWriteLine();
    }

    internal static string GetStackId(string message)
    {
        string stackId = "";
        DbAccess.DisplayAllStacks(stackId);
        Console.WriteLine(message);
        var idInput = Console.ReadLine();

        if (idInput == "0")
            Menus.MainMenu();
        return idInput;
    }
}
