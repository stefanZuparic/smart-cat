// See https://aka.ms/new-console-template for more information
using etl.DTOs;
using etl.Helpers;
using etl.Services;

Console.WriteLine("Hello, World!");


ApiHelper.InitialazeApi();

ShiftsService shiftsService = new ShiftsService();

List<ShiftDto> shifts = new List<ShiftDto>();

shifts = await shiftsService.LoadShift();

shiftsService.SaveShifts(shifts);

Console.ReadLine();