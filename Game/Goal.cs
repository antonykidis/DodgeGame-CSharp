using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Goal : GameObject
    {
        public Goal(int x, int y)
            : base(x, y)
        {
            Width = 10;
            Height = 10;
        }
    }
}
