using ConsoleTableExt;
using Flashcards.Helpers;
using Flashcards.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Flashcards.Controller;

internal class DbAccess
{
    private static readonly string? connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
    //Data Source = (localdb)\FlashcardsServer;Initial Catalog = FlashCardsDB
    //ask user what stack they want to study
    //displayt first row of that stack without the Back of card
    //ask user for answer
    //check if answer was correct
    //prompt user to press enter to go to next card

    public static List<FlashCardDto> GetCardsFromStack(string stackId)//pass stack id
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        var cardsFromStackString =
            $@"SELECT * FROM Flashcards
                 WHERE StackId = {stackId}";

        SqlCommand cardsFromStack = new SqlCommand(cardsFromStackString, connection);
        SqlDataReader reader = cardsFromStack.ExecuteReader();

        var tableData = new List<FlashCardDto>();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                tableData.Add(
                    new FlashCardDto
                    {
                        Id = reader.GetInt32(0),
                        Front = reader.GetString(1),
                        Back = reader.GetString(2),
                    });
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n\nNo cards found.");
        }
        return tableData;
    }



    public static void DisplayAllFlashcards()//TODO close connection
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        var displayStacksString =
            $@"SELECT * FROM Flashcards
                ORDER BY Id";

        SqlCommand displayStacks = new SqlCommand(displayStacksString, connection);
        SqlDataReader reader = displayStacks.ExecuteReader();

        var tableData = new List<FlashCardDto>();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                tableData.Add(
                    new FlashCardDto
                    {
                        Id = reader.GetInt32(0),
                        Front = reader.GetString(1),
                        Back = reader.GetString(2),
                    });
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n\nNo cards found.");
        }
        ConsoleTableBuilder
                    .From(tableData)
                    .ExportAndWriteLine();

    }



    public static void DisplayAllStacks()//TODO close connection
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        var displayStacksString = 
            $@"SELECT * FROM Stacks
                ORDER BY Id";

        SqlCommand displayStacks = new SqlCommand(displayStacksString, connection);
        SqlDataReader reader = displayStacks.ExecuteReader();

        var tableData = new List<Stack>();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                tableData.Add(
                    new Stack
                    {
                        Id = reader.GetInt32(0), //returns values of column(i) specified
                        StackName = reader.GetString(1),
                    });
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n\nNo stacks found.");
        }
        ConsoleTableBuilder
                    .From(tableData)
                    .ExportAndWriteLine();
    }
}
