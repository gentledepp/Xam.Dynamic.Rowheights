using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Xam.Dynamic.Rowheights.ViewModels
{
    public class QuestionViewModel : ViewModelBase
    {

        public QuestionViewModel()
        {
            
        }

        public QuestionViewModel(string title)
        {
            Title = title;
        }

        public QuestionViewModel(string title, string fieldType, params AnswerViewModel[] answers)
        {
            Title = title;
            FieldType = fieldType;
            Children = new ObservableCollection<AnswerViewModel>(answers);
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
