using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    /// <summary>
    /// the viewmodel abstarct class
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// the event of the change in the property
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// the function that checks if the property has changed
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
