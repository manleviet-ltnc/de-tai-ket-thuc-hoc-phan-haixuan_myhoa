using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Sudoku
{
    public partial class Rank : Form
    {
        Label[] lblLevel = new Label[10];
        Label[] lblTime = new Label[10];
        Result result;
        List<Result> resultList = new List<Result>();

        public Rank()
        {
            InitializeComponent();
        }

        public Rank(Result r)
        {
            InitializeComponent();
            result = r;
        }

        void LoadLabel()
        {
            lblLevel[0] = lblLevelTop0;
            lblLevel[1] = lblLevelTop1;
            lblLevel[2] = lblLevelTop2;
            lblLevel[3] = lblLevelTop3;
            lblLevel[4] = lblLevelTop4;
            lblLevel[5] = lblLevelTop5;
            lblLevel[6] = lblLevelTop6;
            lblLevel[7] = lblLevelTop7;
            lblLevel[8] = lblLevelTop8;
            lblLevel[9] = lblLevelTop9;

            lblTime[0] = lblTimeTop0;
            lblTime[1] = lblTimeTop1;
            lblTime[2] = lblTimeTop2;
            lblTime[3] = lblTimeTop3;
            lblTime[4] = lblTimeTop4;
            lblTime[5] = lblTimeTop5;
            lblTime[6] = lblTimeTop6;
            lblTime[7] = lblTimeTop7;
            lblTime[8] = lblTimeTop8;
            lblTime[9] = lblTimeTop9;
        }

        void LoadTop()
        {
            StreamReader sr = new StreamReader("rank.txt");
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] inputs = line.Split('|');
                int level = int.Parse(inputs[0]);
                string[] time = inputs[1].Split(':');
                int m = int.Parse(time[0]);
                int s = int.Parse(time[1]);
                int t = m * 60 + s;
                resultList.Add(new Result(level, t));
            }

            sr.Close();
        }

        void LoadScored()
        {
            for (int i = 0; i < resultList.Count; i++)
            {
                lblLevel[i].Text = "Level " + resultList[i].Level;
                lblTime[i].Text = resultList[i].Time / 60 + ":" + resultList[i].Time % 60;
            }

            SortRank();
            for (int i = 0; i < resultList.Count; i++)
            {
                lblLevel[i].Text = "Level " + resultList[i].Level;
                int m = resultList[i].Time / 60;
                int s = resultList[i].Time % 60;

                string minute;
                string second;
                if (m < 10)
                    minute = "  " + m;
                else
                    minute = m + "";

                if (s < 10)
                    second = "0" + s;
                else
                    second = s + "";

                lblTime[i].Text = minute + ":" + second;
            }
        }

        void SortRank()
        {
            for (int i = 0; i < resultList.Count - 1; i++)
            {
                for (int j = i + 1; j < resultList.Count; j++)
                {
                    if (resultList[i].Level == resultList[j].Level)
                    {
                        if (resultList[i].Time > resultList[j].Time)
                        {
                            int t = resultList[i].Time;
                            resultList[i].Time = resultList[j].Time;
                            resultList[j].Time = t;
                        }
                    }
                    else if (resultList[i].Level < resultList[j].Level)
                    {
                        int t = resultList[i].Level;
                        resultList[i].Level = resultList[j].Level;
                        resultList[j].Level = t;

                        int temp = resultList[i].Time;
                        resultList[i].Time = resultList[j].Time;
                        resultList[j].Time = temp;
                    }
                }
            }
        }

        private void Rank_Load(object sender, EventArgs e)
        {
            LoadLabel();
            LoadTop();
            LoadScored();
            if (result != null)
            {
                resultList.Add(result);

                SortRank();
                if(resultList.Count > 10)
                    resultList.RemoveAt(10);

                StreamWriter sw = null;
                for (int i = 0; i < 10; i++)
                {
                    if (i == 0)
                        sw = new StreamWriter("rank.txt");
                    else
                        sw = File.AppendText("rank.txt");

                    lblLevel[i].Text = resultList[i].Level + "";
                    lblTime[i].Text = resultList[i].Time + "";
                    sw.WriteLine(lblLevel[i].Text + "|" + (resultList[i].Time / 60) + ":" + (resultList[i].Time % 60));
                    sw.Close();
                }
            }
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}