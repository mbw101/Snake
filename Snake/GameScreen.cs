using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Snake
{
    public partial class GameScreen : UserControl
    {
        // TODO: Work on pause screen
        SolidBrush sb = new SolidBrush(Color.White);
        Font textFont;
        List<SnakeComponent> snake = new List<SnakeComponent>();
        Food f;
        Random random = new Random();
        const int SNAKE_SPEED = 24;
        const int MARGIN = 3;
        public const int PART_SIZE = 24;
        const int FOOD_SIZE = 16;
        public static int score = 0;
        bool leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;

        public GameScreen()
        {
            InitializeComponent();

            OnStart();
        }

        public void OnStart()
        {
            // reset score
            score = 0;

            // create text graphics
            textFont = new Font("Verdana", 14, FontStyle.Regular);

            // make snake parts
            SnakeComponent sc = new SnakeComponent(65, 65, PART_SIZE, SNAKE_SPEED, 0);
            snake.Add(sc);          

            // make the first food
            makeFood();
        }

        public void StopGame()
        {
            gameTimer.Stop();

            Thread.Sleep(2000);

            // Remove game screen and add lose screen
            Form f = this.FindForm();
            f.Controls.Remove(this);

            LoseScreen ls = new LoseScreen();
            f.Controls.Add(ls);
        }

        public void moveSnake()
        {
            int tmpX = snake[0].rect.X;
            int tmpY = snake[0].rect.Y;

            // go from the back to the front 
            // and up the back body part
            // to the next part's location

            // the head is the only one that is having a new location
            // mvoe head position first
            snake[0].Move();

            // start from the back and move up list
            for (int i = snake.Count() - 1; i >0; i--)
            {
                snake[i].rect.X = snake[i - 1].rect.X;
                snake[i].rect.Y = snake[i - 1].rect.Y;
            }

            // only check if the snake has a head and tail
            if (snake.Count >= 2)
            {
                // move the one after the head to the old head's position
                snake[1].rect.X = tmpX;
                snake[1].rect.Y = tmpY;
            }
        }

        public void makeFood()
        {
            f = new Food(random.Next(0, 769), random.Next(0, 546), FOOD_SIZE);

            // TODO: Make food not generate on top of any snake body parts
            foreach (SnakeComponent s in snake)
            {
                while (s.Collision(f))
                {
                    f = new Food(random.Next(5, 769), random.Next(5, 546), FOOD_SIZE);
                }
            }              
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            moveSnake();

            #region movement controls

            foreach (SnakeComponent sc in snake)
            {
                // Check to make sure that you can't move into the snake (ie can't move down when snake is moving up)
                if (upArrowDown)
                {
                    // only allow the snake to move up if it isn't moving down
                    if (sc.ySpeed != SNAKE_SPEED)
                    {
                        sc.ySpeed = -SNAKE_SPEED;
                        sc.xSpeed = 0;
                    }
                }
                else if (downArrowDown)
                {
                    // only allow the snake to move down if it isn't moving up
                    if (sc.ySpeed != -SNAKE_SPEED)
                    {
                        sc.ySpeed = SNAKE_SPEED;
                        sc.xSpeed = 0;
                    }
                }
                else if (leftArrowDown)
                {
                    // only allow snake to move left if it isn't moving right
                    if (sc.xSpeed != SNAKE_SPEED)
                    {
                        sc.xSpeed = -SNAKE_SPEED;
                        sc.ySpeed = 0;
                    }
                }
                else if (rightArrowDown)
                {
                    // only allow snake to move right if it isn't moving left
                    if (sc.xSpeed != -SNAKE_SPEED)
                    {
                        sc.xSpeed = SNAKE_SPEED;
                        sc.ySpeed = 0;
                    }
                }
            }

            #endregion

            #region Collision

            // check collision with window
            if (snake[0].Collision(this.FindForm()))
            {
                StopGame();
            }

            // when the head collides with food
            if (snake[0].Collision(f))
            {
                // increase score
                score++;

                // make new food
                makeFood();

                int xSpeed = snake[snake.Count() - 1].xSpeed;
                int ySpeed = snake[snake.Count() - 1].ySpeed;
                SnakeComponent sc;


                if (xSpeed == SNAKE_SPEED)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        sc = new SnakeComponent(snake[snake.Count() - 1].rect.X - PART_SIZE, snake[snake.Count() - 1].rect.Y, PART_SIZE, xSpeed, ySpeed);
                        snake.Add(sc);
                    }
                }
                else if (xSpeed == -SNAKE_SPEED)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        sc = new SnakeComponent(snake[snake.Count() - 1].rect.X + PART_SIZE, snake[snake.Count() - 1].rect.Y, PART_SIZE, xSpeed, ySpeed);
                        snake.Add(sc);
                    }
                }
                else if (ySpeed == SNAKE_SPEED)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        sc = new SnakeComponent(snake[snake.Count() - 1].rect.X, snake[snake.Count() - 1].rect.Y - PART_SIZE, PART_SIZE, xSpeed, ySpeed);
                        snake.Add(sc);
                    }
                }
                else if (ySpeed == -SNAKE_SPEED)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        sc = new SnakeComponent(snake[snake.Count() - 1].rect.X, snake[snake.Count() - 1].rect.Y + PART_SIZE, PART_SIZE, xSpeed, ySpeed);
                        snake.Add(sc);
                    }
                }
            }

            // TODO: Check head collision with the rest of the snake body
            // skip second body part
            if (snake.Count() > 2)
            {
                for (int i = 2; i < snake.Count() - 1; i++)
                {
                    if (snake[0].Collision(snake[i]))
                    {
                        // end game
                        StopGame();
                    }
                }
            }
            #endregion

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(sb, f.rect);

            foreach (SnakeComponent sc in snake)
            {
                e.Graphics.FillRectangle(sb, sc.rect);

                /*Pen pen = new Pen(Color.Red);
                e.Graphics.DrawRectangle(pen, sc.rect); */
            }

            e.Graphics.DrawString("Score: " + score, textFont, sb, 25, 25);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && gameTimer.Enabled)
            {
                gameTimer.Enabled = false;
                rightArrowDown = leftArrowDown = upArrowDown = downArrowDown = false;

                DialogResult result = PauseForm.Show();

                if (result == DialogResult.Cancel)
                {
                    gameTimer.Enabled = true;
                }
                else if (result == DialogResult.Abort)
                {
                    Form1.ChangeScreen(this, "MenuScreen");
                }
            }

            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    leftArrowDown = true;
                    break;

                case Keys.Right:
                case Keys.D:
                    rightArrowDown = true;
                    break;

                case Keys.Up:
                case Keys.W:
                    upArrowDown = true;
                    break;

                case Keys.Down:
                case Keys.S:
                    downArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    leftArrowDown = false;
                    break;

                case Keys.Right:
                case Keys.D:
                    rightArrowDown = false;
                    break;

                case Keys.Up:
                case Keys.W:
                    upArrowDown = false;
                    break;

                case Keys.Down:
                case Keys.S:
                    downArrowDown = false;
                    break;
            }
        }
    }
}
