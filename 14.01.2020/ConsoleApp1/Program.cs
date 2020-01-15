using ConsoleApp2;
using System;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        

        public static void DoWork(WorkCompleteCallBack callback)
        {
            callback += TestCallBack;
            callback("Hello");
        }

        public static void TestCallBack(string result)
        {
            Console.WriteLine(result);
        }

        public static void TestCallBack2(string result)
        {
            Console.WriteLine(result.ToUpper());
        }

        class B
        {
            private int a;
            public B(int A)
            {
                a = A;
            }
            public void TestCallBack3(string result)
            {
                Console.WriteLine(a + "  "+result.ToLower());
            }
        }
        
        public delegate void WorkCompleteCallBack(string result);
        static void Main(string[] args)
        {
            WorkCompleteCallBack c = TestCallBack;
            c += TestCallBack2;
            {
                B b = new B(30);
                c += b.TestCallBack3;
            }
            
            DoWork(c);
            Console.ReadKey();
            c("Hello");
            Console.ReadKey();
        }
    }
}
