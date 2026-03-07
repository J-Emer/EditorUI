using Microsoft.Xna.Framework;

namespace EditorUI.Util
{
    public class MouseEvent
    {
        public Point Position;

        public bool LeftDown;
        public bool LeftReleased;

        public bool RightDown;
        public bool RightReleased;

        public bool MiddleDown;
        public bool MiddleReleased;

        public int ScrollDelta;


        public MouseEvent()
        {
            Position = InputManager.MousePosition;
            LeftDown = InputManager.LeftDown;
            LeftReleased = InputManager.LeftReleased;
            RightDown = InputManager.RightDown;
            RightReleased = InputManager.RightReleased;
            MiddleDown = InputManager.MiddleDown;
            MiddleReleased = InputManager.MiddleReleased;
            ScrollDelta = InputManager.ScrollDelta;
        }


        public override string ToString()
        {
            return $"Position: {Position} | LeftDown: {LeftDown} | LeftReleased: {LeftReleased} | RightDown: {RightDown} | RightReleased: {RightReleased} | MiddleDown: {MiddleDown} | MiddleReleased: {MiddleReleased} | ScrollDelta: {ScrollDelta}";
        }
    }
}
