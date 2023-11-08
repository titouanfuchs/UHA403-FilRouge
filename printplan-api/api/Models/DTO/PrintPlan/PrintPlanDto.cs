using printplan_api.Models.Core;

namespace printplan_api.Models.DTO;

public struct PrintPlanDto
{
    /// <summary>
    ///     Prédiction de remplacement de la bobine de fil lors d'une planification d'impression
    /// </summary>
    public List<FilamentReplacementEvent> SpoolReplacementEvents { get; set; } = new();

    /// <summary>
    ///     Quantité initiale à imprimer
    /// </summary>
    public int InitialPrintQuantity { get; set; }
    
    /// <summary>
    ///     Quantité finale à imprimer
    /// </summary>
    public int PrintQuantity { get; set; }
    
    /// <summary>
    ///     Durée totale d'impression
    /// </summary>
    public TimeSpan TotalDuration { get; set; }

    /// <summary>
    ///     Durée d'impression d'une unité du modèle
    /// </summary>
    public TimeSpan UnitDuration { get; set; }

    /// <summary>
    ///     Quantité de bobines de fil nécéssaire à l'impression
    /// </summary>
    public int RequiredSpoolQuantity { get; set; }

    public PrintPlanDto()
    {
    }
}