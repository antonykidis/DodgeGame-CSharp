using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Zone
    {
        public Rectangle Area { get; protected set; }

        public Zone(int x, int y, int width, int height)
        {
            Area = new Rectangle(x , y, width, height);
        }
    }
}
