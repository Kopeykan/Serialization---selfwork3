using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    [Serializable]
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int ID { get; set; }
        public Employee()
        {

        }
        public Employee(string firstName, string lastName, string position, int id)
        {
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            ID = id;
        }
            

        public override string ToString()
        {
            string str =ID + " " + FirstName + " " + LastName + " " + Position;
            return str;
        }
    }
}
