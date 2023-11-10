namespace printplan_api.Models.DTO;

public class PlanResponse : BaseResponse
{
    public PlanResponse(){}
    public PrintPlanDto Plan { get; set; }
}