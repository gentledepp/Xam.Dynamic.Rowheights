using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xam.Dynamic.Rowheights.ViewModels
{
    public class AnswerViewModel : ViewModelBase
    {
        public AnswerViewModel()
        {
            
        }

        public AnswerViewModel(string title)
        {
            Title = title;
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value; 
                OnPropertyChanged();
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }

        }


    }
}
