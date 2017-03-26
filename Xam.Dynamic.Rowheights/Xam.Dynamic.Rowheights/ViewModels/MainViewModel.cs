using System.Collections.ObjectModel;

namespace Xam.Dynamic.Rowheights.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Title = "Row heights test...";

            _items = new ObservableCollection<ViewModelBase>();
            _items.Add(new HeadlineViewModel("Headline 1"));
            _items.Add(new QuestionViewModel("Checkbox", FieldTypes.Checkbox, new AnswerViewModel("a"),new AnswerViewModel("b"), new AnswerViewModel("c")));
            _items.Add(new QuestionViewModel("Text singleline", FieldTypes.Editor, new AnswerViewModel()));
            _items.Add(new QuestionViewModel("Radiobutton", FieldTypes.RadioButton, new AnswerViewModel("r1"), new AnswerViewModel("r2")));
            _items.Add(new QuestionViewModel("Pic", FieldTypes.Picture, new AnswerViewModel()));

        }

        private ObservableCollection<ViewModelBase> _items;

        public ObservableCollection<ViewModelBase> Items
        {
            get { return _items; }
            set
            {
                _items = value; 
                OnPropertyChanged();
            }
        }
    }
}
