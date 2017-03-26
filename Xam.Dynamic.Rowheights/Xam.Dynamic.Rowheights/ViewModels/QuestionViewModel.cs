using System;
using System.Collections.ObjectModel;

namespace Xam.Dynamic.Rowheights.ViewModels
{
    public class QuestionViewModel : ViewModelBase
    {

        public QuestionViewModel()
            : this(null)
        {
        }

        public QuestionViewModel(string title)
        {
            Title = title;
            Children = new ObservableCollection<AnswerViewModel> {new AnswerViewModel()};
        }

        public QuestionViewModel(string title, string fieldType, params AnswerViewModel[] answers)
        {
            Title = title;
            FieldType = fieldType;
            
            Children = new ObservableCollection<AnswerViewModel>(answers);

            if (answers.Length == 0)
            {
                Children.Add(new AnswerViewModel());
            }
        }

        private string _fieldType;

        public string FieldType
        {
            get { return _fieldType; }
            set
            {
                _fieldType = value; 
                OnPropertyChanged();
            }
        }

        private ObservableCollection<AnswerViewModel> _children;
        private DateTime? _dateValue;

        public ObservableCollection<AnswerViewModel> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DateValue
        {
            get { return _dateValue; }
            set
            {
                _dateValue = value; 
                OnPropertyChanged();
            }
        }
    }
}
