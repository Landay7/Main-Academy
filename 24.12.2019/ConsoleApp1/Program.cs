using ConsoleApp2;
using System;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Swap(ref MyClass a, ref MyClass b)
        {
            var tmp = a.Num;
            a.Num = b.Num;
            b.Num = tmp;
        }

        


        static void Swap(MyClass a, MyClass b)
        {
            var tmp = a.Num;
            a.Num = b.Num;
            b.Num = tmp;
        }

        class MyClass
        {
            static readonly string path = GetPath();
            static readonly string path1 = "bbb";
            static readonly string path2;

            static string GetPath()
            {
                return "aaa";
            }

            static MyClass()
            {
                path2 = "ccc";
            }

            public static int i = 0;
    
            public MyClass(int n)
            {
                Num = n;
            }

            public MyClass(int n, DateTime m)
                : this(n)
            {
                date = m;
            }

            public MyClass(params int[] n)
            {
                for(int i = 0; i < n.Length; i++)
                {
                    Num += n[i];
                }
            }

            public void NewValue(int n, out int m)
            {
                m = Num + n;
            }


            public override String ToString()
            {
                i++;
                return Num.ToString();
            }

            public DateTime date {
                get {
                    return (date + TimeSpan.FromDays(1));
                }
                set { }
            }
            public int Num;

        }



        static void Main(string[] args)
        {
            Random rnd = new Random();
            MyClass a = new MyClass(3, 4, 5, 6, 10);
            MyClass b = new MyClass(1);
            b.NewValue(10, out int m);
            Console.WriteLine($"a = {a}, b = {b}, m = {m}");
            Console.ReadKey();
            Swap(ref a, ref b);
            Console.WriteLine($"a = {a}, b = {b}");
            Console.ReadKey();
            Swap(a, b);
            Console.WriteLine($"a = {a}, b = {b}");
            Console.WriteLine(MyClass.i);
            Console.ReadKey();
            DateTime baseDate = new DateTime(2019, 12, 24, 7, 8, 0);
            DateTime[] schedule = new DateTime[10];
            for(int i = 0; i < 10; i++)
            {
                int val = rnd.Next(1, 15);
                Console.Write($"rnd = {val}  ");
                schedule[i] = baseDate + TimeSpan.FromHours(val);
                Console.WriteLine(schedule[i]);
            }
            Console.ReadKey();
        }
    }
}
