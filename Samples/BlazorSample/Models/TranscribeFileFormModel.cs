using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorSample.Models;

public class TranscribeFileFormModel
{
    [Required]
    public IBrowserFile File { get; set; }

    [Required] public string LanguageCode { get; set; } = "ALD";
}