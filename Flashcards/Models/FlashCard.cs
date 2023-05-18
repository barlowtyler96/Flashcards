
namespace Flashcards.Models;

internal class FlashCard
{
    public int Id { get; set; }

    public string? Front { get; set; }

    public string? Back { get; set; }

    public int StacksId { get; set; }

    public string? StackName { get; set; }
}

internal class FlashCardDto
{
    public string? Front { get; set; }

    public string? Back { get; set; }
}
