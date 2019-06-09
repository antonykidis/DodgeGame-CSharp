using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    interface IGameObject
    {
        void Move(object sender);
        bool CheckHorizntalWallCollision(Form form);
        bool CheckVerticalWallCollision(Form form);
        void SetPosition(int x, int y);
        void SetPosition(Point point);
    }
}
