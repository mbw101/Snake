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
        const int SNAKE_SPEED = 7;
        const int MAX_PARTS = 20;
        const int MARGIN = 3;
        const int PART_SIZE = 16;
        int score = 0;
        bool leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;
        bool moveUp = false;
        bool moveLeft = false;

        public GameScreen()
        {
            InitializeComponent();

            OnStart();         
        }

        public void OnStart()
        {
            // create text graphics
            textFont = new Font("Verdana", 14, FontStyle.Regular);
            // make one snake part
            SnakeComponent sc = new SnakeComponent(65, 65, PART_SIZE, SNAKE_SPEED, 0);
            snake.Add(sc);
            SnakeComponent sc2 = new SnakeComponent(sc.rect.X - sc.rect.Width - MARGIN, sc.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            snake.Add(sc2);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            foreach (SnakeComponent sc in snake)
            {
                sc.Move();
            }

            #region movement controls

            foreach (SnakeComponent sc in snake)
            {
                // TODO: Check to make sure that you can't move into the snake (ie can't move down when snake is moving up)
                if (upArrowDown)
                {
                    if (sc == snake[0])
                    {
                        sc.ySpeed = -SNAKE_SPEED;
                        sc.xSpeed = 0;
                    }
                    else
                    {
                        if (sc.rect.Y == snake[0].rect.Y)
                        {
                            sc.ySpeed = -SNAKE_SPEED;
                            sc.xSpeed = 0;
                        }
                    }
                }
                else if (downArrowDown)
                {
                    sc.ySpeed = SNAKE_SPEED;
                    sc.xSpeed = 0;
                }
                else if (leftArrowDown)
                {
                    sc.xSpeed = -SNAKE_SPEED;
                    sc.ySpeed = 0;
                }
                else if (rightArrowDown)
                {
                    sc.xSpeed = SNAKE_SPEED;
                    sc.ySpeed = 0;
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

            #endregion

            

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
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
