using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Text.Json;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public struct Room
    {
        public string city { get; set; }
        public string university { get; set; }
        public string universityLocation { get; set; }
        public string universityLong { get; set; }
        public string universityLat { get; set; }
        public string roomLocation { get; set; }
        public string roomName { get; set; }
        public string roomLat { get; set; }
        public string roomLong { get; set; }
        public string pricePerMonth { get; set; }
        public string distance { get; set; }
        public string weather { get; set; }


    }

    public partial class MainWindow : Window
    {
        private List<Room> rooms { get; set; } = new List<Room>();
        public List<Room> temp { get; set; } = new List<Room>();

        private HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            _ = loadRooms();
        }
        public async Task loadRooms()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "http://4.231.238.73:5000/api/Room";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {

                        rooms = JsonSerializer.Deserialize<List<Room>>(responseBody);


                        if (rooms != null)
                        {
                            Console.WriteLine($"Loaded {rooms.Count} rooms from the API.");
                            dataGridRooms.ItemsSource = rooms;
                        }
                        else
                        {
                            Console.WriteLine("Failed to deserialize the rooms data. The response might be in an unexpected format.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Received empty response from the API.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public async Task loadBookedRooms()
        {

                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "http://4.231.238.73:5000/api/Room/";
                string bookedURL = apiUrl + "bookedRooms";

                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(bookedURL);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(responseBody))
                        {

                            temp = JsonSerializer.Deserialize<List<Room>>(responseBody);


                            if (temp != null)
                            {
                                Console.WriteLine($"Loaded {temp.Count} rooms from the API.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to deserialize the rooms data. The response might be in an unexpected format.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Received empty response from the API.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            
        }

        public async Task loadCanceledRooms()
        {

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "http://4.231.238.73:5000/api/Room/";
                string bookedURL = apiUrl + "canceledRooms";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(bookedURL);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {

                        temp = JsonSerializer.Deserialize<List<Room>>(responseBody);


                        if (temp != null)
                        {
                            Console.WriteLine($"Loaded {temp.Count} rooms from the API.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to deserialize the rooms data. The response might be in an unexpected format.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Received empty response from the API.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

        }
        private void viewRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            int selected_index = dataGridRooms.SelectedIndex;
            Window1 w1 = new Window1(rooms[selected_index], selected_index);
            w1.Show();
        }

        private void bookedRoomsBtn_Click(object sender, RoutedEventArgs e)
        {
            _ = loadBookedRooms();

            Window2 w2 = new Window2("booked", temp);
            w2.Show();
        }

        private void canceledRoomsBtn_Click(object sender, RoutedEventArgs e)
        {
            _ = loadCanceledRooms();

            Window2 w2 = new Window2("canceled", temp);
            w2.Show();
        }
    }
}
