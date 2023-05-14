
namespace Flashcards.Models;

internal class FlashCard
{
    public int Id { get; set; }

    public string? Front { get; set; }

    public string? Back { get; set; }

    public int StacksId { get; set; }    
}

internal class FlashCardDto //use this to display to user without the StackId above
{
    public int Id { get; set; }

    public string? Front { get; set; }

    public string? Back { get; set; }
}
