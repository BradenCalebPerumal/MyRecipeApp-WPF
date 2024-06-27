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
    public partial class DeleteRecipe : Window
    {
        private List<Recipe> _recipes;

        public DeleteRecipe()
        {
            InitializeComponent();

            // Retrieve the recipes from RecipeManager
            _recipes = AddRecipeSave.Recipes;
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Apply a fade-in animation to each of the search fields when the window is loaded.
            IngredientFilter.BeginStoryboard((Storyboard)FindResource("FadeInAnimation"));
            FoodGroupFilter.BeginStoryboard((Storyboard)FindResource("FadeInAnimation"));
            MaxCaloriesFilter.BeginStoryboard((Storyboard)FindResource("FadeInAnimation"));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Hide all search fields initially to ensure only the relevant one is shown.
            IngredientFilter.Visibility = Visibility.Collapsed;
            FoodGroupFilter.Visibility = Visibility.Collapsed;
            MaxCaloriesFilter.Visibility = Visibility.Collapsed;

            // Determine which search field to display based on which radio button is checked.
            if (IngredientRadioButton.IsChecked == true)
            {
                // Show the ingredient filter if the corresponding radio button is selected.
                IngredientFilter.Visibility = Visibility.Visible;
            }
            else if (FoodGroupRadioButton.IsChecked == true)
            {
                // Show the food group filter if its radio button is selected.
                FoodGroupFilter.Visibility = Visibility.Visible;
            }
            else if (CaloriesRadioButton.IsChecked == true)
            {
                // Show the calories filter if this radio button is selected.
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
            // Exit if no recipe is selected
            if (RecipeList.SelectedItem == null) return;

            // Get the name of the selected recipe
            string selectedRecipeName = RecipeList.SelectedItem.ToString();
            // Find the selected recipe in the list of recipes
            var selectedRecipe = _recipes.FirstOrDefault(recipe => recipe.Name == selectedRecipeName);

            // If the recipe is found
            if (selectedRecipe != null)
            {
                // Create a new FlowDocument to display the recipe details
                FlowDocument doc = new FlowDocument();
                int totalCalories = 0;

                // Add the recipe name to the document
                doc.Blocks.Add(new Paragraph(new Run($"Recipe Name: {selectedRecipe.Name.ToUpper()}")));
                // Add the serving size to the document
                doc.Blocks.Add(new Paragraph(new Run($"Serving Size: {selectedRecipe.AmtServe}")));

                // Add a separator line to the document
                Paragraph line = new Paragraph(new Run("-------------------------------------------------------------------------------"));
                line.Foreground = new SolidColorBrush(Colors.Black);
                doc.Blocks.Add(line);

                // Add the ingredients section header to the document
                doc.Blocks.Add(new Paragraph(new Run($"Ingredients ({selectedRecipe.IngredientsList.Count}):")));

                // Loop through each ingredient in the recipe
                foreach (var ingredient in selectedRecipe.IngredientsList)
                {
                    // Accumulate the total calories
                    totalCalories += ingredient.ingredientCalorie;
                    // Add each ingredient to the document
                    doc.Blocks.Add(new Paragraph(new Run($">> {ingredient.ingredientQuantity}{ingredient.ingredientUnit} {ingredient.ingredientName}   (Food Group:{ingredient.IngredientFoodGroup})")));
                }

                // Add the total calories to the document
                Paragraph calorieInfo = new Paragraph(new Run($"Total Calories: {totalCalories}"));
                doc.Blocks.Add(calorieInfo);

                // Add the preparation steps section header to the document
                doc.Blocks.Add(new Paragraph(new Run($"Preparation Steps ({selectedRecipe.Steps.Count}):")));

                // Loop through each step in the recipe and add to the document
                for (int i = 0; i < selectedRecipe.Steps.Count; i++)
                {
                    doc.Blocks.Add(new Paragraph(new Run($"{i + 1}. {selectedRecipe.Steps[i].Description}")));
                }

                // Add a warning if the total calories exceed 300
                if (totalCalories > 300)
                {
                    Paragraph warning = new Paragraph(new Run("Warning: This recipe contains high calories. Calories total exceeded 300!"));
                    warning.Foreground = new SolidColorBrush(Colors.Red);
                    doc.Blocks.Add(warning);
                }
                else
                {
                    // Add a healthy message if the total calories are within a healthy range
                    Paragraph healthyMessage = new Paragraph(new Run("This recipe is within a healthy calorie range."));
                    healthyMessage.Foreground = new SolidColorBrush(Colors.Green);  // Set text color to green
                    doc.Blocks.Add(healthyMessage);
                }

                // Set the document to the RichTextBox to display the recipe details
                RecipeDetails.Document = doc;
            }
        }



        private void RecipeDetails_TextChanged(object sender, TextChangedEventArgs e)
        {

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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeList.SelectedItem == null)
            {
                MessageBox.Show("Please select a recipe to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedRecipeName = RecipeList.SelectedItem.ToString();
            var selectedRecipe = _recipes.FirstOrDefault(recipe => recipe.Name == selectedRecipeName);

            if (selectedRecipe != null)
            {
                // Confirm deletion
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the recipe '{selectedRecipe.Name}' permanently?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _recipes.Remove(selectedRecipe); // Remove from the list

                    // Update the list display
                    UpdateRecipeList();

                    // Clear the details panel
                    RecipeDetails.Document.Blocks.Clear();

                    MessageBox.Show("Recipe deleted successfully.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mo= new MainMenu();
            mo.Show(); 
            this.Close();   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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