using printplan_api.Contexts;
using printplan_api.Models.DTO;

namespace printplan_api.Services;

public class PlanService
{
    private readonly PrintPlanContext _context;
    public PlanService(PrintPlanContext context)
    {
        _context = context;
    }

    public PrintPlanDto Plan(PrintPlanDto input)
    {
        
        
        return new();
    }
}