using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Settings
    {

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }   


        public int Width { get; set; }
        public int Height { get; set; }
        public int Speed { get; set; }
        public int Score { get; set; }
        public int points { get; set; }
        public bool GameOver { get; set; }
        public Direction direction { get; set; }

        public Settings()
        {
            Width = 16;
            Height = 16;
            Speed = 20;
            Score = 0;
            points = 100;
            GameOver = false;
            direction = Direction.Down;
        }



    }
}
