using ConsoleTableExt;
using Flashcards.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Flashcards.Controller;

internal class DbAccess
{
    private static readonly string? connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
    //Data Source = (localdb)\FlashcardsServer;Initial Catalog = FlashCardsDB
    public static List<FlashCardDto> CardsFromStackWithoutAnswer(string stackId)//pass stack id
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        //for loop through the select and increment id by one each time
        var cardsFromStackString =
            $@"SELECT Front, Back FROM Flashcards
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
                        Front = reader.GetString(0),
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

    public static List<FlashCard> CardsFromStackWithAnswer(string stackId)//pass stack id
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        //for loop through the select and increment id by one each time
        var cardsFromStackString =
            $@"SELECT * FROM Flashcards
                 WHERE StacksId = {stackId}";

        SqlCommand cardsFromStack = new SqlCommand(cardsFromStackString, connection);
        SqlDataReader reader = cardsFromStack.ExecuteReader();
        var tableData = new List<FlashCard>();


        if (reader.HasRows)
        {
            while (reader.Read())
            {
                tableData.Add(
                    new FlashCard
                    {
                        Id = reader.GetInt32(0),
                        Front = reader.GetString(1),
                        Back = reader.GetString(2),
                        StacksId = reader.GetInt32(3),
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
        DataTable dataTable = new DataTable();

        if (reader.HasRows)
        {
            dataTable.Load(reader);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n\nNo cards found.");
        }
        ConsoleTableBuilder
                    .From(dataTable)
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
        DataTable dataTable = new DataTable();

        if (reader.HasRows)
        {
            dataTable.Load(reader);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n\nNo stacks found.");
        }
        ConsoleTableBuilder
                    .From(dataTable)
                    .ExportAndWriteLine();
    }
}
