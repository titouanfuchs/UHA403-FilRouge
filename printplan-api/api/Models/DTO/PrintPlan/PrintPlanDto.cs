using printplan_api.Models.Core;

namespace printplan_api.Models.DTO;

public struct PrintPlanDto
{
    /// <summary>
    /// Identifiant de la planification d'origine
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Identifiant de l'imprimante associée a cette planification
    /// </summary>
    public int Printer { get; set; }
    /// <summary>
    /// Id du model a imprimer
    /// </summary>
    public int Model { get; set; }
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

    /// <summary>
    /// Quantité de fil nécéssair au total
    /// </summary>
    public float RequiredFilamentLenght { get; set; }
    
    public PrintPlanDto()
    {
    }
}