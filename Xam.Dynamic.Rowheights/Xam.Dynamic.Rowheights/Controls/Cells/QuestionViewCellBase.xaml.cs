using Xamarin.Forms;

namespace Xam.Dynamic.Rowheights.Controls.Cells
{
    [ContentProperty("QuestionContent")]
    public partial class QuestionViewCellBase
    {
        public View QuestionContent
        {
            get { return ContentView.Content; }
            set { ContentView.Content = value; }
        }

        public QuestionViewCellBase()
        {
            InitializeComponent();
        }
    }
}
