using System.Windows.Input;
using Xamarin.Forms;

namespace Xam.Dynamic.Rowheights.Controls.Cells
{
    public partial class QuestionEntryAndButtonViewCell
    {
        public static BindableProperty ButtonTextProperty = BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(QuestionEntryAndButtonViewCell));

        public static BindableProperty ButtonCommandProperty = BindableProperty.Create(nameof(ButtonCommand), typeof(ICommand), typeof(QuestionEntryAndButtonViewCell));

        public QuestionEntryAndButtonViewCell()
        {
            InitializeComponent();
        }

        public string ButtonText
        {
            get { return (string) GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public ICommand ButtonCommand
        {
            get { return (ICommand) GetValue(ButtonCommandProperty); }
            set { SetValue(ButtonCommandProperty, value); }
        }
    }
}
