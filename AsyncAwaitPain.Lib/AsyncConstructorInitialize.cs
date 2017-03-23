using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib
{
    public class AsyncConstructorInitialize : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task Initialize()
        {
            await Task.Delay(5);
            Message = "Completed";
            Completed = true;
        }

        private string _message = "Started";

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
            }
        }

        private bool _completed;

        public bool Completed
        {
            get { return _completed; }
            set
            {
                _completed = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Completed)));
            }
        }
    }
}
