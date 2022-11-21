using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point (int x, int y)
    { 
        X = x;
        Y = y;
    }

    Boundaries boundaries = new Boundaries(20, 20);

    public Point DownBy(int n) => new Point(X, Y + n);
    public Point RightBy(int n) => new Point(X + n, Y);


    public Point GenerateEggLocation()
    {
        var rows = boundaries.Rows;
        var columns = boundaries.Columns;
        var random = new Random();
        var y = random.Next(0, rows + 1);
        var x = random.Next(0, columns + 1);
        return new Point(x, y);
    }
}

