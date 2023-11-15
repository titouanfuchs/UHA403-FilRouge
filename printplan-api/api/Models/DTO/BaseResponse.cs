using printplan_api.Models.Enums;

namespace printplan_api.Models.DTO;

public class BaseResponse
{
    public BaseResponse()
    {
    }

    public Status Status { get; set; }
    public string Message { get; set; } = "Message";
}