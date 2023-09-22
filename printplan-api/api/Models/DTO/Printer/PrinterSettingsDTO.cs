using System.Runtime.InteropServices;

namespace printplan_api.Models.DTO.Printer;

public struct PrinterSettingsDTO
{
    /// <summary>
    /// Nom de l'imprimante 3D
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Vitesse d'impression de l'imprimante 3D en mm/s
    /// </summary>
    public float PrinterSpeed { get; set; }
    
    /// <summary>
    /// Durée de préchauffage en secondes
    /// </summary>
    public float PreaheatingDuration { get; set; }
}