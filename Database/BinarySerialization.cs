using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class BinarySerialization : ISerializable
    {
        BinaryFormatter formatter = new BinaryFormatter();
        public List<Employee> Read()
        {
            List<Employee> employees;
            using (FileStream fs = new FileStream("Employees.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    employees = (List<Employee>)formatter.Deserialize(fs);
                }
                else
                {
                    employees = new List<Employee>();
                }
            }
            return employees;
        }

        public void Write(List<Employee> employees)
        {
            using (FileStream fs = new FileStream("Employees.dat", FileMode.Create))
            {
                formatter.Serialize(fs, employees);
            }
        }
    }
}
