using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib
{
    public class AsyncConstructorInitializeException : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task Initialize()
        {
            await Task.Delay(5);
            Message = "Exception";
            throw new Exception("Failure");
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
    }
}
