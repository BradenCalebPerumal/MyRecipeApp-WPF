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
using System.Windows.Shapes;

namespace ST10287165_POE_P3
{
    /// <summary>
    /// Interaction logic for AddRecipe.xaml
    /// </summary>

    /// 
    public partial class AddRecipe : Window
    {

/*        public List<Recipe> recBook = new List<Recipe>();
        public static double OriginalServe = 0;
        double quanSizeTemp = 0.0;
        double numIngred = 0;*/
        public AddRecipe()
        {
            InitializeComponent();

        }
            private void Window_Loaded(object sender, RoutedEventArgs e)
            {
                var inputFieldsStoryboard = (Storyboard)FindResource("InputFieldsAnimation");
                inputFieldsStoryboard.Begin((FrameworkElement)this.Content);
            }

  
    
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // Collect data from text boxes
            string recipeName = tbRecipeName.Text;
            double servingSize;
            int numberOfIngredients;
            int numberOfSteps;

            // Validate and parse input
            if (double.TryParse(tbServingSize.Text, out servingSize) &&
                int.TryParse(tbNumberIngreds.Text, out numberOfIngredients) &&
                int.TryParse(tbNumberSteps.Text, out numberOfSteps))
            {
                // Create a new Recipe instance and populate it
                Recipe newRecipe = new Recipe
                {
                    Name = recipeName,
                    AmtServe = servingSize,
                    OriginalServingSize = servingSize,
                    IngredientsList = new List<Ingredients>(numberOfIngredients),
                    Steps = new List<Step>(numberOfSteps)
                };

                // Open the next window to collect ingredients and steps
                AddIngredientsAndSteps addIngredientsAndSteps = new AddIngredientsAndSteps(newRecipe);
                addIngredientsAndSteps.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter valid values for serving size, number of ingredients, and number of steps.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    



        private void BackMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mo = new MainMenu();
            mo.Show();
            this.Close();
        }

        private void ExitAddrec_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbRecipeName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbServingSize_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbNumberIngreds_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbNumberSteps_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mo = new MainMenu();
            mo.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Exit mo = new Exit();   
            mo.Show();
            this.Close();  
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mo = new MainMenu();
            mo.Show();
            this.Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Exit mo = new Exit();
            mo.Show();
            this.Close();

        }
    }
    }
