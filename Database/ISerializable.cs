using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    interface ISerializable
    {
        List<Employee> Read();
        void Write(List<Employee> employees);
    }
}
