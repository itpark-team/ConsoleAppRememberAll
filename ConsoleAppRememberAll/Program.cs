using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRememberAll
{
    internal class Program
    {
        enum Gender
        {
            Man = 1,
            Woman = 0
        }

        struct Person
        {
            public Gender Gender { get; set; }
            public int Salary { get; set; }
        }

        static Person[] GetAllPersonsFromFile(string fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            int countPersons = int.Parse(reader.ReadLine());

            Person[] persons = new Person[countPersons];
            for (int i = 0; i < countPersons; i++)
            {
                string currentLine = reader.ReadLine();
                string[] values = currentLine.Split(' ');

                persons[i] = new Person();
                persons[i].Gender = (Gender)int.Parse(values[0]);
                persons[i].Salary = int.Parse(values[1]);
            }

            reader.Close();

            return persons;
        }

        static double GetAvgSalaryOfPersons(Person[] persons, Gender gender)
        {
            int sumSalary = 0;
            int countPersons = 0;

            for (int i = 0; i < persons.Length; i++)
            {
                if (persons[i].Gender == gender)
                {
                    countPersons++;
                    sumSalary += persons[i].Salary;
                }
            }

            return sumSalary / (double)countPersons;
        }


        static void SaveAvgValueToFile(string fileName, double value)
        {
            StreamWriter writer = new StreamWriter(fileName);
            writer.Write(value);
            writer.Close();
        }

        static int InputIntValue(string message, int minValue, int maxValue)
        {
            int value;
            bool inputResult;

            do
            {
                inputResult = true;

                Console.Write(message);
                inputResult = int.TryParse(Console.ReadLine(), out value);

                if (inputResult == false)
                {
                    Console.WriteLine("Ошибка ввода. Вы ввели НЕ число!");
                }
                else
                {
                    if (value < minValue || value > maxValue)
                    {
                        inputResult = false;
                        Console.WriteLine("Ошибка ввода. Вы ввели число за пределами диапазона!");
                    }
                }

            } while (inputResult == false);


            return value;
        }


        static void Main(string[] args)
        {
            Person[] persons = GetAllPersonsFromFile("persons.txt");

            int choosenAction = InputIntValue("выберите кого посчитать мужчин(1) или женщин(0): ", 0, 1);

            switch (choosenAction)
            {
                case 1:
                    double avgSalaryOfMan = GetAvgSalaryOfPersons(persons, Gender.Man);

                    SaveAvgValueToFile("man.txt", avgSalaryOfMan);

                    Console.WriteLine("Среднее значение зп мужчин успешно посчитано и записано в файл");

                    break;

                case 0:
                    double avgSalaryOfWoman = GetAvgSalaryOfPersons(persons, Gender.Woman);

                    SaveAvgValueToFile("woman.txt", avgSalaryOfWoman);

                    Console.WriteLine("Среднее значение зп женщин успешно посчитано и записано в файл");
                    break;
            }

            Console.ReadKey();
        }
    }
}
