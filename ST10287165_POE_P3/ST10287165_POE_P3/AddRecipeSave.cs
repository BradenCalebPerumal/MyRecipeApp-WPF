using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10287165_POE_P3
{
    // Defines a class to manage recipe storage.
    internal class AddRecipeSave
    {
        // Static list to hold recipes, accessible throughout the application.
        public static List<Recipe> Recipes { get; private set; } = new List<Recipe>();

        // Method to add a recipe to the static list.
        public static void AddRecipe(Recipe recipe)
        {
            // Add the new recipe to the collection.
            Recipes.Add(recipe);
        }
    }
}
