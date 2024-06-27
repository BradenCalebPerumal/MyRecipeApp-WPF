using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10287165_POE_P3
{
    // This class represents a single step in a process or lis
    public class Step
    {
        // This is what you need to do in this step.
        public string Description { get; set; }

        // Keeps track if the step is checked off or not
        public bool IsChecked { get; set; }
    }
}
// 
