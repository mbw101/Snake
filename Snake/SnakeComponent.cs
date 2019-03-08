using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Snake
{
    class SnakeComponent
    {
        public Rectangle rect;
        public int xSpeed, ySpeed;
        public Color colour;

        public SnakeComponent(int _x, int _y, int _size, int _xSpeed, int _ySpeed)
        {
            rect = new Rectangle(_x, _y, _size, _size);
            colour = Color.White;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
        }

        // collision with the form
        public bool Collision(Form f)
        {
            if (rect.X < 0 || rect.X > f.Width - (rect.Width / 2))
            {
                return true;
            }

            if (rect.Y < 0 || rect.Y > f.Height - (rect.Height / 2))
            {
                return true;
            }

            return false;
        }

        // Collision with food
        public bool Collision(Food f)
        {
            if (rect.IntersectsWith(f.rect))
            {
                return true;
            }

            return false;
        }

        // TODO: Collision with a snake body part with the head
        public bool Collision(SnakeComponent sc)
        {
            if (rect.IntersectsWith(sc.rect))
            {
                return true;
            }
            return false;
        }

        // move the snake body part with its x and y speeds
        public void Move()
        {
            rect.X += xSpeed;
            rect.Y += ySpeed;
        }

        // move the body part to that position
        public void Move(int x, int y)
        {
            rect.X = x;
            rect.Y = y;
        }
    }
}
