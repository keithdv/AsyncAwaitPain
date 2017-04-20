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

    public class TaskClass
    {
        public Task Task { get; set; }
    }


    [Serializable]
    public class CslaBusinessObjectList : BusinessListBase<CslaBusinessObjectList, CslaBusinessObject>
    {
        private async Task DataPortal_Fetch()
        {
            for (var i = 0; i < 10; i++)
            {
                TaskClass t = new TaskClass();
                Add(DataPortal.FetchChild<CslaBusinessObject>(t));
                await t.Task;
            }

        }
    }

    [Serializable]
    public class CslaBusinessObject : BusinessBase<CslaBusinessObject>
    {
        
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        private void Child_Fetch(TaskClass t)
        {
            t.Task = Child_Fetch();
        }

        private async Task Child_Fetch()
        {
            Debug.WriteLine($"Child Start: TaskID: {Task.CurrentId}  ThreadID: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(500);
            LoadProperty(NameProperty, Guid.NewGuid().ToString());
            Debug.WriteLine($"Child Finish: TaskID: {Task.CurrentId}  ThreadID: {Thread.CurrentThread.ManagedThreadId}");
        }

    }
}
