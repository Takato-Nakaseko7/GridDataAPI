// See https://aka.ms/new-console-template for more information
using GridDataAPI;

GridDataController gridDataController = new GridDataController();

DateTime startTime = DateTime.Parse("2024/04/27 00:00");
DateTime endTime = DateTime.Parse("2024/04/27 05:00");

string? latestValue = gridDataController.GetLatestValue(startTime, endTime);

Console.WriteLine(latestValue);


