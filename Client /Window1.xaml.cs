using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Room curRoom;
        private int curRoomId;
        public Window1(Room r, int index)
        {
            InitializeComponent();

            curRoom = r;
            curRoomId = index;

            displayValues(r);
            _ = getWeatherDistance(index);

        }

        public void displayValues(Room r)
        {
            cityTxt.Text = r.city;
            cityTxt.IsReadOnly = true;

            uniTxt.Text = r.university;
            uniTxt.IsReadOnly = true;

            uniLocTxt.Text = r.universityLocation;
            uniLocTxt.IsReadOnly = true;

            roomTxt.Text = r.roomName;
            roomTxt.IsReadOnly = true;

            roomLocTxt.Text = r.roomLocation;
            roomLocTxt.IsReadOnly = true;

            priceTxt.Text = r.pricePerMonth;
            priceTxt.IsReadOnly = true;

           

        }

        public async Task getWeatherDistance(int index)
        {
            string apiUrl = "http://4.231.238.73:5000/api/Room/";
            string distanceURL = apiUrl + "distance/" + index.ToString();
            string weatherURL = apiUrl + "weather/" + index.ToString();

            try
            {
                //Weather
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(weatherURL);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        weatherTxt.Text = responseBody;
                        weatherTxt.IsReadOnly = true;
                    }

                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }


            try
            {

                //Distance
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(distanceURL);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        distanceTxt.Text = responseBody;
                        distanceTxt.IsReadOnly = true;
                    }

                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public async Task bookRoom(int index)
        {
            string apiUrl = "http://4.231.238.73:5000/api/Room/";
            string bookURL = apiUrl + "book/" + index.ToString();

            try
            {

                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.PostAsync(bookURL, null);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        MessageBox.Show(responseBody);
                    }

                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public async Task cancelRoom(int index)
        {
            string apiUrl = "http://4.231.238.73:5000/api/Room/";
            string cancelURL = apiUrl + "cancel/" + index.ToString();

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.PostAsync(cancelURL, null);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        MessageBox.Show(responseBody);
                    }

                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bookBtn_Click(object sender, RoutedEventArgs e)
        {
            _ = bookRoom(curRoomId);
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _ = cancelRoom(curRoomId);
        }
    }
}
