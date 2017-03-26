using Xamarin.Forms;

namespace Xam.Dynamic.Rowheights.Controls.Cells
{
    //[ContentProperty("QuestionContent")]
    public partial class QuestionViewCellBase
    {
        //public View QuestionContent
        //{
        //    get { return ContentView.Content; }
        //    set { ContentView.Content = value; }
        //}

        protected override void OnBindingContextChanged()
        {
            this.ContextActions.Clear();
            this.ContextActions.Add(new MenuItem() {IsDestructive = true, Text = "Clear"});

            base.OnBindingContextChanged();

            ForceUpdateSize();
        }

        public QuestionViewCellBase()
        {
            InitializeComponent();
        }
    }
}
