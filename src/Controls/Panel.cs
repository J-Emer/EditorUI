using EditorUI.Util;

namespace EditorUI.Controls
{
    public class Panel : ContainerControl
    {
        //note: Panel is essentially a ContainerControl

        public Panel() : base()
        {
            BackgroundColor = Theme.Background;
        }
    }
}