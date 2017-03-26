using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xam.Dynamic.Rowheights.Properties;

namespace Xam.Dynamic.Rowheights.ViewModels
{
    public abstract class NotifyPropertyChangedBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
