// See https://aka.ms/new-console-template for more information
using GridDataAPI;
using System.Data.SqlClient;

GridDataController gridDataController = new GridDataController();

// Change Datetime to test different result.
DateTime startTime = DateTime.Parse("2024/04/27 20:00");
DateTime endTime = DateTime.Parse("2024/04/27 23:00");
DateTime collectedTime = DateTime.Parse("2024/04/28 23:00");

// Result of API that will accept a start datetime and end datetime. The API should return the latest value for each timestamp in the date range. 
string? latestValue = gridDataController.GetLatestValue(startTime, endTime);

Console.WriteLine($"The latest value between {startTime.ToString("yyyy/MM/dd HH:mm:ss")} and {endTime.ToString("yyyy/MM/dd HH:mm:ss")} is {latestValue}");

// Result of API that will accept a start datetime, end datetime and collected datetime. The API should return the value corresponding to the collected datetime for each timestamp in the date range. 
List<string>? values = gridDataController.GetCollectedData(startTime, endTime, collectedTime);

Console.WriteLine("The Values collected are");

foreach (string value in values)
{
    Console.WriteLine(value);
}