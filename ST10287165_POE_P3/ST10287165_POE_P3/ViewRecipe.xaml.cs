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
    public partial class ViewRecipe : Window
    {
        private List<Recipe> _recipes;

        public ViewRecipe()
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
            IngredientFilter.BeginStoryboard((Storyboard)FindResource("FadeInAnimation"));
            FoodGroupFilter.BeginStoryboard((Storyboard)FindResource("FadeInAnimation"));
            MaxCaloriesFilter.BeginStoryboard((Storyboard)FindResource("FadeInAnimation"));
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Hide all search fields initially
            IngredientFilter.Visibility = Visibility.Collapsed;
            FoodGroupFilter.Visibility = Visibility.Collapsed;
            MaxCaloriesFilter.Visibility = Visibility.Collapsed;

            // Show the relevant search field based on the selected radio button
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
                // Populate RichTextBox with recipe details up to ingredients
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
        }
        
        public class StepItem
        {
            public string Description { get; set; }
            public bool IsChecked { get; set; }  // This property will be used for binding the checkbox state
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



        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mo = new MainMenu();
            mo.Show();
            this.Close();
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

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Exit mo = new Exit();
            mo.Show();
            this.Close();
        }
    }
}