using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Database
{
    internal class XmlSerialization : ISerializable
    {
        private XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));


        public List<Employee> Read()
        {
            List<Employee> employees;
            using (FileStream fs = new FileStream("Employees.xml", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    employees = (List<Employee>)serializer.Deserialize(fs);
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
            using (FileStream fs = new FileStream("Employees.xml", FileMode.Create))
            {
                serializer.Serialize(fs, employees);
            }
        }


    }
}
