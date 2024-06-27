using ST10287165_POE_P3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10287165_POE_P3
{
    public class Recipe
    {
        public string Name { get; set; }
        public double AmtServe { get; set; }
        public List<Ingredients> IngredientsList { get; set; }  // List of ingredients
        public List<Step> Steps { get; set; }
        public List<ScalingRecipe> scaleOrig { get; set; }
        public double OriginalServingSize { get; set; }


        //this is our constructor which is used to initilize these steps         
        //i have decided to put the lists here so that all information for a praticual recipe will be saves inder one geneirc list
        // each recipe will have its own index. 
        // i will implement an if statemet for the user not to be able to save more than one recipe.
        //by me doing this, i have made provisons for part 2 of the poe as i can just remove the if statment which restricts the user.
        public Recipe()
        {
            IngredientsList = new List<Ingredients>();
            Steps = new List<Step>();
            // scaleOrig = new List<ScalingRecipe>();
        }
    }
}
//done
