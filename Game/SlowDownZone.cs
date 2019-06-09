using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class SlowDownZone : Zone
    {
        public int PlayerSpeedInsideZone { get; private set; }

        public SlowDownZone(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            PlayerSpeedInsideZone = 1;
        }
    }
}
