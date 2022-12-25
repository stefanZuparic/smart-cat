// See https://aka.ms/new-console-template for more information
using etl.DTOs;
using etl.Helpers;
using etl.Services;

Console.WriteLine("Hello, World!");


ApiHelper.InitialazeApi();

ShiftService shiftsService = new ShiftService();
KpisService kpisService = new KpisService();
BreakService breakService = new BreakService();
List<ShiftDto> shifts = new List<ShiftDto>();

shifts = await shiftsService.LoadShift();

shiftsService.Save(shifts);

kpisService.TotalNumberOfPaidBreaks();
kpisService.MinShiftLengthInHours();
kpisService.MeanShiftCost();
kpisService.MeanBreakLengthInMinutes();
kpisService.MaxBreakFreeShiftPeriodInDays();

Console.WriteLine("Max brak:" + shiftsService.MaxBreakFreeShiftPeriodInDays());

Console.ReadLine();