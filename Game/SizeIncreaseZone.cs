using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class SizeIncreaseZone : Zone
    {
        public int PlayerWidthIncreaseValue { get; private set; }
        public int PlayerHeightIncreaseValue { get; private set; }

        public SizeIncreaseZone(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            PlayerWidthIncreaseValue = 20;
            PlayerHeightIncreaseValue = 20;
        }
    }
}
