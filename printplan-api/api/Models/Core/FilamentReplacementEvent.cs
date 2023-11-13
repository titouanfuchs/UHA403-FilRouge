namespace printplan_api.Models.Core;

public struct FilamentReplacementEvent
{
    public TimeSpan ReplacementDate { get; set; }
    
    public int SpoolId { get; set; }
}