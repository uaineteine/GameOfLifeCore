using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class cell
    {
        public cell(bool aliv)
        {
            alive = aliv;
            changed = false;
            previous = alive;
        }
        public bool alive;
        protected bool changed;
        protected bool previous;

        public void update(bool aliv)
        {
            alive = aliv;
            changed = (alive == previous);
        }
    }
}
