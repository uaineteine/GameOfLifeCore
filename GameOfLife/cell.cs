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
        public bool HasChanged()
        {
            return changed;
        }
        protected bool previous;

        public void update(bool aliv)
        {
            previous = alive;
            alive = aliv;
            changed = (alive != previous);
        }
    }
}
