using System.ComponentModel.DataAnnotations;

namespace BlazorSample.Shared.Models;

public class LemurFormModel
{
    [Required]
    public string Question { get; set; }
}