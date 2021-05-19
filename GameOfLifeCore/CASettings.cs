using System;
using System.Collections.Generic;
using System.Text;

namespace Uaine.CellularAutomata
{
    public class CASettings
    {
        public float ChanceStartAlive;
        public bool wrapping;    //if the edges should be wrapped

        public CASettings(bool wrapping, float chanceStartAlive)
        {
            ChanceStartAlive = chanceStartAlive;
            this.wrapping = wrapping;
        }
    }
}
