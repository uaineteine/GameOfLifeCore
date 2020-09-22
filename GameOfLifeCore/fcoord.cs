using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class fcoord
    {
        public fcoord(float xi, float yi)
        {
            x = xi;
            y = yi;
        }
        public float x;
        public float y;

        public static fcoord operator *(fcoord a, float b)
            => new fcoord(a.x * b, a.y * b);

        public static fcoord operator *(fcoord a, fcoord b)
            => new fcoord(a.x * b.x, a.y * b.y);
    }
}
