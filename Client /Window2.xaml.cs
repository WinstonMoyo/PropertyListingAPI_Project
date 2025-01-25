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
using System.Windows.Shapes;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2(string type, List<Room> rs)
        {
            InitializeComponent();

            if(type == "booked")
            {
                viewerLabel.Content = "Booked Rooms";
            }

            if (type == "canceled")
            {
                viewerLabel.Content = "Canceled Rooms";
            }

            dataViewer.ItemsSource = rs;
        }
    }
}
