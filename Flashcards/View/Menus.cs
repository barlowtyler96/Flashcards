using Flashcards.Controller;
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
            Console.WriteLine("================================");
            Console.WriteLine("Type 0 to Exit");
            Console.WriteLine("Type 1 to Manage Stacks");
            Console.WriteLine("Type 2 to Manage Flashcards");
            Console.WriteLine("Type 3 to Begin a Study Session");
            Console.WriteLine("Type 4 to View Study Session Data");
            Console.WriteLine("================================\n");

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
                    //DbController.Update();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Review the menu and enter a valid command.");
                    break;
            }
        }
    }

    private static void FlashcardsMenu()
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
            Console.WriteLine("Type 3 to Delete a Flashcards");
            Console.WriteLine("================================\n");

            var commandInput = Console.ReadLine();

            switch (commandInput.Trim())
            {
                case "0":
                    MainMenu();
                    closeMenu = true;
                    break;

                case "1":
                    DbController.InsertFlashcards();
                    break;

                case "2":
                    DbController.UpdateFlashcards();
                    break;

                case "3":
                    DbController.DeleteFlashcards();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Review the menu and enter a valid command.");
                    break;
            }
        }

    }

    private static void StacksMenu()
    {
        Console.Clear();
        var closeMenu = false;
        while (closeMenu == false)
        {
            Console.WriteLine("\n\nStacks MENU");
            Console.WriteLine("\nWhat would you like to do?\n");
            Console.WriteLine("================================");
            Console.WriteLine("Type 0 to Exit");
            Console.WriteLine("Type 1 to Insert Stacks");
            Console.WriteLine("Type 2 to Update Stacks");
            Console.WriteLine("Type 3 to Delete a Stack");
            Console.WriteLine("================================\n");

            var commandInput = Console.ReadLine();

            switch (commandInput.Trim())
            {
                case "0":
                    closeMenu = true;
                    MainMenu();
                    break;

                case "1":
                    DbController.InsertStack();
                    break;

                case "2":
                    DbController.UpdateStack();
                    break;

                case "3":
                    DbController.DeleteStack();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Review the menu and enter a valid command.");
                    break;
            }
        }
    }
}

