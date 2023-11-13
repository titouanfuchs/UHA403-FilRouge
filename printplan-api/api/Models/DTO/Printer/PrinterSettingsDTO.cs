namespace printplan_api.Models.DTO.Printer;

public struct PrinterSettingsDTO
{
    /// <summary>
    ///     Nom de l'imprimante 3D
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Vitesse d'impression de l'imprimante 3D en mm/s
    /// </summary>
    public float? PrinterSpeed { get; set; }

    /// <summary>
    ///     Durée de préchauffage en secondes
    /// </summary>
    public float? PreheatingDuration { get; set; }

    public PrinterSettingsDTO()
    {
    }

    public PrinterSettingsDTO(Core.Printer printer)
    {
        Name = printer.Name;
        PrinterSpeed = printer.PrinterSpeed;
        PreheatingDuration = printer.PreheatingDuration;
    }
}