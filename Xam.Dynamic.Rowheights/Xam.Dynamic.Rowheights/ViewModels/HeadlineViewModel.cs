using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xam.Dynamic.Rowheights.ViewModels
{
    public class HeadlineViewModel : ViewModelBase
    {
        public HeadlineViewModel()
        {
            
        }

        public HeadlineViewModel(string title)
        {
            Title = title;
        }
    }
}
