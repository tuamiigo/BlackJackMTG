using System.ComponentModel.DataAnnotations;
namespace BlackJackMTG.Models;

public class SearchModel
{
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Search term should contain only letters.")]
    public string? SearchTerm { get; set; }
}