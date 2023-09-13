using System.ComponentModel.DataAnnotations;

namespace printplan_api.Models.DTO;

public struct PostPrintPlanDto
{
    /// <summary>
    /// Quantité de modèle à imprimer
    /// </summary>
    [Required]
    public int Quantity { get; set; } = 1;
    
    /// <summary>
    /// Id du modèle à imprimer
    /// </summary>
    [Required]
    public int PrintModelId { get; set; } = 0;
    
    public PostPrintPlanDto(){}
}