using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Windows.Shapes;

namespace ST10287165_POE_P3
{
    public partial class AddIngredientsAndSteps : Window
    {
        private Recipe _recipe;

        public AddIngredientsAndSteps(Recipe recipe)
        {
            InitializeComponent();
            _recipe = recipe;

            // Populate the ItemsControls with empty fields based on the number of ingredients and steps
            for (int i = 0; i < _recipe.IngredientsList.Capacity; i++)
            {
                IngredientsList.Items.Add(new Ingredients());
            }

            for (int i = 0; i < _recipe.Steps.Capacity; i++)
            {
                StepsList.Items.Add(new Step());
            }
        }

        private void SubmitRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Collect ingredient data
            foreach (var item in IngredientsList.Items)
            {
                var container = IngredientsList.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                if (container != null)
                {
                    var stackPanel = FindVisualChild<StackPanel>(container);
                    if (stackPanel != null)
                    {
                        string name = ((TextBox)stackPanel.Children[0]).Text;
                        double quantity;
                        ComboBox unitComboBox = (ComboBox)stackPanel.Children[2]; // Assuming unit ComboBox is at index 2
                        int calories;
                        ComboBox foodGroupComboBox = (ComboBox)stackPanel.Children[4]; // Assuming food group ComboBox is at index 4

                        if (double.TryParse(((TextBox)stackPanel.Children[1]).Text, out quantity) &&
                            int.TryParse(((TextBox)stackPanel.Children[3]).Text, out calories))
                        {
                            string unit = (unitComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                            string foodGroup = (foodGroupComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                            // Convert the quantity based on the selected unit
                            //quantity = ConvertQuantityBasedOnUnit(unit, quantity, name);

                            double convertedQuantity = 0;
                            double tempVariable = quantity;
                            string units = null;

                            if (unit == "Kg")
                            {
                                if (tempVariable < 1 && tempVariable > 0.000001)
                                {
                                    double quanSizeTemp = Math.Round((tempVariable * 1000), 2);
                                    convertedQuantity = quanSizeTemp;
                                    units = "g ";
                                }
                                else if (tempVariable >= 1)
                                {
                                    convertedQuantity = tempVariable;
                                    units = "kg ";
                                }
                                else if (tempVariable <= 0.000001)
                                {
                                    double quanSizeTemp = Math.Round((tempVariable * 1000000), 2);
                                    convertedQuantity = quanSizeTemp;
                                    units = "mg ";
                                }
                            }
                            else if (unit == "g")
                            {
                                if (tempVariable >= 1000)
                                {
                                    double quanSizeTemp = Math.Round((tempVariable / 1000), 2);
                                    convertedQuantity = quanSizeTemp;
                                    units = "Kg ";
                                }
                                else if (tempVariable < 1000 && tempVariable > 0.001)
                                {
                                    convertedQuantity = tempVariable;
                                    units = "g ";
                                }
                                else if (tempVariable <= 0.001)
                                {
                                    double quanSizeTemp = Math.Round((tempVariable * 1000), 2);
                                    convertedQuantity = quanSizeTemp;
                                    units = "mg ";
                                }
                            }
                            else if (unit == "mg")
                            {
                                if (tempVariable >= 1000)
                                {
                                    convertedQuantity = Math.Round((tempVariable / 1000), 2); // Convert mg to g
                                    units = "g ";
                                }
                                else if (tempVariable < 1000)
                                {
                                    convertedQuantity = tempVariable;
                                    // Already in milligrams, no conversion needed
                                    units = "mg ";
                                }
                            }
                            else if (unit == "L")
                            {
                                if (tempVariable < 1)
                                {
                                    double quanSizeTemp = Math.Round((tempVariable * 1000), 2);
                                    convertedQuantity = quanSizeTemp;
                                    units = "mL ";
                                }
                                else if (tempVariable >= 1)
                                {
                                    convertedQuantity = tempVariable;
                                    units = "L ";
                                }
                            }
                            else if (unit == "mL")
                            {
                                if (tempVariable >= 1000)
                                {
                                    double quanSizeTemp = Math.Round((tempVariable / 1000), 2);
                                    convertedQuantity = quanSizeTemp;
                                    units = "L ";
                                }
                                else
                                {
                                    convertedQuantity = tempVariable;
                                    units = "mL ";
                                }
                            }
                            else if (unit == "Tablespoon")
                            {
                                if (tempVariable >= 62.5)
                                {
                                    double tablespoonsToL = Math.Round((tempVariable * 16), 2);
                                    double ansInL = tablespoonsToL / 1000;
                                    convertedQuantity = ansInL;
                                    units = "L ";
                                }
                                else if (tempVariable < 62.5 && tempVariable > 1)
                                {
                                    convertedQuantity = tempVariable;
                                    units = " tablespoon/s ";
                                }
                                else if (tempVariable < 1)
                                {
                                    double tblToTea = tempVariable * 3;
                                    convertedQuantity = tblToTea;
                                    units = " teaspoon/s ";
                                }
                            }
                            else if (unit == "Teaspoon")
                            {
                                if (tempVariable >= 3)
                                {
                                    double teaspoonsToTablespoons = Math.Round((tempVariable / 3), 2);
                                    convertedQuantity = teaspoonsToTablespoons;
                                    units = " tablespoon/s ";
                                }
                                else if (tempVariable < 3 && tempVariable > 1)
                                {
                                    units = " teaspoon/s ";
                                }
                                else if (tempVariable < 1)
                                {
                                    double mlInTea = Math.Round((tempVariable * 5), 2);
                                    convertedQuantity = mlInTea;
                                    units = "ml ";
                                }
                            }
                            else if (unit == "Units")
                            {
                                if (tempVariable > 1)
                                {
                                    if (!name.EndsWith("s"))
                                    {
                                        string temp = name += "s";
                                        units = null;
                                        convertedQuantity = tempVariable;
                                       
                                    }
                                }
                                else
                                {
                                    if (name.EndsWith("s"))
                                    {
                                        name = name.Remove(name.Length - 1);
                                        units = null;
                                        convertedQuantity = tempVariable;
                                    }
                                }
                            }

                                _recipe.IngredientsList.Add(new Ingredients
                            {
                                ingredientName = name,
                                ingredientQuantity = convertedQuantity,
                                scaledQnty = convertedQuantity,
                                ingredientUnit = units,
                                scaledUnit = units,
                                OriginalCalories=calories,
                                ingredientCalorie = calories,
                                IngredientFoodGroup = foodGroup
                            });
                        }
                        else
                        {
                            MessageBox.Show("Please enter valid values for ingredient quantity and calories.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                }
            }


            // Collect step data
            foreach (var item in StepsList.Items)
            {
                var container = StepsList.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                if (container != null)
                {
                    var textBox = FindVisualChild<TextBox>(container);
                    if (textBox != null)
                    {
                        string stepDescription = textBox.Text;
                        _recipe.Steps.Add(new Step { Description = stepDescription });
                    }
                }
            }

            // Save the recipe to RecipeManager
            AddRecipeSave.AddRecipe(_recipe);

            // Display success message
            MessageBox.Show("Recipe saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            MainMenu mo = new MainMenu();
            mo.Show();
            this.Close();
        }
       
        
            private void Window_Loaded(object sender, RoutedEventArgs e) { }

        // Helper method to find a child element by type
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T childOfType)
                {
                    return childOfType;
                }
                var result = FindVisualChild<T>(child);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
        // Event handler to show the sidebar



        private void BackMenu_Click(object sender, RoutedEventArgs e)
        {
           MainMenu Mo = new MainMenu();    
            Mo.Show();
            this.Close(); 
        }

        private void ExitAddrec_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Closes the window
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainMenu Mo = new MainMenu();
            Mo.Show();
            this.Close(); 
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text == "Name" || textBox.Text == "Quantity" || textBox.Text == "Calories"|| textBox.Text == "Step Description") // Assuming these are the placeholders
                {
                    textBox.Text = "";
                    textBox.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            else
            {
                // Log or handle the case where textBox is null
                Debug.WriteLine("TextBox_GotFocus called with null sender");
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
            {
                Debug.WriteLine("TextBox_LostFocus: sender is not a TextBox or is null.");
                return; // Exit if sender is not correctly cast to TextBox
            }

            // Ensure the placeholder text is restored only if textBox is empty
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Placeholder text should be set according to the specific textbox if multiple textboxes use this handler
                switch (textBox.Name)
                {
                    case "Ingredient":
                        textBox.Text = "Name";
                        break;
                    case "quan":
                        textBox.Text = "Quantity";
                        break;
                    case "cal":
                        textBox.Text = "Calories";
                        break;
                    case "step":
                        textBox.Text = "Step Description";
                        break;
                    default:
                        Debug.WriteLine("Unexpected TextBox Name: " + textBox.Name);
                        break;
                }
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Exit exit = new Exit();
            exit.Show();
            this.Close();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mo = new MainMenu();  
            mo.Show();
            this.Close(); 
        }
    }
}