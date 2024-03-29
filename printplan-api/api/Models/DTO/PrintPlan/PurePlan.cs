using System.ComponentModel.DataAnnotations;

namespace printplan_api.Models.DTO;

public class PurePlan
{
    /// <summary>
    ///     Quantité de modèle à imprimer
    /// </summary>
    [Required]
    public int Id { get; set; } = 0;
    
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
}