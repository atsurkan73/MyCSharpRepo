using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{

    public class Snake
    {
        public List<Point> WholeBody = new List<Point>();
        public Point Head => WholeBody.First();
        public IEnumerable <Point> Body => WholeBody.Skip(1).ToList();
        public int GrowthDelta;
        public bool isAlive { get; set; } = true;

        public Snake(Point position, int length = 1)
        {
            WholeBody = new List<Point> { position };
            GrowthDelta = Math.Max(0, length - 1);
        }

        public void Move (Direction direction)

        {
            if (!isAlive) throw new Exception("");
            Point newHead = direction switch
            {
                Direction.Right => Head.RightBy(1),
                Direction.Left => Head.RightBy(-1),
                Direction.Up => Head.DownBy(-1),
                Direction.Down => Head.DownBy(1),
                _ => throw new Exception(""),
            };
            if (WholeBody.Contains(newHead) || !IsInBoundaries(newHead))
            {
                isAlive = false;
                return;
            }

            WholeBody.Insert(0, newHead);

            if (GrowthDelta > 0)   GrowthDelta--;
            else WholeBody.RemoveAt(WholeBody.Count - 1);
        }

        public void Grow()
        {
            if (!isAlive) throw new Exception("");
            GrowthDelta++;
        }

        public void Render()
        {
            Console.SetCursorPosition(Head.X, Head.Y);
            Console.Write("*");
            foreach (var index in Body)
            {
                Console.SetCursorPosition(index.X, index.Y);
                Console.Write("o");
            }
        }

        public bool IsInBoundaries(Point pnt) =>
            pnt.X >= 0 && pnt.Y >= 0;

    }
}
