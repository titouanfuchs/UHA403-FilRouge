namespace printplan_api.Models.Core;

public class Printer
{
    private string Name { get; set; }
    private float PrinterSpeed { get; set; }
    private float PreheatingDuration { get; set; }

    public Printer(string name, float printerSpeed, float preheatingDuration)
    {
        Name = name;
        PrinterSpeed = printerSpeed;
        PreheatingDuration = preheatingDuration;
    }
}