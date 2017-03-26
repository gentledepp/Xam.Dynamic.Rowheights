using Xam.Dynamic.Rowheights.ViewModels;
using Xamarin.Forms;

namespace Xam.Dynamic.Rowheights
{
    internal class ItemTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var section = item as HeadlineViewModel;

            if (section != null)
                return ExpandableSectionTemplate;
            
            var question = item as QuestionViewModel;

            switch (question?.FieldType)
            {
                case FieldTypes.RadioButton:
                case FieldTypes.Checkbox:
                    return QuestionCheckboxTemplate;
                case FieldTypes.Editor:
                    return QuestionMultilineTextTemplate;
                case FieldTypes.ComboBox:
                    return QuestionPickerTemplate;
                case FieldTypes.DatePicker:
                    return QuestionDatePickerTemplate;
                case FieldTypes.TimePicker:
                    return QuestionTimePickerTemplate;
                case FieldTypes.Picture:
                case FieldTypes.Sketch:
                case FieldTypes.Signature:
                    return QuestionImageTemplate;
                case FieldTypes.Barcode:
                case FieldTypes.Location:
                    return QuestionEntryAndButtonTemplate;
            }

            return QuestionTextTemplate;
        }
        
        public DataTemplate QuestionEntryAndButtonTemplate { get; set; }

        public DataTemplate QuestionImageTemplate { get; set; }

        public DataTemplate QuestionTimePickerTemplate { get; set; }

        public DataTemplate QuestionDatePickerTemplate { get; set; }

        public DataTemplate QuestionPickerTemplate { get; set; }

        public DataTemplate QuestionTextTemplate { get; set; }

        public DataTemplate QuestionMultilineTextTemplate { get; set; }

        public DataTemplate QuestionCheckboxTemplate { get; set; }

        public DataTemplate ExpandableSectionTemplate { get; set; }
    }
}
