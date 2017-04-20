using Csla;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.CSLA
{

    [Serializable]
    public class CslaBusinessObjectExceptionList : BusinessListBase<CslaBusinessObjectExceptionList, CslaBusinessObjectException>
    {
        private async Task DataPortal_Fetch()
        {

            for (var i = 0; i < 10; i++)
            {
                TaskClass t = new TaskClass();
                Add(DataPortal.FetchChild<CslaBusinessObjectException>(t, i == 8));
                await t.Task;
            }

            // Option 2
            //await Task.WhenAll(tasks.ToArray());
        }
    }

    [Serializable]
    public class CslaBusinessObjectException : BusinessBase<CslaBusinessObjectException>
    {

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        private void Child_Fetch(TaskClass t, bool throwException)
        {
            t.Task = Child_Fetch(throwException);
        }

        private async Task Child_Fetch(bool throwException)
        {
            Debug.WriteLine($"Child Start: TaskID: {Task.CurrentId}  ThreadID: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(500);
            await Task.Run(async () => { await Task.Delay(100); if (throwException) { throw new Exception("Child exception"); } });
            LoadProperty(NameProperty, Guid.NewGuid().ToString());
            Debug.WriteLine($"Child Finish: TaskID: {Task.CurrentId}  ThreadID: {Thread.CurrentThread.ManagedThreadId}");
        }

    }
}
