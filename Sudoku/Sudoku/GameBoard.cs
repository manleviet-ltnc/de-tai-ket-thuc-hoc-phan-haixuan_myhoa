using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Sudoku
{
    public partial class GameBoard : Form
    {
        Thread thread;
        TextBox[,] textBox = new TextBox[9, 9];
        int level = 0;            // Tăng level để qua màn khác
        int count = 0;            // Đếm xem có những ô nào nhập sai
        string fileName;          // File đề và đáp án của các level
        string line;              // Đọc từng dòng trong file đề và đáp án của các level
        bool hasStarted = false;  // Kiểm tra đã chơi game chưa
        Result result;

        public GameBoard()
        {
            InitializeComponent();
        }

        private void GameBoard_Load(object sender, EventArgs e)
        {
            TextBoxList();
            btnOK.Hide();
        }

        void TextBoxList()
        {
            textBox[0, 0] = txt00;
            textBox[1, 0] = txt10;
            textBox[2, 0] = txt20;
            textBox[3, 0] = txt30;
            textBox[4, 0] = txt40;
            textBox[5, 0] = txt50;
            textBox[6, 0] = txt60;
            textBox[7, 0] = txt70;
            textBox[8, 0] = txt80;
            textBox[0, 1] = txt01;
            textBox[1, 1] = txt11;
            textBox[2, 1] = txt21;
            textBox[3, 1] = txt31;
            textBox[4, 1] = txt41;
            textBox[5, 1] = txt51;
            textBox[6, 1] = txt61;
            textBox[7, 1] = txt71;
            textBox[8, 1] = txt81;
            textBox[0, 2] = txt02;
            textBox[1, 2] = txt12;
            textBox[2, 2] = txt22;
            textBox[3, 2] = txt32;
            textBox[4, 2] = txt42;
            textBox[5, 2] = txt52;
            textBox[6, 2] = txt62;
            textBox[7, 2] = txt72;
            textBox[8, 2] = txt82;
            textBox[0, 3] = txt03;
            textBox[1, 3] = txt13;
            textBox[2, 3] = txt23;
            textBox[3, 3] = txt33;
            textBox[4, 3] = txt43;
            textBox[5, 3] = txt53;
            textBox[6, 3] = txt63;
            textBox[7, 3] = txt73;
            textBox[8, 3] = txt83;
            textBox[0, 4] = txt04;
            textBox[1, 4] = txt14;
            textBox[2, 4] = txt24;
            textBox[3, 4] = txt34;
            textBox[4, 4] = txt44;
            textBox[5, 4] = txt54;
            textBox[6, 4] = txt64;
            textBox[7, 4] = txt74;
            textBox[8, 4] = txt84;
            textBox[0, 5] = txt05;
            textBox[1, 5] = txt15;
            textBox[2, 5] = txt25;
            textBox[3, 5] = txt35;
            textBox[4, 5] = txt45;
            textBox[5, 5] = txt55;
            textBox[6, 5] = txt65;
            textBox[7, 5] = txt75;
            textBox[8, 5] = txt85;
            textBox[0, 6] = txt06;
            textBox[1, 6] = txt16;
            textBox[2, 6] = txt26;
            textBox[3, 6] = txt36;
            textBox[4, 6] = txt46;
            textBox[5, 6] = txt56;
            textBox[6, 6] = txt66;
            textBox[7, 6] = txt76;
            textBox[8, 6] = txt86;
            textBox[0, 7] = txt07;
            textBox[1, 7] = txt17;
            textBox[2, 7] = txt27;
            textBox[3, 7] = txt37;
            textBox[4, 7] = txt47;
            textBox[5, 7] = txt57;
            textBox[6, 7] = txt67;
            textBox[7, 7] = txt77;
            textBox[8, 7] = txt87;
            textBox[0, 8] = txt08;
            textBox[1, 8] = txt18;
            textBox[2, 8] = txt28;
            textBox[3, 8] = txt38;
            textBox[4, 8] = txt48;
            textBox[5, 8] = txt58;
            textBox[6, 8] = txt68;
            textBox[7, 8] = txt78;
            textBox[8, 8] = txt88;

            // Quy định tối đa mỗi ô chỉ được nhập 1 số
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    textBox[i, j].MaxLength = 1;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    for (int i = 0; i < 9; i++)
                        for (int j = 0; j < 9; j++)
                            if (i > 0 && textBox[i, j].Focused)
                            {
                                textBox[i - 1, j].Focus();
                                return true;
                            }

                    if (btnBack.Focused)
                        txt80.Focus();

                    if (btnPlay.Focused || btnOK.Focused)
                        txt88.Focus();
                    return true;

                case Keys.Down:
                    for (int i = 0; i < 9; i++)
                        for (int j = 0; j < 9; j++)
                            if (i < 8 && textBox[i, j].Focused)
                            {
                                textBox[i + 1, j].Focus();
                                return true;
                            }

                    if (txt80.Focused)
                    {
                        btnBack.Focus();
                        return true;
                    }

                    if (txt84.Focused)
                    {
                        btnRankList.Focus();
                        return true;
                    }

                    if (txt88.Focused)
                    {
                        if (btnOK.Visible == true)
                            btnOK.Focus();
                        else
                            btnPlay.Focus();
                        return true;
                    }

                    if (btnBack.Focused || btnPlay.Focused || btnOK.Focused)
                        return true;

                    break;
                case Keys.Right:
                    for (int i = 0; i < 9; i++)
                        for (int j = 0; j < 9; j++)
                            if (j < 8 && textBox[i, j].Focused)
                            {
                                textBox[i, j + 1].Focus();
                                return true;
                            }

                    if (btnBack.Focused)
                    {
                        btnRankList.Focus();
                        return true;
                    }

                    if (btnRankList.Focused)
                    {
                        if (btnOK.Visible == false)
                            btnPlay.Focus();
                        else
                            btnOK.Focus();
                        return true;
                    }

                    if (btnPlay.Focused || btnOK.Focused)
                        return true;

                    break;
                case Keys.Left:
                    for (int i = 0; i < 9; i++)
                        for (int j = 0; j < 9; j++)
                            if (j > 0 && textBox[i, j].Focused)
                            {
                                textBox[i, j - 1].Focus();
                                return true;
                            }

                    if (btnPlay.Focused || btnOK.Focused)
                    {
                        btnRankList.Focus();
                        return true;
                    }

                    if (btnRankList.Focused)
                    {
                        btnBack.Focus();
                        return true;
                    }

                    if (btnBack.Focused)
                        return true;

                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            timer.Stop();
            Hide();

            if (hasStarted)
                ReturnMenu();
            else
                ReturnMenu();
        }

        void ReturnMenu()
        {
            Close();
            thread = new Thread(OpenMenu);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        void OpenMenu()
        {
            Application.Run(new NewGame());
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            hasStarted = true;
            lblMinute.Show();
            lblColon.Show();
            lblSecond.Show();
            btnOK.Show();
            btnPlay.Hide();
            level++;
            if (level == 1)
            {
                lblLevel.Show();
                result = new Result(level, 0);
                fileName = "level1.txt";
                LoadLevel(fileName);
            }
            else if (level == 2)
            {
                lblLevel.Text = "Level 2";
                fileName = "level2.txt";
                LoadLevel(fileName);
            }
            else if (level == 3)
            {
                lblLevel.Text = "Level 3";
                fileName = "level3.txt";
                LoadLevel(fileName);
            }
            else if (level == 4)
            {
                lblLevel.Text = "Level 4";
                fileName = "level4.txt";
                LoadLevel(fileName);
            }
            else if (level == 5)
            {
                lblLevel.Text = "Level 5";
                fileName = "level5.txt";
                LoadLevel(fileName);
            }
            else if (level == 6)
            {
                lblLevel.Text = "Level 6";
                fileName = "level6.txt";
                LoadLevel(fileName);
            }
            else if (level == 7)
            {
                lblLevel.Text = "Level 7";
                fileName = "level7.txt";
                LoadLevel(fileName);
            }
            else if (level == 8)
            {
                lblLevel.Text = "Level 8";
                fileName = "level8.txt";
                LoadLevel(fileName);
            }
            else if (level == 9)
            {
                lblLevel.Text = "Level 9";
                fileName = "level9.txt";
                LoadLevel(fileName);
            }
            else
            {
                lblLevel.Text = "Level 10";
                lblLevel.Left = (ClientSize.Width - lblLevel.Width) / 2;
                fileName = "level10.txt";
                LoadLevel(fileName);
            }

            timer.Enabled = true;
        }

        void LoadLevel(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            int k = 0;
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                // Chia các ký tự quanh ký tự "|" thành nhiều phần vào đưa vào các chỉ số trong mảng info qua mỗi lần đọc
                string[] info = line.Split('|');
                if (info.Length == 9)
                {
                    for (int i = k; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            // Gán các giá trị trong mảng info vào các từng dòng của textBox
                            textBox[i, j].Text = info[j];

                            // Những ô có số không được nhập thêm
                            if (textBox[i, j].Text != "")
                                textBox[i, j].BackColor = Color.Gainsboro;
                            else
                            {
                                textBox[i, j].BackColor = Color.White;
                                textBox[i, j].ReadOnly = false;
                            }
                        }

                        break;
                    }
                }
                k++;
            }
            sr.Close();
        }

        void CheckResult(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            int k = 0;
            while ((line = sr.ReadLine()) != "-")
                continue;

            while ((line = sr.ReadLine()) != null)
            {
                string[] info = line.Split('|');
                if (info.Length == 9)
                {
                    // Kiểm tra kết quả
                    for (int i = k; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            // Những ô nhập sai gán cho màu đỏ
                            if (textBox[i, j].Text != info[j])
                            {
                                textBox[i, j].BackColor = Color.Red;
                                count++;
                            }
                        }
                        break;
                    }
                }
                k++;
            }
            sr.Close();

            if (count == 0)
            {
                timer.Stop();
                btnOK.Hide();
                btnPlay.Show();

                // Kết thúc level không cho sửa đổi bất kì ô nào
                MessageBox.Show("Chúc mừng bạn đã vượt qua " + lblLevel.Text + "!");
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        textBox[i, j].BackColor = Color.Gainsboro;
                        textBox[i, j].ReadOnly = true;
                    }

                int minute = int.Parse(lblMinute.Text);
                int second = int.Parse(lblSecond.Text);
                result.Level = level;
                result.Time += (10 - minute) * 60 + (60 - second);

                lblMinute.Text = "10";
                lblSecond.Text = "00";

                lblMinute.Hide();
                lblColon.Hide();
                lblSecond.Hide();
            }

            count = 0;

            if (level == 10)
            {
                Rank r = new Rank(result);
                r.ShowDialog();
                ReturnMenu();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CheckResult(fileName);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int minute = int.Parse(lblMinute.Text);
            int second = int.Parse(lblSecond.Text);
            second--;

            if (second < 0)
            {
                second = 59;
                minute--;
            }

            if (minute < 10)
                lblMinute.Text = "  " + minute;
            else
                lblMinute.Text = minute + "";

            if (second < 10)
                lblSecond.Text = "0" + second;
            else
                lblSecond.Text = second + "";

            if (minute == 0 && second == 0)
            {
                MessageBox.Show("Bạn đã thua!");
                Rank r = new Rank();
                r.ShowDialog();
                ReturnMenu();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timer.Stop();
            Hide();

            if (hasStarted)
            {
                DialogResult result = MessageBox.Show("Are you want to exit the game?", "",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Chọn yes thì thoát
                    e.Cancel = false;
                }
                else if (result == DialogResult.No)
                {
                    // Chọn no thì tiếp tục chơi
                    e.Cancel = true;
                    if (level != 0)
                    {
                        Show();
                        timer.Start();
                    }
                }
            }
        }

        private void btnRankList_Click(object sender, EventArgs e)
        {
            Close();
            thread = new Thread(OpenRank);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        void OpenRank()
        {
            Application.Run(new Rank());
        }
    }
}