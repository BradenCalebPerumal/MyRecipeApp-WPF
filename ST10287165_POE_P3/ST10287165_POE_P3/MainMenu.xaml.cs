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

namespace ST10287165_POE_P3
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        // Constructor for the MainMenu window
        public MainMenu()
        {
            InitializeComponent();
        }

        // Event handler for the "Add Recipe" button click
        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Open the AddRecipe window
            AddRecipe ar = new AddRecipe();
            ar.Show();
            // Close the current MainMenu window
            this.Close();
        }

        // Event handler for the "Search Recipe" button click
        private void SearchRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Open the ViewRecipe window
            ViewRecipe viewRecipeWindow = new ViewRecipe();
            viewRecipeWindow.Show();
            // Close the current MainMenu window
            this.Close();
        }

        // Event handler for the "Scale Recipe" button click
        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Open the ScalingRecipe window
            ScalingRecipe mo = new ScalingRecipe();
            mo.Show();
            // Close the current MainMenu window
            this.Close();
        }

        // Event handler for the "Delete Recipe" button click
        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Open the DeleteRecipe window
            DeleteRecipe mo = new DeleteRecipe();
            mo.Show();
            // Close the current MainMenu window
            this.Close();
        }

        // Event handler for the "Exit" button click
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Open the Exit window
            Exit mo = new Exit();
            mo.Show();
            // Close the current MainMenu window
            this.Close();
        }
    }
}
