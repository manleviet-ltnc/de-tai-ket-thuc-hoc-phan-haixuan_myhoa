using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class SDK
    {
        int index = 0, add = 1, i = 0, j = 0;
        public int MaxIndexResultCanFind;
        public int[] Result = new int[82];
        public int MaxIndex
        {
            get { return MaxIndexResultCanFind; }
            set { MaxIndexResultCanFind = value; }
        }
        bool HaveResult = true;
        #region Các hàm chung
        //=======================================================================================
        const int size = 9;
        int tox(int x)
        {
            return (x - 1) / size + 1;
        }
        int toj(int x)
        {
            return (x - 1) % size + 1;
        }
        int tou(int x, int y)
        {
            return (x - 1) * size + y;
        }
        //=====================================================================================
        int[,] row = new int[10, 10];
        int[,] collum = new int[10, 10];
        int[,] area = new int[10, 10];
        int[,] AREA = new int[10, 10];

        int[, ,] agree = new int[size + 1, size + 1, 11];
        int[,] value = new int[size + 1, size + 1];
        int[,] Area = new int[size + 1, size + 1];
        int[,] problem = new int[size + 1, size + 1];
        //=====================================================================================
        int[] stack = new int[500];
        int top = 0;
        void push(int a)
        {
            top++;
            stack[top] = a;
        }
        int pop()
        {
            top--;
            return stack[top + 1];
        }
        //======================================================================================
        void initilizing()
        {
            int i, j, k;
            for (i = 1; i <= size; i++) for (j = 1; j <= size; j++) Area[i, j] = 0;
            for (i = 1; i <= size; i++)
                for (j = 1; j <= size; j++)
                {
                    row[i, j] = collum[i, j] = area[i, j] = 1;
                    AREA[i, j] = 1 + ((i - 1) / 3) * 3 + (j - 1) / 3;
                    for (k = 1; k <= size; k++) agree[i, j, k] = k;
                    agree[i, j, 10] = problem[i, j] = 0;
                    agree[i, j, 0] = size;
                    problem[i, j] = value[i, j] = 0;
                    Area[AREA[i, j], 0]++;
                    Area[AREA[i, j], Area[AREA[i, j], 0]] = tou(i, j);
                }
        }//Remove a number  from list of available number
        bool remove(int i, int j, int value)
        {
            int k = 1;
            while ((agree[i, j, k] < value) && (k <= agree[i, j, 0])) k++;
            if ((agree[i, j, k] == value) && (k <= agree[i, j, 0]))
            {
                while (agree[i, j, k] != 0)
                {
                    agree[i, j, k] = agree[i, j, k + 1];
                    k++;
                }
                agree[i, j, 0]--;
                return agree[i, j, 0] == 1;
            }
            return false;
        }
        void effectFromANumber(int i, int j)
        {
            int u, v;
            for (u = 1; u <= size; u++)
                for (v = 1; v <= size; v++)
                {
                    if (!((u == i) && (v == j)))
                        if ((u == i) || (v == j) || (AREA[u, v] == AREA[i, j]))//
                            if (remove(u, v, agree[i, j, 1]))
                                push(tou(u, v));
                }
        }
        bool exit(int i, int j, int v)
        {
            int k;
            for (k = 1; k <= agree[i, j, 0]; k++) if (agree[i, j, k] == v) return true;
            return false;
        }
        void setValue(int i, int j, int newValue)
        {
            int k;
            agree[i, j, 1] = newValue;
            agree[i, j, 0] = 1;
            for (k = 2; k <= 10; k++) agree[i, j, k] = 0;
            row[i, newValue] = collum[j, newValue] = area[AREA[i, j], newValue] = 0;
            value[i, j] = 1;
        }
        void checkrow()
        {
            int i, j, value, o = 0, count = 0;
            for (i = 1; i <= size; i++) for (value = 1; value <= 9; value++)
                {
                    if (row[i, value] == 0) continue;
                    for (count = 0, j = 1; j <= size; j++)
                        if (exit(i, j, value))
                        {
                            o = j;
                            count++;
                        }
                    if ((count == 1))
                    {
                        setValue(i, o, value);
                        push(tou(i, o));
                    }
                }
        }
        void checkcollum()
        {
            int i, j, value, o = 0, count;
            for (j = 1; j <= size; j++) for (value = 1; value <= 9; value++)
                {
                    if (collum[j, value] == 0) continue;
                    for (count = 0, i = 1; i <= size; i++) if (exit(i, j, value))
                        {
                            o = i;
                            count++;
                        }
                    if ((count == 1) && (agree[o, j, 0] != 1))
                    {
                        setValue(o, j, value);
                        push(tou(o, j));
                    }
                }
        }
        void checkarea()
        {
            int k, value, o = 0, count, b, i, j;
            for (k = 1; k <= 9; k++) for (value = 1; value <= 9; value++)
                {
                    if (area[k, value] == 0) continue;
                    for (count = 0, b = 1; b <= size; b++)
                    {
                        i = tox(Area[k, b]);
                        j = toj(Area[k, b]);
                        if (exit(i, j, value)) { o = Area[k, b]; count++; }
                    }
                    if (count == 1)
                    {
                        setValue(tox(o), toj(o), value);
                        push(o);
                    }
                }
        }
        void preSolve()
        {
            int k;
            while (top != 0)
            {
                checkrow();
                checkcollum();
                checkarea();
                k = pop();
                effectFromANumber(tox(k), toj(k));
            };
        }

        int Solve(int count)
        {
            int ResultCount = 0;
            do
            {
                index += add;
                if (index == size * size + 1)
                {
                    ResultCount++;
                    add = -1;
                    if (count == -1)//Khi cần đếm số nghiệm
                    {
                        if (ResultCount > MaxIndexResultCanFind)
                            return -1;//Nếu như có quá nhiều nghiệm thì thôi
                        continue;
                    }
                    else
                    {
                        if (ResultCount >= count)//Nếu đã tìm thấy nghiệm thứ count
                        {
                            for (int ii = 1; ii <= 9; ii++)
                                for (int jj = 1; jj <= 9; jj++)
                                    Result[tou(ii, jj)] = agree[ii, jj, value[ii, jj]];
                            return 1;
                        }
                    }
                    continue;//Khi cần tìm tiếp nghiệm khác
                }
                i = tox(index); j = toj(index);
                if (agree[i, j, 0] == 1) continue;
                if (value[i, j] != 0) row[i, agree[i, j, value[i, j]]] = collum[j, agree[i, j, value[i, j]]] = area[AREA[i, j], agree[i, j, value[i, j]]] = 1;
                if ((add < 0) && (value[i, j] == agree[i, j, 0]))
                {
                    value[i, j] = 0; continue;
                }
                for (value[i, j]++; value[i, j] <= agree[i, j, 0]; value[i, j]++)
                    if ((row[i, agree[i, j, value[i, j]]] != 0) && (collum[j, agree[i, j, value[i, j]]] != 0) && (area[AREA[i, j], agree[i, j, value[i, j]]] != 0))
                    {
                        row[i, agree[i, j, value[i, j]]] = collum[j, agree[i, j, value[i, j]]] = area[AREA[i, j], agree[i, j, value[i, j]]] = 0;
                        add = 1; break;
                    }
                if (value[i, j] > agree[i, j, 0])
                {
                    value[i, j] = 0; add = -1;
                }
            } while (index + add > 0);
            return count == -1 ? ResultCount : 0;
        }

        //=====================================================================================================
        private int[] pro = new int[82];
        private bool inputData()
        {
            bool run = true;
            int i, j, a;
            for (i = 1; i <= size; i++)
                for (j = 1; j <= size; j++)
                {
                    a = pro[tou(i, j)];
                    if (a != 0)
                    {
                        if (!exit(i, j, a))// agree[i,j,1]
                            run = false;//Can not solve
                        setValue(i, j, a);
                        effectFromANumber(i, j);
                        push(tou(i, j));
                        problem[i, j] = a;
                    };
                }
            return run;
        }
        #endregion
        public SDK(int[] _pro)
        {
            initilizing();
            pro = _pro;
            MaxIndexResultCanFind = 10000;
            HaveResult = inputData();
            index = 0; add = 1; i = 0; j = 0;
            top = 1;// Top of stack
            preSolve();
        }
        public bool SolveFirst()//Beginning Solve by reseting the agruments and calling the Sove function
        {
            return (!HaveResult) ? false : Solve(0) == 1;
        }
        public bool SolveTo(int _index)
        {
            return (!HaveResult) ? false : Solve(_index) == 1;
        }
        public int ResultCount()
        {
            return (!HaveResult) ? 0 : Solve(-1);
        }
    }
}
