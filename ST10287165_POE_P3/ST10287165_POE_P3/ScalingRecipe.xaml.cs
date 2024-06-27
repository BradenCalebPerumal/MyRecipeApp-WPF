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
    public partial class ScalingRecipe : Window
    {
        private List<Recipe> _recipes;

        public ScalingRecipe()
        {
            InitializeComponent();
            _recipes = AddRecipeSave.Recipes;  // Assume AddRecipeSave.Recipes provides the list of recipes
            UpdateRecipeList();
        }

        private void UpdateRecipeList()
        {
            RecipeList.Items.Clear();
            foreach (var recipe in _recipes.OrderBy(r => r.Name))
            {
                RecipeList.Items.Add(recipe.Name);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            IngredientFilter.Visibility = Visibility.Collapsed;
            FoodGroupFilter.Visibility = Visibility.Collapsed;
            MaxCaloriesFilter.Visibility = Visibility.Collapsed;

            if (IngredientRadioButton.IsChecked == true)
            {
                IngredientFilter.Visibility = Visibility.Visible;
            }
            else if (FoodGroupRadioButton.IsChecked == true)
            {
                FoodGroupFilter.Visibility = Visibility.Visible;
            }
            else if (CaloriesRadioButton.IsChecked == true)
            {
                MaxCaloriesFilter.Visibility = Visibility.Visible;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Recipe> filteredRecipes = _recipes;

            if (IngredientRadioButton.IsChecked == true)
            {
                string ingredientFilter = IngredientFilter.Text.ToLower();
                filteredRecipes = filteredRecipes.Where(recipe =>
                    recipe.IngredientsList.Any(ingredient => ingredient.ingredientName.ToLower().Contains(ingredientFilter)));
            }
            else if (FoodGroupRadioButton.IsChecked == true)
            {
                // Adjusted to work with ComboBox
                if (FoodGroupFilter.SelectedItem != null)
                {
                    string foodGroupFilter = ((ComboBoxItem)FoodGroupFilter.SelectedItem).Content.ToString().ToLower();
                    filteredRecipes = filteredRecipes.Where(recipe =>
                        recipe.IngredientsList.Any(ingredient => ingredient.IngredientFoodGroup.ToLower().Contains(foodGroupFilter)));
                }
            }
            else if (CaloriesRadioButton.IsChecked == true)
            {
                if (int.TryParse(MaxCaloriesFilter.Text, out int maxCalories))
                {
                    filteredRecipes = filteredRecipes.Where(recipe =>
                        recipe.IngredientsList.Sum(ingredient => ingredient.ingredientCalorie) <= maxCalories);
                }
                else
                {
                    MessageBox.Show("Please enter a valid number for maximum calories.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            RecipeList.Items.Clear();

            // Check if there are any recipes found
            if (!filteredRecipes.Any())
            {
                MessageBox.Show("No recipes found with the specified criteria.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                foreach (var recipe in filteredRecipes.OrderBy(r => r.Name))
                {
                    RecipeList.Items.Add(recipe.Name);
                }
            }
        }


        private void RecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeList.SelectedItem == null) return;

            string selectedRecipeName = RecipeList.SelectedItem.ToString();
            var selectedRecipe = _recipes.FirstOrDefault(recipe => recipe.Name == selectedRecipeName);

            if (selectedRecipe != null)
            {
                RefreshRecipeDetails(selectedRecipe);
            }
        }

        private void ScaleButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeList.SelectedItem == null) return;

            string selectedRecipeName = RecipeList.SelectedItem.ToString();
            var selectedRecipe = _recipes.FirstOrDefault(recipe => recipe.Name == selectedRecipeName);

            if (selectedRecipe != null)
            {
                ComboBoxItem selectedScaleItem = (ComboBoxItem)ScalingComboBox.SelectedItem;
                if (selectedScaleItem != null)
                {
                    string selectedScale = selectedScaleItem.Content.ToString().Replace(',', '.'); // Ensure the correct decimal separator
                    if (double.TryParse(selectedScale, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out double scaleValue))
                    {
                        ScaleRecipe(selectedRecipe, scaleValue);
                        RefreshRecipeDetails(selectedRecipe);
                    }
                    else
                    {
                        MessageBox.Show("Invalid scaling factor selected.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
 private void ScaleRecipe(Recipe recipe, double factorMult)
{
    foreach (Ingredients ingredient in recipe.IngredientsList)
    {
        ingredient.ingredientQuantity = Math.Round(ingredient.ingredientQuantity * factorMult, 2);
        ingredient.ingredientCalorie = (int)(ingredient.ingredientCalorie * factorMult);

        if (ingredient.ingredientUnit != null)
        {
            // Convert kilograms to grams
            if (ingredient.ingredientUnit == "Kg")
            {
                if (ingredient.ingredientQuantity < 1)
                {
                    ingredient.ingredientQuantity = Math.Round(ingredient.ingredientQuantity * 1000, 2);
                    ingredient.ingredientUnit = "g";
                }
            }
            // Convert grams to kilograms or milligrams
            else if (ingredient.ingredientUnit == "g")
            {
                if (ingredient.ingredientQuantity >= 1000)
                {
                    ingredient.ingredientQuantity = Math.Round(ingredient.ingredientQuantity / 1000, 2);
                    ingredient.ingredientUnit = "Kg";
                }
                else if (ingredient.ingredientQuantity < 1 && ingredient.ingredientQuantity >= 0.001)
                {
                    ingredient.ingredientQuantity = Math.Round(ingredient.ingredientQuantity * 1000, 2);
                    ingredient.ingredientUnit = "mg";
                }
            }
            // Convert milligrams to grams
            else if (ingredient.ingredientUnit == "mg")
            {
                if (ingredient.ingredientQuantity >= 1000)
                {
                    ingredient.ingredientQuantity = Math.Round(ingredient.ingredientQuantity / 1000, 2);
                    ingredient.ingredientUnit = "g";
                }
            }

            // Convert liters to milliliters
            if (ingredient.ingredientUnit == "L")
            {
                if (ingredient.ingredientQuantity < 1)
                {
                    ingredient.ingredientQuantity = Math.Round(ingredient.ingredientQuantity * 1000, 2);
                    ingredient.ingredientUnit = "mL";
                }
            }
            // Convert milliliters to liters or microliters
            else if (ingredient.ingredientUnit == "mL")
            {
                if (ingredient.ingredientQuantity >= 1000)
                {
                    ingredient.ingredientQuantity = Math.Round(ingredient.ingredientQuantity / 1000, 2);
                    ingredient.ingredientUnit = "L";
                }
                else if (ingredient.ingredientQuantity < 1 && ingredient.ingredientQuantity >= 0.001)
                {
                    ingredient.ingredientQuantity = Math.Round(ingredient.ingredientQuantity * 1000, 2);
                    ingredient.ingredientUnit = "μL";
                }
            }
            // Convert microliters to milliliters
            else if (ingredient.ingredientUnit == "μL")
            {
                if (ingredient.ingredientQuantity >= 1000)
                {
                    ingredient.ingredientQuantity = Math.Round(ingredient.ingredientQuantity / 1000, 2);
                    ingredient.ingredientUnit = "mL";
                }
            }
        }
    }
    recipe.AmtServe = Math.Round(recipe.AmtServe * factorMult, 2);
}

        private void ResetScaleButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeList.SelectedItem == null) return;

            string selectedRecipeName = RecipeList.SelectedItem.ToString();
            var selectedRecipe = _recipes.FirstOrDefault(recipe => recipe.Name == selectedRecipeName);

            if (selectedRecipe != null)
            {
                ResetQuantities(selectedRecipe);
                RefreshRecipeDetails(selectedRecipe);
            }
        }

        private void ResetQuantities(Recipe recipe)
        {
            if (recipe == null || recipe.IngredientsList == null)
                return;

            foreach (var ingredient in recipe.IngredientsList)
            {
                if (ingredient != null)
                {
                    // Ensure that scaledQnty, scaledUnit, and OriginalCalories have valid values
                    if (ingredient.scaledQnty != 0)
                        ingredient.ingredientQuantity = ingredient.scaledQnty;
                    if (!string.IsNullOrEmpty(ingredient.scaledUnit))
                        ingredient.ingredientUnit = ingredient.scaledUnit;
                    if (ingredient.OriginalCalories != 0)
                        ingredient.ingredientCalorie = ingredient.OriginalCalories;
                }
            }
            recipe.AmtServe = recipe.OriginalServingSize;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeList.SelectedItem == null) return;

            string selectedRecipeName = RecipeList.SelectedItem.ToString();
            var selectedRecipe = _recipes.FirstOrDefault(recipe => recipe.Name == selectedRecipeName);

            if (selectedRecipe != null)
            {
                RefreshRecipeDetails(selectedRecipe);
            }
        }

        private void RefreshRecipeDetails(Recipe selectedRecipe)
        {
            FlowDocument doc = new FlowDocument();
            int totalCalories = 0;

            doc.Blocks.Add(new Paragraph(new Run($"Recipe Name: {selectedRecipe.Name.ToUpper()}")));
            doc.Blocks.Add(new Paragraph(new Run($"Serving Size: {selectedRecipe.AmtServe}")));

            Paragraph line = new Paragraph(new Run("-------------------------------------------------------------------------------"));
            line.Foreground = new SolidColorBrush(Colors.Black);
            doc.Blocks.Add(line);

            doc.Blocks.Add(new Paragraph(new Run($"Ingredients ({selectedRecipe.IngredientsList.Count}):")));
            foreach (var ingredient in selectedRecipe.IngredientsList)
            {
                totalCalories += ingredient.ingredientCalorie;
                doc.Blocks.Add(new Paragraph(new Run($">> {ingredient.ingredientQuantity}{ingredient.ingredientUnit} {ingredient.ingredientName} (Food Group: {ingredient.IngredientFoodGroup})")));
            }

            Paragraph calorieInfo = new Paragraph(new Run($"Total Calories: {totalCalories}"));
            doc.Blocks.Add(calorieInfo);

            if (totalCalories > 300)
            {
                Paragraph warning = new Paragraph(new Run("Warning: This recipe contains high calories. Calories total exceeded 300!"));
                warning.Foreground = new SolidColorBrush(Colors.Red);
                doc.Blocks.Add(warning);
            }
            else
            {
                Paragraph healthyMessage = new Paragraph(new Run("This recipe is within a healthy calorie range."));
                healthyMessage.Foreground = new SolidColorBrush(Colors.Green);
                doc.Blocks.Add(healthyMessage);
            }

            RecipeDetails.Document = doc;

            // Populate ListView with steps using the StepItem class
            var stepDetails = selectedRecipe.Steps.Select(step => new StepItem
            {
                Description = step.Description,
                IsChecked = false  // default unchecked
            }).ToList();

            StepsListView.ItemsSource = stepDetails;
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the recipes from RecipeManager
            _recipes = AddRecipeSave.Recipes;

            // Sort the recipes alphabetically
            _recipes.Sort((x, y) => x.Name.CompareTo(y.Name));

            // Update the list display
            UpdateRecipeList();

            // Clear any search filters
            IngredientFilter.Text = string.Empty;
            FoodGroupFilter.Text = string.Empty;
            MaxCaloriesFilter.Text = string.Empty;
        }


        public class StepItem
        {
            public string Description { get; set; }
            public bool IsChecked { get; set; }  // This property will be used for binding the checkbox state
        }

      

        private void exit_Click(object sender, RoutedEventArgs e)
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
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                // Clear the placeholder text when the TextBox gains focus
                if (textBox.Text == "Filter by Ingredient" || textBox.Text == "Filter by Max Calories")
                {
                    textBox.Text = string.Empty;
                    textBox.Foreground = new SolidColorBrush(Colors.Black); // Optionally, set the text color to black or any appropriate color
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                // Restore the placeholder text if the TextBox is empty when it loses focus
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = textBox.Name.Contains("Ingredient") ? "Filter by Ingredient" : "Filter by Max Calories";
                    textBox.Foreground = new SolidColorBrush(Colors.Gray); // Optionally, set the text color to gray or any placeholder color
                }
            }
        }



    }
}