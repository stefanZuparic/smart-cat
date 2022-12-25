using etl.DTOs;
using etl.Helpers;
using etl.Services;

ApiHelper.InitialazeApi();

ShiftService shiftsService = new ShiftService();
KpiService kpisService = new KpiService();
BreakService breakService = new BreakService();
AllowanceService allowanceService = new AllowanceService();

List<ShiftDTO> shifts = new List<ShiftDTO>();

try
{
    shifts = await shiftsService.LoadShifts();

    shiftsService.Save(shifts);
}
catch(Exception e)
{
    Console.WriteLine("Shifts API is not available. Error message - " + e.Message);
}

kpisService.CalculateKpis();

Console.WriteLine("All tasks done!");
