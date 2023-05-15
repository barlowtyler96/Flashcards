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
    public static List<FlashCardDto> GetCardsFromStack(string stackId)//pass stack id
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        //for loop through the select and increment id by one each time
        var cardsFromStackString =
            $@"SELECT Front, Back  FROM Flashcards
                 WHERE StacksId = {stackId}";

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
                        Front = reader.GetString(1),
                        Back = "Answer"
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
                        //Id = reader.GetInt32(0),
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
