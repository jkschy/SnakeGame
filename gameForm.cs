using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        Settings settings = new Settings();
        private List<Snake> snake = new List<Snake>();
        private Snake food = new Snake();
        Timer timer = new Timer();

        Brush snakeColor = Brushes.Black;
        Brush foodColor = Brushes.Red;


        private void GameForm_Load(object sender, EventArgs e)
        {
            timer.Interval = 1000 / settings.Speed;
            timer.Tick += updateScreen;
            
            startGame();
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            int index = 1;
            foreach(Snake s in snake) { //Draws each item inside of the snake array list
                if(index == 1)
                {
                    snakeColor = Brushes.Black;
                } else
                {
                    snakeColor = Brushes.Green;
                }
                canvas.FillEllipse(snakeColor, new Rectangle(s.X * settings.Width, s.Y * settings.Height, settings.Width, settings.Height));
                index++;
            }
            canvas.FillEllipse(foodColor, new Rectangle(food.X * settings.Width, food.Y * settings.Width, settings.Width, settings.Height));
        }
        private void updateScreen(object sender, EventArgs e)
        {
            if (settings.GameOver != true)
            {
                movePlayer();
                PbCanvas.Invalidate();
                if ((snake[0].X == food.X) && (snake[0].Y == food.Y))
                    Eat();
            }else
            {
                label1.Text = "Game Over! \n" + "Final Score: " + settings.Score + "\nPress Enter to Restart \n";
                label1.Visible = true;
            }
        }
        public void movePlayer()
        {
            for (int i = snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    //changes direction of head
                    switch (settings.direction)
                    {
                        case Settings.Direction.Right:
                            snake[i].X++;
                            break;
                        case Settings.Direction.Left:
                            snake[i].X--;
                            break;
                        case Settings.Direction.Up:
                            snake[i].Y--;
                            break;
                        case Settings.Direction.Down:
                            snake[i].Y++;
                            break;
                    }

                    int maxXpos = PbCanvas.Size.Width / settings.Width;
                    int maxYpos = PbCanvas.Size.Height / settings.Height;

                    if((snake[i].X < 0) || (snake[i].Y < 0) || (snake[i].X > maxXpos) || (snake[i].Y > maxYpos))
                    {
                        Die();
                    }
                    for (int j = 1; j < snake.Count; j++)
                    {
                        if ((snake[i].X == snake[j].X) && (snake[i].Y == snake[j].Y) && (snake.Count != 1))
                        {
                            Die();
                        }
                    }

                } else
                {
                    //Moves the body to the spot ahead of it
                    snake[i].X = snake[i - 1].X;
                    snake[i].Y = snake[i - 1].Y;
                }
            }
        }
        private void startGame()
        {
            snake.Clear();
            settings.Score = 0;
            label1.Text = "Score: " + settings.Score;
            settings.GameOver = false;
            snake.Add(new Snake { X = 10, Y = 5});
            generateFood();
        }
        private void generateFood()
        {
            int maxXpos = PbCanvas.Size.Width / settings.Width;
            //creates max X value for food
            int maxYpos = PbCanvas.Size.Height / settings.Height;
            //creates max Y value for food
            Random ObjR = new Random();
            food = new Snake { X = ObjR.Next(0, maxXpos), Y = ObjR.Next(0, maxYpos) }; 
        }
        private void Eat()
        {
            Snake body = new Snake()
            {
                X = snake[snake.Count - 1].X,
                Y = snake[snake.Count - 1].Y
            };

            snake.Add(body);
            generateFood();
            settings.Score += settings.points;
            settings.Speed += 1;
            scoreLabel.Text = "Score: " + settings.Score.ToString();
        }
        private void Die()
        {
            settings.GameOver = true;
        }
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.Left) && (settings.direction != Settings.Direction.Right))
            {
                settings.direction = Settings.Direction.Left;
            }
            if ((e.KeyCode == Keys.Right) && (settings.direction != Settings.Direction.Left))
            {
                settings.direction = Settings.Direction.Right;
            }
            if ((e.KeyCode == Keys.Up) && (settings.direction != Settings.Direction.Down))
            {
                settings.direction = Settings.Direction.Up;
            }
            if ((e.KeyCode == Keys.Down) && (settings.direction != Settings.Direction.Up))
            {
                settings.direction = Settings.Direction.Down;
            }
            if(timer.Enabled == false)
            {
                timer.Start();
            }
            if((settings.GameOver == true) && (e.KeyCode == Keys.Enter))
            {
                startGame();
            }
        }
    }
}
