using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class GameObject : IGameObject
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int Height { get; protected set; }
        public int Width { get; protected set; }
        public int Speed { get; protected set; }
        public readonly int OriginalWidth;
        public readonly int OriginalHeight;
        public readonly Point OriginalPosition;

        public GameObject(int x, int y)
            : this(x, y, 10, 10) { }
        public GameObject(int x, int y, int width, int height)
        {
            Height = height;
            Width = width;
            OriginalHeight = Height;
            OriginalWidth = Width;
            X = x;
            Y = y;
            OriginalPosition = new Point(x, y);
        }

        public virtual bool CheckHorizntalWallCollision(Form form)
        {
            return X < 0 || X + Width > form.ClientRectangle.Width;
        }

        public virtual bool CheckVerticalWallCollision(Form form)
        {
            return Y < 0 || Y + Height > form.ClientRectangle.Height;
        }

        public virtual void ResolveHorizntalWallCollision(Form form)
        {
            while (X + Width > form.ClientRectangle.Width)
                X--;

            while (X < 0)
                X++;
        }

        public virtual void ResolveVerticallWallCollision(Form form)
        {
            while (Y + Height > form.ClientRectangle.Height)
                Y--;

            while (Y < 0)
                Y++;
        }

        public virtual void Move(object sender)
        {
            return;
        }

        public void SetOriginalSize()
        {
            Height = OriginalHeight;
            Width = OriginalWidth;
        }

        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetPosition(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public virtual void MoveUp(Form form)
        {
            Y -= Speed;
            if (CheckVerticalWallCollision(form))
                ResolveVerticallWallCollision(form);
        }

        public virtual void MoveDown(Form form)
        {
            Y += Speed;
            if (CheckVerticalWallCollision(form))
                ResolveVerticallWallCollision(form);
        }

        public virtual void MoveLeft(Form form)
        {
            X -= Speed;
            if (CheckHorizntalWallCollision(form))
                ResolveHorizntalWallCollision(form);
        }

        public virtual void MoveRight(Form form)
        {
            X += Speed;
            if (CheckHorizntalWallCollision(form))
                ResolveHorizntalWallCollision(form);
        }
    }
}
