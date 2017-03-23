using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib
{
    public class AsyncConstructorException : INotifyPropertyChanged
    {
        public AsyncConstructorException()
        {

            Delay().ContinueWith(x =>
            {
                if (x.Exception != null)
                {
                    throw x.Exception;
                }
            });

        }

        private async Task Delay()
        {
            await Task.Delay(5);
            Message = "Exception";
            throw new Exception("Failure");
        }

        private string _message = "Started";

        public event PropertyChangedEventHandler PropertyChanged;

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
