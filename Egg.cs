using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{

    public class Egg
    {
        public Point Point { get; set; }
        public Egg(Point point)
        {
            Point = point;
        }

        Boundaries boundaries = new Boundaries(20,20);

        public void Render()
        {
            Console.SetCursorPosition(Point.X, Point.Y);
            Console.Write("*");
        }
        
        public Egg GenerateEgg()
        {
            var rows = boundaries.Rows;
            var columns  = boundaries.Columns;
            var random = new Random();
            var y = random.Next(0, rows+1);
            var x = random.Next(0, columns+1);
            Point = new Point(x, y);
            return new Egg(Point);
        }
    }
}
