using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Database
{
    class Menu
    {
        private string type;
        List<Employee> employees;
        private ISerializable ser;
        public void Start()
        {
            try
            {
                using (FileStream fs = File.OpenRead("option.ini"))
                {
                    byte[] array = new byte[fs.Length];
                    fs.Read(array, 0, array.Length);
                    type = System.Text.Encoding.Default.GetString(array).ToUpper();
                    switch (type)
                    {
                        case "XML":
                            ser = new XmlSerialization();
                            break;
                        case "BIN":
                              ser = new BinarySerialization();
                              break;
                        default: 
                            ser = new BinarySerialization();
                              break;

                    }
                   LoadDatabase(ser);
                }
            }
            catch (FileNotFoundException ex)
            {
                type = "BIN";
                using (FileStream fs = new FileStream("option.ini", FileMode.Create))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(type);
                    fs.Write(array, 0, array.Length);
                }
                ser = new BinarySerialization();
                LoadDatabase(ser);
            }
        }

        private void LoadDatabase(ISerializable ser)
        {
            employees = ser.Read();
            ShowMenu();
        }

        private void ShowMenu()
        {
            while (true)
            {
                string str;
                if (employees.Count == 0)
                {
                    Console.WriteLine("База данных пуста");
                    Console.WriteLine("Выбирите действие:");
                    Console.WriteLine("1 - добавить сотрудника;");
                    Console.WriteLine("2 - выход");

                    str = Console.ReadLine();
                    switch (str)
                    {
                        case "1":
                            AddEmployee();
                            break;
                        case "2":
                            Exit(ser);
                            return;
                        default:
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("База данных загружена.");
                    Console.WriteLine("Выбирите действие:");
                    Console.WriteLine("\t1 - Добавить сотрудника;");
                    Console.WriteLine("\t2 - Удалить сотрудника;");
                    Console.WriteLine("\t3 - Информация о сотруднике по ID;");
                    Console.WriteLine("\t4 - Показать список сотрудников;");
                    Console.WriteLine("\t5 - выход");
                    
                }
                str = Console.ReadLine();
                switch (str)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        RemoveEmployee();
                        break;
                    case "3":
                        GetEmployeeById();
                        break;
                    case "4":
                        GetEmployees();
                        break;
                    case "5":
                        Exit(ser);
                        return;
                    default:
                        Console.WriteLine("Введите одну из команд!");
                        Console.WriteLine();
                        continue;
                }
            }
        }

        private void AddEmployee()
        {
            Employee emp;
            Console.WriteLine("Введите имя:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Введите фамилию:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Введите должность:");
            string position = Console.ReadLine();              
            if (employees.Count == 0)
            {
                emp = new Employee(firstName, lastName, position, 1);
            }
            else
            {
                emp = new Employee(firstName, lastName, position, employees.Last().ID + 1);
            }            
            employees.Add(emp);
            Console.WriteLine("Сотруник успешно добавлен. ");
            Console.WriteLine("------");
            
        }

        private void RemoveEmployee()
        {
            int delId = 0;
            Console.WriteLine("Введите ID сотрудника для удаления его из базы данных:");
            string str = Console.ReadLine();
            try
            {
                delId = Int32.Parse(str);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Введите число! ");
                Console.WriteLine("------");
                
                return;
            }
            foreach (Employee e in employees)
            {
                if (e.ID == delId)
                {
                    employees.Remove(e);
                    Console.WriteLine("Сотрудник {0} был удален", e);
                    Console.WriteLine("------");
                    return;
                }
            }
            Console.WriteLine("Такого сотрудника не существует!", delId);
            Console.WriteLine("------");
            
        }

        private void GetEmployeeById()
        {
            int empId = 0;
            Console.WriteLine("Введите ID сотрудника:");
            string str = Console.ReadLine();
            try
            {
                empId = Int32.Parse(str);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Введите число!");
                Console.WriteLine("------");
               
                return;
            }
            foreach (Employee e in employees)
            {
                if (e.ID == empId)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("------");
                    
                    return;
                }
            }
            Console.WriteLine("Сотрудудника с c ID = {0} не существует. ", empId);
            Console.WriteLine("------");
        }

        private void GetEmployees()
        {
            foreach (Employee e in employees)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("------");
           
        }

        private void Exit(ISerializable ser)
        {
            ser.Write(employees);
        }
    }
}
