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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ST10287165_POE_P3
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Random random = new Random();    
        public DispatcherTimer dt = new DispatcherTimer();
        private int progressValue = 0;
        private readonly string[] cookingTips = new string[]
        {
            "#TipOfTheDay: Keep your knives sharp.",
            "#TipOfTheDay: Read the entire recipe before starting.",
            "#TipOfTheDay: Measure your ingredients accurately.",
            "#TipOfTheDay: Clean as you go.",
            "#TipOfTheDay: Use fresh herbs for better flavor."
        };
        public MainWindow()
        {
            InitializeComponent();
            dt.Interval = TimeSpan.FromMilliseconds(75); 
            dt.Tick += Dt_Tick; // Ensure the tick event is correctly attached

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            dt.Start(); // Start the timer that handles the progress bar update
            var storyboard = (Storyboard)FindResource("TipFadeIn");
            storyboard.Begin();
            tipTextBlock.Text = cookingTips[random.Next(cookingTips.Length)]; // Display a random tip on load
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            progressValue++; // Increment the progress value
            progressBar.Value = progressValue; // Update the progress bar's value

            if (progressValue >= 100)
            {
                dt.Stop(); // Stop the timer once the progress reaches 100%
                NavigateToNextPage(); // Call a method to navigate to the next page
            }
        }

        private void NavigateToNextPage()
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Storyboard steamAnimation = this.FindResource("SteamAnimation") as Storyboard;
        }

        /*private void OnMediaEnded(object sender, RoutedEventArgs e)
        {
            MediaElement m = (MediaElement)sender;
            m.Position = TimeSpan.Zero;
            m.Play(); // Play the media again if it ends
        }*/
    }
}
