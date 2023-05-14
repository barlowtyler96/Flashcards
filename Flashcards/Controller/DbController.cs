using System.Configuration;
using System.Data.SqlClient;
namespace Flashcards.Controller;

internal class DbController
{
    private static readonly string? connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
    //create database first
    public void CreateTables()
    {
        SqlConnection connection = new SqlConnection(connectionString);

        var createStacksString =
            $@"IF NOT EXISTS (SELECT * FROM sysobjects
                WHERE name='Stacks' and xtype='U')
                    CREATE TABLE Stacks(
	                ID INT PRIMARY KEY IDENTITY NOT NULL,
	                StackName NVARCHAR(20) NOT NULL UNIQUE";

        SqlCommand createStacksTable = new SqlCommand(createStacksString, connection);

        var createFlashcardsString =
            $@"IF NOT EXISTS (SELECT * FROM sysobjects
                WHERE name='Flashcards' and xtype='U')
                    CREATE TABLE Flashcards(
                    ID INT PRIMARY KEY IDENTITY NOT NULL,
                    Front NVARCHAR(25) NOT NULL,
                    Back NVARCHAR(25) NOT NULL,
                    StacksID INT FOREIGN KEY REFERENCES Stacks(ID)";

        SqlCommand createFlashcardsTable = new SqlCommand(createFlashcardsString, connection);

        try
        {
            connection.Open();
            createStacksTable.ExecuteNonQuery();
            createFlashcardsTable.ExecuteNonQuery();
        }
        catch
        {
            
        }
    }


    static void ControllerMenu()
    {

    }

    internal static void InsertFlashcards()
    {

    }
    internal static void UpdateFlashcards()
    {

    }
    internal static void DeleteFlashcards()
    {

    }
    internal static void InsertStack()
    {

    }
    internal static void UpdateStack()
    {

    }
    internal static void DeleteStack()
    {
        //call delete all flashcards from same stack
    }
}
