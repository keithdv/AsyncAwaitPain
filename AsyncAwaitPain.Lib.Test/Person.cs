using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.Test
{
    public class Person
    {
        public Person()
        { }

        ~Person()
        {      // This gets called on Garbage Collection
        }
    }


    [TestClass]
    public class PersonTest
    {
        public Person CreatePerson()
        {
            return new Person();
        }

        [TestMethod]
        public void Person()
        {
            // Person objects getting created but not used
            // Valid syntatically
            CreatePerson();
            CreatePerson();

            // 0 References
            // Caught by the next garbage collector
            GC.Collect();
        }

    }

}
