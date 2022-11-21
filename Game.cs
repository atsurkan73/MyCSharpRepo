using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame;

public class Game
{ 
    public Snake Snake;
    
    public Point StartPoint = new Point(0, 0);
    public List<Point> Points = new List<Point>();
    public Point EggPoint = new Point(0, 0);
    public Egg Egg;


    Direction CurrentDirection;
    Direction NextDirection;

    public Game()
    {
        Snake = new Snake(StartPoint, 3);
        EggPoint = EggPoint.GenerateEggLocation();
        Egg = new Egg(EggPoint).GenerateEgg();
        CurrentDirection = Direction.Right;
        NextDirection = Direction.Right;
    }


    public bool IsGameOver => !Snake.isAlive;

    public void OnKeyPress(ConsoleKey key)
    {
        Direction newDirection;
        Point NewPoint;
       
        switch (key)
        {
            case ConsoleKey.LeftArrow:
                newDirection = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                newDirection = Direction.Right;
                break;
            case ConsoleKey.UpArrow:
                newDirection = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                 newDirection = Direction.Down;
                break; 
            default:
                Console.WriteLine("Use Arrow kes");
                return;
        }
        
        if (newDirection == OppositeDirection(CurrentDirection))
            return;
        
        NextDirection = newDirection;
    }

    public Direction OppositeDirection(Direction direction)
    { 
        return direction switch
            {
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            _ => throw new Exception()
            };
    }

        public void Render()
    {
        Console.Clear();
        Snake.Render();
        Egg.Render();
        Console.SetCursorPosition(0,0);
    }

    public void OnTick()
    {
        if (IsGameOver) throw new Exception();
        CurrentDirection  = NextDirection;
        Snake.Move(CurrentDirection);
        if (Snake.Head.Equals(Egg.Point))
            {
            Snake.Grow();
            Egg.GenerateEgg();
            }
    }
}
