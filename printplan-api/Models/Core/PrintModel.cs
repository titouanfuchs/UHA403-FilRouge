using System.ComponentModel.DataAnnotations;

namespace printplan_api.Models.Core;

public class PrintModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public float RequiredFilamentLenght { get; set; }
}