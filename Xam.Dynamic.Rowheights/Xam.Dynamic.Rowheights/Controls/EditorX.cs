using Xamarin.Forms;

namespace Xam.Dynamic.Rowheights.Controls
{
    /// <summary>
    /// Part of a workaround to let the editor grow in height as text is added
    /// see: https://forums.xamarin.com/discussion/21951/allow-the-editor-control-to-grow-as-content-lines-are-added
    /// </summary>
    public class EditorX : Editor
    {
        public void InvalidateLayout()
        {
            this.InvalidateMeasure();
        }
    }
}
