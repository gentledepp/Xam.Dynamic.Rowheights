using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xam.Dynamic.Rowheights.ViewModels
{
    public abstract class ViewModelBase : NotifyPropertyChangedBase
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }
}
