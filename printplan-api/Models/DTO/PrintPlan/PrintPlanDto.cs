using printplan_api.Models.Core;

namespace printplan_api.Models.DTO;

public struct PrintPlanDto
{
    /// <summary>
    /// Prédiction de remplacement de la bobine de fil lors d'une planification d'impression
    /// </summary>
    public List<FilamentReplacementEvent> SpoolReplacementEvents { get; set; } = new List<FilamentReplacementEvent>();
    
    /// <summary>
    /// Durée totale d'impression
    /// </summary>
    public int TotalDuration { get; set; }
    
    /// <summary>
    /// Durée d'impression d'une unité du modèle
    /// </summary>
    public int UnitDuration { get; set; }
    
    /// <summary>
    /// Quantité de bobines de fil nécéssaire à l'impression
    /// </summary>
    public int RequiredSpoolQuantity { get; set; }
    
    public PrintPlanDto(){}
}