using System;
using System.Linq;
using EditorUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EditorUI.Controls
{
    public class Window : DockableControl
    {
        public int HeaderHeight{get;set;} = 30;
        public SpriteFont Font{get;set;} = AssetLoader.DefaultFont;
        public string HeaderText{get;set;}
        private Rectangle _headerRect = new Rectangle();
        private Rectangle _bodyRect = new Rectangle();
        public Color HeaderColor{get;set;} = Theme.HeaderBackground;
        public Color FontColor{get;set;} = Theme.TextPrimary;
        private Vector2 _textPos = Vector2.Zero;
        private bool _isDragging = false;
        private bool _isResizing = false;
        private Point _offset = Point.Zero;
        private Rectangle _grabRect = new Rectangle();
        public int GrabberSize{get;set;} = 30;
        public Color GrabberColor{get;set;} = Color.White * 0.25f;
        public Point MinSize{get;set;} = new Point(400, 300);
        private Button _closeButton;



        public ControlCollection Children{get; private set;} = new ControlCollection();
        public Layout Layout{get;set;} = new RowLayout();
        public int Padding{get;set;} = 5;



        private ContextMenu _windowContextMenu;



        public Window(string name) : base()
        {
            _closeButton = new Button
            {
                Text = "X",
                BorderThickness = 0,
                Size = new Point(HeaderHeight, HeaderHeight),
                NormalColor = Color.Transparent,
                HoverColor = Color.Red,
                FontColor = Color.White,
                OnClick = CloseBtnClicked
            };

            Name = name;
            HeaderText = name;
            BackgroundColor = Theme.Background;
            BorderColor = Theme.BorderLight;
            Size = new Point(300, 400);

            Children.OnControlsChanged += AfterDirty;

            SetWindowContextMenu();
            // Logger.Log(this, "//todo: Add a MinSize check");
        }
        private void SetWindowContextMenu()
        {
            _windowContextMenu = new ContextMenu();
            Button left = _windowContextMenu.Add("Left");
            left.UserData = DockStyle.Left;
            left.OnClick = DockButtonClicked;

            Button right = _windowContextMenu.Add("Right");
            right.UserData = DockStyle.Right;
            right.OnClick = DockButtonClicked;

            Button top = _windowContextMenu.Add("Top");
            top.UserData = DockStyle.Top;
            top.OnClick = DockButtonClicked;

            Button bottom = _windowContextMenu.Add("Bottom");
            bottom.UserData = DockStyle.Bottom;
            bottom.OnClick = DockButtonClicked;

            Button close = _windowContextMenu.Add("Close");
            close.OnClick = CloseBtnClicked;
        }

        private void DockButtonClicked(Button button, MouseEvent @event)
        {
            Dock = (DockStyle)button.UserData;
            _windowContextMenu.IsActive = false;
            UIManager.Instance.RemoveOverlay(_windowContextMenu);
        }

        private void CloseBtnClicked(Button button, MouseEvent @event)
        {
            UIManager.Instance.RemoveOverlay(_windowContextMenu);
            UIManager.Instance.Remove(this);
        }
        public override Control HitTest(Point p)
        {
            if(!IsActive){return null;}

            var hitClose = _closeButton.HitTest(p);

            if(hitClose != null)
            {
                return _closeButton;
            }

            var contextMenuHit = _windowContextMenu.HitTest(p);

            if(contextMenuHit != null)
            {
                return contextMenuHit;
            }

            for (int i = 0; i < Children.Controls.Count; i++)
            {
                var hit = Children.Controls[i].HitTest(p);

                if(hit != null)
                {
                    return hit;
                }
            }
            return Bounds.Contains(p)? this : null;
        }
        protected override void AfterDirty()
        {           
            _headerRect = new Rectangle(Position.X + BorderThickness, Position.Y + BorderThickness, Size.X - (BorderThickness * 2), HeaderHeight);
            _bodyRect = new Rectangle(
                Position.X,
                Position.Y + HeaderHeight,
                Size.X,
                Size.Y - HeaderHeight
            );

            Vector2 _halfText = Font.MeasureString(HeaderText) * 0.5f;
            _textPos = _headerRect.Center.ToVector2() - _halfText;

            _grabRect = new Rectangle(
                Bounds.Right - GrabberSize,
                Bounds.Bottom - GrabberSize,
                GrabberSize,
                GrabberSize
            );

            _closeButton.Position = new Point(Bounds.Right - HeaderHeight, Position.Y);

            Layout.HandleLayout(_bodyRect, Children.Controls, Padding);
        }
        public override void Draw(SpriteBatch spritebatch)
        {           
            ScissorStack.Push(Bounds);

            base.Draw(spritebatch);
            spritebatch.Draw(AssetLoader.Pixel, _headerRect, HeaderColor);
            spritebatch.DrawString(Font, HeaderText, _textPos, FontColor);
            _closeButton.Draw(spritebatch);

            foreach (var control in Children.Controls.OrderByDescending(c => c.ZOrder))
            {
                control.Draw(spritebatch);
            }

            if(Dock == DockStyle.None)
            {
                spritebatch.Draw(AssetLoader.Pixel, _grabRect, GrabberColor);
            }


            ScissorStack.Pop(); 
        }




        public override void OnMouseDown(MouseEvent e)
        {
            if(e.LeftDown)
            {
                if(_headerRect.Contains(e.Position))
                {
                    _isDragging = true;
                    _offset = e.Position - Position;
                    Size = MinSize;
                    dock = DockStyle.None;
                }

                if(_grabRect.Contains(e.Position) && Dock == DockStyle.None)
                {
                    _isResizing = true;
                }
            }

            if(e.RightDown)
            {
                if(_headerRect.Contains(e.Position))
                {
                    _windowContextMenu.IsActive = true;
                    _windowContextMenu.Position = e.Position;
                    UIManager.Instance.ShowOverlay(_windowContextMenu);
                }
            }

        }
        public override void OnMouseDrag(MouseEvent e)
        {
            if(_isDragging)
            {
                Position = e.Position - _offset;
                UIManager.Instance.ShowDropTargets();
            }
            if(_isResizing)
            {
                Size = e.Position - Position;
            }
        }
        public override void OnMouseUp(MouseEvent e)
        {
            if(_isDragging)
            {
                UIManager.Instance.CheckDock(this);
                UIManager.Instance.HideDropTargets();
            }
            _isDragging = false;
            _isResizing = false;
        }
    
    
    }
}