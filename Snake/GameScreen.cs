﻿using System;
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
        int nx, ny;
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

            // make snake parts
            SnakeComponent sc = new SnakeComponent(65, 65, PART_SIZE, SNAKE_SPEED, 0);
            snake.Add(sc);
            SnakeComponent sc2 = new SnakeComponent(sc.rect.X - sc.rect.Width - MARGIN, sc.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            snake.Add(sc2);
            //SnakeComponent sc3 = new SnakeComponent(sc2.rect.X - sc2.rect.Width - MARGIN, sc2.rect.Y, PART_SIZE, SNAKE_SPEED, 0);
            //snake.Add(sc3);
        }

        public void moveSnake()
        {
            int tmpX = 0;
            int tmpY = 0;
            int velocityX = 0;
            int velocityY = 0;

            foreach (SnakeComponent sc in snake)
            {
                // head part
                if (sc == snake[0])
                {
                    // save 
                    tmpX = sc.rect.X;
                    tmpY = sc.rect.Y;
                    velocityX = sc.xSpeed;
                    velocityY = sc.ySpeed;

                    sc.Move();
                }
                else
                {
                    if (velocityX == SNAKE_SPEED)
                    {
                        // the head is moving right
                        sc.Move(tmpX - PART_SIZE - MARGIN, tmpY);
                    }
                    else if (velocityX == -SNAKE_SPEED)
                    {
                        sc.Move(tmpX + PART_SIZE + MARGIN, tmpY);
                    }
                    else if (velocityY == SNAKE_SPEED)
                    {
                        sc.Move(tmpX, tmpY - MARGIN - PART_SIZE);
                    }
                    else if (velocityY == -SNAKE_SPEED)
                    {
                        sc.Move(tmpX, tmpY + MARGIN + PART_SIZE);
                    }
                }
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            moveSnake();
            //foreach (SnakeComponent sc in snake)
            //{
            //    sc.Move();
            //}

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
