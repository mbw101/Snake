using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class Food
    {
        public Rectangle rect;
        public Color colour;

        public Food(int _x, int _y, int _size)
        {
            rect.X = _x;
            rect.Y = _y;
            rect.Width = _size;
            rect.Height = _size;
            colour = Color.White;
        }

        public Food(int _x, int _y, int _size, Color _colour)
        {
            rect.X = _x;
            rect.Y = _y;
            rect.Width = _size;
            rect.Height = _size;
            colour = _colour;
        }
    }
}
