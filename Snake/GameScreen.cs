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
        SolidBrush sb = new SolidBrush(Color.White);
        Font textFont;
        List<SnakeComponent> snake = new List<SnakeComponent>();
        Food f;
        Random random = new Random();
        const int SNAKE_SPEED = 7;
        const int MAX_PARTS = 20;
        const int MARGIN = 3;
        const int PART_SIZE = 16;
        int score = 0;
        bool leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;

        public GameScreen()
        {
            InitializeComponent();

            OnStart();         
        }

        public void OnStart()
        {
            // create text graphics
            textFont = new Font("Verdana", 14, FontStyle.Regular);

            // make snake parts
            SnakeComponent sc = new SnakeComponent(65, 65, PART_SIZE, SNAKE_SPEED, 0);
            snake.Add(sc);
            //SnakeComponent sc2 = new SnakeComponent(sc.rect.X - sc.rect.Width, sc.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            //snake.Add(sc2);
            //SnakeComponent sc3 = new SnakeComponent(sc2.rect.X - sc2.rect.Width, sc2.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            //snake.Add(sc3);
            //SnakeComponent sc4 = new SnakeComponent(sc3.rect.X - sc3.rect.Width, sc3.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            //snake.Add(sc4);
            //SnakeComponent sc5 = new SnakeComponent(sc4.rect.X - sc4.rect.Width, sc4.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            //snake.Add(sc5);
            //SnakeComponent sc6 = new SnakeComponent(sc5.rect.X - sc5.rect.Width, sc5.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            //snake.Add(sc6);
            //SnakeComponent sc7 = new SnakeComponent(sc6.rect.X - sc6.rect.Width, sc6.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            //snake.Add(sc7);
            //SnakeComponent sc8 = new SnakeComponent(sc7.rect.X - sc7.rect.Width, sc7.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            //snake.Add(sc8);
            //SnakeComponent sc9 = new SnakeComponent(sc8.rect.X - sc8.rect.Width, sc8.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            //snake.Add(sc9);
            //SnakeComponent sc10 = new SnakeComponent(sc9.rect.X - sc9.rect.Width, sc9.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            //snake.Add(sc10);

            makeFood();
        }

        public void moveSnake()
        {
            int tmpX = snake[0].rect.X;
            int tmpY = snake[0].rect.Y;
            int velocityX = 0;
            int velocityY = 0;

            // go from the back to the front 
            // and up the back body part
            // to the next part's location

            // the head is the only one that is having a new location
            // mvoe head position first
            snake[0].Move();

            // start from the back and move up list
            for (int i = snake.Count() - 1; i > 1 ; i--)
            {
                snake[i].rect.X = snake[i - 1].rect.X;
                snake[i].rect.Y = snake[i - 1].rect.Y;
            }

            // move the one after the head to the old head's position
            snake[1].rect.X = tmpX;
            snake[1].rect.Y = tmpY;
        }

        public void makeFood()
        {
            f = new Food(random.Next(0, 800 - PART_SIZE), random.Next(0, PART_SIZE), PART_SIZE);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            moveSnake();

            #region movement controls

            foreach (SnakeComponent sc in snake)
            {
                // TODO: Check to make sure that you can't move into the snake (ie can't move down when snake is moving up)
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

            // TODO: Add part to snake if collides with food

            if (snake[0].Collision(this.FindForm()))
            {
                gameTimer.Stop();

                Thread.Sleep(2000);

                // TODO: Remove game screen and add lose screen
            }
            // when the head collides with food
            if (snake[0].Collision(f))
            {
                // increase score
                score++;
                // make new food
                makeFood();
                // TODO: add body part
                //SnakeComponent sc = new SnakeComponent(snake[snake.Count() - 1].rectX , 65, PART_SIZE, SNAKE_SPEED, 0);
                //snake.Add(sc);
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
            }
           
            e.Graphics.DrawString("Score: " + score, textFont, sb, 25, 25);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;

                case Keys.Right:
                    rightArrowDown = true;
                    break;

                case Keys.Up:
                    upArrowDown = true;
                    break;

                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;

                case Keys.Right:
                    rightArrowDown = false;
                    break;

                case Keys.Up:
                    upArrowDown = false;
                    break;

                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }
    }
}
