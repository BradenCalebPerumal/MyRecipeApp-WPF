using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10287165_POE_P3
{
    // This class keeps track of steps in a recipe.
    public class RecipeStep : INotifyPropertyChanged
    {
        // Holds if the step is done or not
        private bool _isCompleted;

        // What you need to do in this step
        public string Description { get; set; }

        // Checks if the step is done. Tells the app to update stuff if it changes.
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        // This thingy tells the program that something changed
        public event PropertyChangedEventHandler PropertyChanged;

        // Tells whatever needs to know that a property changed
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
