using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper;


public struct Room
{
    public string City { get; set; }
    public string University { get; set; }
    public string UniversityLocation { get; set; }
    public double UniversityLong { get; set; }
    public double UniversityLat { get; set; }
    public string RoomLocation { get; set; }
    public double roomLat { get; set; }
    public double roomLong { get;  set; }
    public double PricePerMonth { get; set; }
    public double distance { get; set; }
    public string weather { get; set; }


}

public class RoomService
{
    public static List<Room> Rooms { get; set; } = new List<Room>();

    public RoomService(string filePath)
    {
        LoadRoomsFromFile(filePath).Wait();
    }
    public static async Task LoadRoomsFromFile(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<dynamic>().ToList();

            foreach (var record in records)
            {
                Room room = new Room
                {
                    City = record.City,
                    University = record.University,
                    UniversityLocation = record.UniversityLocation,
                    UniversityLong = Convert.ToDouble(record.UniversityLongitude),
                    UniversityLat = Convert.ToDouble(record.UniversityLatitude),
                    RoomLocation = record.RoomLocation,
                    roomLat = Convert.ToDouble(record.RoomLatitude),
                    roomLong = Convert.ToDouble(record.RoomLongitude),
                    PricePerMonth = Convert.ToDouble(record.Price)
                };


                room.distance = await CalculateDistance(room);
                room.weather = await GetWeather(room);

                Rooms.Add(room);
            }
        }
    }

    private static async Task<double> CalculateDistance(Room r)
    {
        string startLong = r.roomLong.ToString();
        string startLat = r.roomLat.ToString();

        string endLong = r.UniversityLong.ToString();
        string endLat = r.UniversityLat.ToString();

        string url = "http://router.project-osrm.org/table/v1/driving/";

        url = url + startLat + "," + startLong + ";" + endLat + "," + endLong;
        url = url + "?annotations=distance";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string _content = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(_content);

                double? distance = json["distances"]?[0]?[1]?.Value<double>();

                return distance ?? -1; 
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error calculating distance: {e.Message}");
                return -1;
            }
        }
    }


    private static async Task<string> GetWeather(Room r)
    {
        string lat = r.roomLat.ToString();
        string lon = r.roomLong.ToString();


        string url = "https://www.7timer.info/bin/astro.php?lon=" + lon + "&lat=" + lat + "&ac=0&unit=metric&output=json";


        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string _content = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(_content);

                var temp = json["dataseries"]?[0]?["temp2m"]?.Value<double>();

                if (temp.HasValue)
                {
                    return $"{temp.Value}°C";
                }
                else
                {
                    return "Weather data unavailable";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting temperature: {e.Message}");
                return "Error retrieving temperature";
            }
        }
    }

}
