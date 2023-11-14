using System.ComponentModel.DataAnnotations;
using printplan_api.Models.Core;

namespace printplan_api.Models.DTO;

public struct PostPrintPlanDto
{
    /// <summary>
    /// Id de l'imprimante qui devra réaliser l'impression
    /// </summary>
    [Required] 
    public int PrinterId { get; set; } = 0;
    
    /// <summary>
    ///     Quantité de modèle à imprimer
    /// </summary>
    [Required]
    public int Quantity { get; set; } = 1;

    /// <summary>
    ///     Id du modèle à imprimer
    /// </summary>
    [Required]
    public int PrintModelId { get; set; } = 0;

    public PostPrintPlanDto()
    {
    }

    public PostPrintPlanDto(PrintingSlot slot)
    {
        PrinterId = 1;
        Quantity = slot.Quantity;
        PrintModelId = slot.CurrentModel.Id;
    }
}