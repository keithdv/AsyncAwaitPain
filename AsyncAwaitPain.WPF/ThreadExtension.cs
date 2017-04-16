using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitPain.WPF
{
    public static class ThreadExtension
    {

        public static ExecutionContext PrivateExecutionContext(this Thread thread)
        {
            var field = typeof(Thread).GetField("m_ExecutionContext", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            return (ExecutionContext)field.GetValue(thread);

        }
    }
}
