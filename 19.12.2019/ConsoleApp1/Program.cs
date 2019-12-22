using ConsoleApp2;
using System;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        interface ILogger
        {
            void Log(string info);
        }

        class Database : ILogger
        {
            public void Write(object conn, string info)
            {
                File.AppendAllText(conn.ToString(), info +"\r\n");
            }

            public void Log(string info)
            {
                Write(Connection, info);
            }

            object Connection { get; set; }
        }

        class Cloud : ILogger
        {
            public void Log(string info)
            {
                Upload(Connection, Protocol, IsEncrypted, info);
            }

            public void Upload(object conn, string protocol, bool isEncrypted, string info)
            {
                File.AppendAllText(conn.ToString(), protocol + " " + isEncrypted + " " + info + "\r\n");
            }

            public string Connection { get; set; }
            public string Protocol { get; set; }
            public bool IsEncrypted { get; set; }

        }

        enum Destination
        {
            Database,
            File,
            Cloud
        }

        class FileLogger : ILogger
        {
            private const string fileName = "log.txt";

            public void Log(string info)
            {
                File.AppendAllText(fileName, DateTime.UtcNow.ToString() + " " + info + "\r\n");
            }
        }

        enum Status
        {
            Fatal,
            Bad,
            Normal,
            Good
        }

        struct Student
        {
            public Status StudentStatus;
            public string Fio;
            public string Group;
            public int Age;
        }


        class Journal
        {   
            public Journal(Student[] students, ILogger log)
            {
                Students = students;
                logger = log;
            }

            public void CalculateResults()
            {
                foreach(var student in Students)
                {
                    if(student.StudentStatus == Status.Bad)
                    {
                        logger.Log(string.Format("{0} из группі {1} нужно исключить", student.Fio, student.Group));
                    }
                    else if (student.StudentStatus == Status.Fatal)
                    {
                        logger.Log(string.Format("{0} из группі {1} исключаем", student.Fio, student.Group));
                    }
                    else if (student.StudentStatus == Status.Good)
                    {
                        logger.Log(string.Format("{0} из группі {1} молодец", student.Fio, student.Group));
                    }
                }
            }

            private Student[] Students;
            private ILogger logger;
        }



        static void Main(string[] args)
        {
            const int studentsCount = 200;
            Student[] students = new Student[studentsCount];
            Random rnd = new Random();
            string[] names = { "Андрей", "Вася", "Петя", "Саша", "Сергей", "Кирилл", "Юля", "Вика", "Катя" };
            string[] surnames = { "Иванов", "Петров", "Сидоров", "Боголюбов", "Ландау", "Коломойский", "Никитин", "Федоров", "Тютюник" };
            for (int i = 0; i < studentsCount; i++)
            {
                students[i].Age = rnd.Next(17, 65);
                students[i].Fio = string.Format("{1} {0}", 
                    surnames[rnd.Next(0, surnames.Length)], 
                    names[rnd.Next(0, names.Length)]);
                students[i].Group = "PHP";
                students[i].StudentStatus = (Status)rnd.Next(0, 4);
            }
            Console.WriteLine("{0} {1} years in group {2} has {3} status", students[0].Fio, students[0].Age, students[0].Group, students[0].StudentStatus);
            var cloudLogger = new Cloud()
            {
                Connection = "cloud.txt",
                IsEncrypted = false,
                Protocol = "http"
            };
            Journal journal = new Journal(students, cloudLogger);
            journal.CalculateResults();
            Console.ReadKey();
        }
    }
}
