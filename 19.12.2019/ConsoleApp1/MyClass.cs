using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    class Indexed_students
    {
        public static int stud_cnt = 8;
        private string[] stud_list = new string[stud_cnt];

        public Indexed_students()
        {
            for (int j = 0; j < stud_cnt; j++)
                stud_list[j] = "C# student";
        }

        private bool IsInRange(int index_var) => index_var >= 0 && index_var <= stud_cnt - 1;

        public string this[int index_var]
        {
            get 
            {
                if (IsInRange(index_var))
                {
                    return stud_list[index_var];
                }
                return "";
            }
            set
            {
                if (!IsInRange(index_var)) return;
                stud_list[index_var] = value;   
            }
        }
    }
}