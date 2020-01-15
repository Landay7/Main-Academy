using ConsoleApp2;
using System;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        

        static int count = 0;

        static void Method4()
        {
            Console.WriteLine(count++);
            Method4();
        }

        static void Method1()
        {
            File.AppendAllText("test.txt", "Hello\r\n");
            Method2();
        }

        static void Method2()
        {
            File.AppendAllText("test.txt", "hi\r\n");
            Method3();
        }

        static void Method3()
        {
            File.AppendAllText("test.txt", "Goodbye\r\n");
            count++;
            if(count == 4)
            {
                File.AppendAllText("test.txt", "Die die!\r\n");
                throw new MyPerfectException();
            }
            Method1();
        }

        class MyPerfectException: Exception
        {
            public void ProcessStackTrace()
            {
                string trace = StackTrace;
                string[] separators = new string[] { "\r\n" };
                string[] traceLines = trace.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                foreach(var line in traceLines)
                {
                    int index = line.LastIndexOf('\\');

                    string substring = line.Substring(index + 1);
                    StringBuilder sb = new StringBuilder();
                    string[] info = substring.Split(':');
                    var lineWithNumber = info[1].Split(); // использует пробел по умолчанию
                    sb.Append(info[0])
                        .Append(" ошибка в строке ")
                        .AppendLine(lineWithNumber[1]);

                    File.AppendAllText("trace.txt", sb.ToString());
                }

                File.AppendAllText("trace1.txt", StackTrace);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Method1();
            }
            catch (MyPerfectException e)
            {
                e.ProcessStackTrace();
            }
            catch (Exception e)
            {
                File.AppendAllText("test.txt", e.Message.ToString() + "\r\n");
                File.AppendAllText("test.txt", e.StackTrace);
                Console.WriteLine("Ooops");
            }

            Console.ReadKey();
        }
    }
}
