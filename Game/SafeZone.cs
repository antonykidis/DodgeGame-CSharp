using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    class SafeZone : Zone
    {
        public readonly int BorderWidth;

        public SafeZone(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            BorderWidth = 4;
            Area = new Rectangle(x, y, width - BorderWidth / 2, height - BorderWidth / 2);
        }

        public void DecreaseZoneWidth()
        {
            var currentX = Area.X;
            var currentY = Area.Y;
            var currentWidth = Area.Width;
            var currentHeight = Area.Height;
            var delta = 1;
            Area = new Rectangle(currentX + delta, currentY, currentWidth - delta * 2, currentHeight);
        }

        public void DecreaseZoneHeight()
        {
            var currentX = Area.X;
            var currentY = Area.Y;
            var currentWidth = Area.Width;
            var currentHeight = Area.Height;
            var delta = 1;
            Area = new Rectangle(currentX, currentY + delta, currentWidth, currentHeight - delta * 2);
        }
    }
}
