using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.Constructor
{
    public class AsyncConstructor : INotifyPropertyChanged
    {
        public AsyncConstructor()
        {

            Completed = false;

            // Very tempting to do
            // However exceptions are lost 
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
            await Task.Run(async () =>
            {
                await Task.Delay(1000);
            });

            Message = "Completed";
            Completed = true;
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
