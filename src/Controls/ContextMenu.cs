using EditorUI.Util;
using Microsoft.Xna.Framework;

namespace EditorUI.Controls
{
    public class ContextMenu : ContainerControl
    {



        public ContextMenu()
        {
            Layout = new RowLayout();
            Padding = 5;
            BackgroundColor = Color.LightGray;   
            Size = new Point(150, 300);
            BorderThickness = 1;
            BorderColor = Color.Black; 
            IsActive = false;
        }

        public Button Add(string text)
        {
            Logger.Log(this, "Add");

            Button _b = new Button
            {
                Text = text,
                OnClick = RemoveOverlay
            };

            _b.OnClick += RemoveOverlay;

            Children.Add(_b);
            
            return _b;
        }
        public Button Add(string text, object userdata)
        {
            Logger.Log(this, "Add with userdata");

            Button _b = new Button
            {
                Text = text,
                UserData = userdata,
                OnClick = RemoveOverlay
            };

            _b.OnClick += RemoveOverlay;

            Children.Add(_b);
            
            return _b;
        }
        private void RemoveOverlay(Button button, MouseEvent @event)
        {
            UIManager.Instance.RemoveOverlay(this);
            IsActive = false;
            Logger.Log(this, "Removing Overlay");
        }
        public void Remove(Button button)
        {
            Children.Remove(button);
        }
        public override void OnMouseExit(MouseEvent e)
        {
            // IsActive = false;
            // UIManager.Instance.RemoveOverlay(this);
        }
    }
}