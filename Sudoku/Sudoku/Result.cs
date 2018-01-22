using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Result
    {
        int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        int time;
        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public Result(int level, int t)
        {
            Level = level;
            time = t;
        }

        public int CompareTo(object obj)
        {
            Result r = obj as Result;
            if (r != null)
            {
                if (Level < r.Level) return 1;
                if (Level > r.Level) return -1;
                else return 0;
            }
            throw new ArgumentException("not a Result");
        }
    }
}