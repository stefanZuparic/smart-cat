// See https://aka.ms/new-console-template for more information
using etl.DTOs;
using etl.Helpers;
using etl.Services;

Console.WriteLine("Hello, World!");


ApiHelper.InitialazeApi();

ShiftsService shiftsService = new ShiftsService();
KpisService kpisService = new KpisService();

List<ShiftDto> shifts = new List<ShiftDto>();

shifts = await shiftsService.LoadShift();

shiftsService.Save(shifts);

kpisService.TotalNumberOfPaidBreaks();

Console.ReadLine();