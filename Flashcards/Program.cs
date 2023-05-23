using Flashcards.Controller;
using Flashcards.View;
namespace Flashcards;

internal class Program
{
    static void Main(string[] args)
    {
        DbManager.CreateTables();
        Menus.MainMenu();
    }
}