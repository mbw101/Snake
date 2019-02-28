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
        List<SnakeComponent> snake = new List<SnakeComponent>();
        const int snakeSpeed = 7;
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
            // make one snake part
            SnakeComponent sc = new SnakeComponent(25, 25, 20, snakeSpeed, 0);
            snake.Add(sc);
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
                    sc.ySpeed = -snakeSpeed;
                    sc.xSpeed = 0;
                }
                else if (downArrowDown)
                {
                    sc.ySpeed = snakeSpeed;
                    sc.xSpeed = 0;
                }
                else if (leftArrowDown)
                {
                    sc.xSpeed = -snakeSpeed;
                    sc.ySpeed = 0;
                }
                else if (rightArrowDown)
                {
                    sc.xSpeed = snakeSpeed;
                    sc.ySpeed = 0;
                }
            }

            #endregion

            #region Collision
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
