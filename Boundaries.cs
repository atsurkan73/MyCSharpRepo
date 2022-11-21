using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{

    public record Boundaries 
    {
        public int Rows { get; set; } 
        public int Columns { get; set; }

        
        public  Boundaries(int rows, int columns)
        {
           Rows = rows;
           Columns = columns;
        }
    }
}
