using Flashcards.Controller;
using Flashcards.Helpers;

namespace Flashcards.View;

internal class Menus
{
    public static void MainMenu()
    {
        Console.Clear();
        var closeApp = false;
        while (closeApp == false)
        {
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?\n");
            Console.WriteLine("====================================");
            Console.WriteLine("Type 0 to Exit");
            Console.WriteLine("Type 1 to Manage Stacks");
            Console.WriteLine("Type 2 to Manage Flashcards");
            Console.WriteLine("Type 3 to Begin a Study Session");
            Console.WriteLine("Type 4 to View Study Session History");
            Console.WriteLine("====================================\n");

            var commandInput = Console.ReadLine();

            switch (commandInput.Trim())
            {
                case "0":
                    Console.WriteLine("\nGoodbye!");
                    closeApp = true;
                    break;

                case "1":
                    StacksMenu();
                    break;

                case "2":
                    FlashcardsMenu();
                    break;

                case "3":
                    StudyCards.Study();
                    break;

                case "4":
                    string stackId = "";
                    DbAccess.DisplayAllStacks(stackId);
                    Console.WriteLine("Enter the Stack Id of the  sessions you would like to view or press Enter to view all: ");
                    string? userInput = Console.ReadLine();
                    DbAccess.ViewSessions(userInput.Trim());
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Review the menu and enter a valid command.");
                    break;
            }
        }
    }

    internal static void FlashcardsMenu()
    {
        Console.Clear();
        var closeMenu = false;
        while (closeMenu == false)
        {
            Console.WriteLine("\n\nFlashcards MENU");
            Console.WriteLine("\nWhat would you like to do?\n");
            Console.WriteLine("================================");
            Console.WriteLine("Type 0 to Exit");
            Console.WriteLine("Type 1 to Insert Flashcards");
            Console.WriteLine("Type 2 to Update Flashcards");
            Console.WriteLine("Type 3 to Delete a Flashcard");
            Console.WriteLine("Type 4 to View all Flashcards");
            Console.WriteLine("================================\n");

            var commandInput = Console.ReadLine();

            switch (commandInput.Trim())
            {
                case "0":
                    MainMenu();
                    closeMenu = true;
                    break;

                case "1":
                    DbManager.InsertFlashcards();
                    break;

                case "2":
                    DbManager.UpdateFlashcard();
                    break;

                case "3":
                    DbManager.DeleteFlashcard();
                    break;

                case "4":
                    DbAccess.DisplayAllFlashcards("");
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Review the menu and enter a valid command.");
                    break;
            }
        }

    }

    internal static void StacksMenu()
    {
        Console.Clear();
        var closeMenu = false;
        while (closeMenu == false)
        {
            Console.WriteLine("\n\nStacks MENU");
            Console.WriteLine("\nWhat would you like to do?\n");
            Console.WriteLine("================================");
            Console.WriteLine("Type 0 to Exit");
            Console.WriteLine("Type 1 to View all Stacks");
            Console.WriteLine("Type 2 to Insert Stacks");
            Console.WriteLine("Type 3 to Delete a Stack");
            Console.WriteLine("================================\n");

            var commandInput = Console.ReadLine();
            Console.Clear();
            switch (commandInput.Trim())
            {
                case "0":
                    closeMenu = true;
                    MainMenu();
                    break;
                case "1":
                    string stackId = "";
                    DbAccess.DisplayAllStacks(stackId);
                    break;

                case "2":
                    DbManager.InsertStack();
                    break;

                case "3":
                    DbManager.DeleteStack();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Review the menu and enter a valid command.");
                    break;
            }
        }
    }
}

