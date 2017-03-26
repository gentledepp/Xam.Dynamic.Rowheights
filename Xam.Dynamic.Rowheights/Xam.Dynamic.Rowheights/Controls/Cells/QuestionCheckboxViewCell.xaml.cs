using Xamarin.Forms;

namespace Xam.Dynamic.Rowheights.Controls.Cells
{
    public partial class QuestionCheckboxViewCell
    {

        public static readonly BindableProperty VariantProperty = BindableProperty.Create(nameof(Variant), typeof(CheckboxVariant), typeof(QuestionCheckboxViewCell), CheckboxVariant.Box, propertyChanged: CheckboxVariantPropertyChanged);

        public CheckboxVariant Variant
        {
            get { return (CheckboxVariant) CheckboxGroup.GetValue(CheckboxGroup.VariantProperty); }
            set { CheckboxGroup.SetValue(CheckboxGroup.VariantProperty, value); }
        }

        private static void CheckboxVariantPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var checkbox = bindable as QuestionCheckboxViewCell;

            checkbox?.CheckboxGroup.SetValue(CheckboxGroup.VariantProperty, newvalue);
        }

        public QuestionCheckboxViewCell()
        {
            InitializeComponent();
        }
    }
}
