using System.ComponentModel.DataAnnotations;

namespace printplan_api.Models.DTO;

public class PatchPrintPlanDto
{
    [Required]
    public int Id { get; set; }
    
    /// <summary>
    ///     Quantité de modèle à imprimer
    /// </summary>
    public int? Quantity { get; set; }
}