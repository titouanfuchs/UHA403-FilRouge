using System.ComponentModel.DataAnnotations;

namespace printplan_api.Models.Core;

public class PrintModel
{
    [Key]
    private int Id { get; set; }
    private string Name { get; set; }
    private float RequiredFilamentLenght { get; set; }
}