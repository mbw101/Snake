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

        public bool Collision(Form f)
        {
            if (rect.X < 0 || rect.X > f.Width - rect.Width)
            {
                return true;
            }

            if (rect.Y < 0 || rect.Y > f.Height - rect.Height)
            {
                return true;

            }

            return false;
        }

        public void Collision()
        {

        }

        public void Move()
        {
            rect.X += xSpeed;
            rect.Y += ySpeed;
        }

        public void Move(int x, int y)
        {
            rect.X = x;
            rect.Y = y;
        }
    }
}
