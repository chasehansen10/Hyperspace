using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperspace
{
    class Program
    {
        static void Main(string[] args)
        {
            Hyperspace game = new Hyperspace();
            game.PlayGame();
        }
    }
    class Unit
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public string Symbol { get; set; }
        public bool IsSpaceRift { get; set; }
        public static List<string> ObstacleList = new List<string> { "*", "!", "::", ",", ";", ":" };
        public Unit(int x,int y)
        {
            Random rng=new Random();
            this.Color = ConsoleColor.Cyan;
            this.Symbol = ObstacleList[rng.Next(0, ObstacleList.Count())];
            this.X = x;
            this.Y = y;

        }
        public Unit(int x, int y, ConsoleColor color, string symbol, bool IsSaceRift)
        {

            Random rng = new Random();
            this.Color = color;
            this.Symbol = symbol;
            this.X = x;
            this.Y = y;
            this.IsSpaceRift = IsSaceRift;


        }
        
        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
            
        }

    }
    class Hyperspace
    {
        public int Score { get; set; }
        public int Speed { get; set; }
        public List<Unit> ObstacleList { get; set; }
        public Unit SpaceShip { get; set; }
        public bool Smashed { get; set; }
        private Random rng = new Random();
        public Hyperspace()
        {
            Console.BufferHeight = Console.WindowHeight;
            Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth;
            Console.WindowWidth = 60;
            this.Score = 0;
            this.Speed = 0;
            this.ObstacleList = new List<Unit>();
            this.SpaceShip = new Unit((Console.WindowWidth / 2) - 1, Console.WindowHeight - 1, ConsoleColor.Blue, "@", false);
            

        }
        public void PlayGame()
        {
            while (Smashed == false)
            {
                
                if(rng.Next(0,11)>9)
                {

                    Unit SpaceRift = new Unit(rng.Next(0, Console.WindowWidth - 2), 5, ConsoleColor.Green,"%",true);
                    ObstacleList.Add(SpaceRift);
                }
                else
                {
                    Unit Obstacle = new Unit(rng.Next(0, Console.WindowWidth - 2), 5);
                    ObstacleList.Add(Obstacle);
                }
                MoveShip();
                MoveObstacles();
                DrawGame();
                if(Speed<170)
                {
                    Speed++;
                }
                System.Threading.Thread.Sleep(170 - Speed);

            }
        }
        public void MoveShip()
        {
            if(Console.KeyAvailable)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey();
                while (Console.KeyAvailable) { Console.ReadKey(true); }
                if((keyPressed.Key==ConsoleKey.LeftArrow)&&SpaceShip.X>0)
                {
                    SpaceShip.X--;
                }
                if((keyPressed.Key==ConsoleKey.RightArrow)&&SpaceShip.X<Console.WindowWidth-2)
                {
                    SpaceShip.X++;
                }
            }
        }
        public void MoveObstacles()
        {
            List<Unit> newObstacleList = new List<Unit>();
            foreach(Unit item in ObstacleList)
            {
                item.Y++;
                if((item.IsSpaceRift)&&item.X==SpaceShip.X&&item.Y==SpaceShip.Y)
                {
                    Speed -= 50;
                }
                else if(!item.IsSpaceRift&&item.X==SpaceShip.X&&item.Y==SpaceShip.Y)
                {
                    Smashed = true;
                }
                else if(item.Y<Console.WindowHeight)
                {
                    newObstacleList.Add(item);
                }
                else
                {
                    Score++;
                }
            }
            ObstacleList = newObstacleList;
        }
        public void DrawGame()
        {
            Console.Clear();
            SpaceShip.Draw();
            foreach(Unit item in ObstacleList)
            {
                item.Draw();
            }
            PrintAtPosition(20, 2, "Score:" + this.Score, ConsoleColor.Green);
            PrintAtPosition(20, 3, "Speed:" + this.Speed, ConsoleColor.Green);
        }
        public void PrintAtPosition(int x, int y,string text, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
        }
    }
}
