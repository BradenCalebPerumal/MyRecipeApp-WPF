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
using System.Windows.Threading;

namespace ST10287165_POE_P3
{
    /// <summary>
    /// Interaction logic for Exit.xaml
    /// </summary>
    public partial class Exit : Window
    {
        // Constructor for the Exit window
        public Exit()
        {
            InitializeComponent();
        }

        // Event handler for when the window is loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create and configure a timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5); // Set the timer interval to 5 seconds
            timer.Tick += Timer_Tick; // Attach the event handler for the Tick event
            timer.Start(); // Start the timer
        }

        // Event handler for the timer's Tick event
        private void Timer_Tick(object sender, EventArgs e)
        {
            ((DispatcherTimer)sender).Stop();  // Stop the timer
            this.Close();                      // Close the Exit window
            Application.Current.Shutdown();    // Shutdown the application
        }
    }
}
