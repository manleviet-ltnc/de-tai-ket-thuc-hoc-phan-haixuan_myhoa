﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        int dapAn;
        public Color ProblemFore = Color.Red;
        public Color ProblemBack = Color.Cyan;

        public Color ResultFore = Color.Black;
        public Color ResultBack = Color.White;


        public TextBox[] text = new TextBox[82];
        int[] de = new int[82];
        public void LoadText()
        {
            const int space = 10;
            for (int i = 1; i <= 81; i++)
            {
                text[i] = new TextBox();
                text[i].Size = txt05.Size;
                text[i].Font = txt05.Font;
                text[i].MaxLength = 1;
                text[i].TabIndex = i;

                text[i].Top = ((i - 1) / 9) % 9 * text[i].Height + 1 + ((i - 1) / 27) * space + space;
                text[i].Left = (i - 1) % 9 * text[i].Width + 1 + ((i - 1) % 9) / 3 * space + space;
                text[i].Show();
                text[i].TextAlign = HorizontalAlignment.Center;
                text[i].Name = "Text" + i.ToString();
                text[i].Tag = i;
                text[i].Text = "";
                text[i].ContextMenuStrip = PoppupMenu;
                this.text[i].TextChanged += new System.EventHandler(this.Mau_TextChanged);
                this.text[i].KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Mau_KeyPress);
                this.text[i].KeyDown += new KeyEventHandler(this.Mau_KeyDown);
                this.text[i].Enter += new System.EventHandler(this.Mau_Enter);
                myPanel.Controls.Add(text[i]);
            }
            myTool.Top = myMenu.Height;
            myPanel.Left = 0;
            myPanel.Top = myTool.Top + myTool.Height;
            myPanel.Height = text[81].Top + text[81].Height + space;
            myPanel.Width = text[81].Left + text[81].Width + space;
            this.Width = myPanel.Width + 6;
            this.Height = myPanel.Top + myPanel.Height + myMenu.Height + space - 2 + statusStrip1.Height;

        }
        private void LoadPictureTool()
        {
            toolNew.Image = mnuNew.Image;
            toolOpen.Image = mnuOpen.Image;
            toolSave.Image = mnuSave.Image;
            toolSolve.Image = mnuSolve.Image;
            toolSolveNext.Image = mnuSolveNext.Image;
            toolSolvePre.Image = mnuSolvePrevious.Image;
            toolSoveTo.Image = mnuSolveTo.Image;
            toolStatic.Image = mnuSatic.Image;
            toolInfo.Image = mnuInformation.Image;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadText();
            LoadPictureTool();
        }
        private void napde()
        {
            int i;
            for (i = 1; i <= 81; i++)
            {
                if ((text[i].Text == "") || (text[i].ForeColor == Color.Black)) de[i] = 0;
                else de[i] = int.Parse(text[i].Text);
            }
        }
        private void mnuNew_Click(object sender, EventArgs e)
        {
            dapAn = 0;
            myStatus.Text = "Sẳn sàng nhập dữ liệu";
            for (int i = 0; i < 81; i++)
            {
                text[i + 1].Text = "";
                text[i + 1].ForeColor = Color.Black;
                text[i + 1].BackColor = Color.White;
            }
            text[1].Focus();
        }
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            dapAn = 0;
            DiagOpen.InitialDirectory = Application.StartupPath;
            if (DiagOpen.ShowDialog() == DialogResult.Cancel) return;
            char x;
            try
            {
                FileStream file = new FileStream(DiagOpen.FileName, FileMode.Open);
                BinaryReader BR = new BinaryReader(file);
                for (int i = 0; i < 81; i++)
                {
                    do
                    {
                        x = BR.ReadChar();
                    } while (x < '0' || x > '9');
                    if (x > '0')
                    {
                        text[i + 1].Text = x.ToString();
                        text[i + 1].ForeColor = Color.Red;
                        text[i + 1].BackColor = Color.Cyan;
                    }
                    else
                    {
                        text[i + 1].Text = "";
                        text[i + 1].ForeColor = Color.Black;
                        text[i + 1].BackColor = Color.White;
                    }
                }
                BR.Close();
                file.Close();
                myStatus.Text = "Mở tập tin " + DiagOpen.FileName;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Lỗi khi đọc file !\n" + Ex.Message, "Đã có lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            };
        }
        private void mnuSave_Click(object sender, EventArgs e)
        {
            dapAn = 0;
            DiagSave.Filter = "Sudoku file(*.sdk)|*.sdk|Text file (*.txt)|*.txt";
            if (DiagSave.ShowDialog() == DialogResult.Cancel) return;
            try
            {
                FileStream file = new FileStream(DiagSave.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(file);

                for (int i = 0; i < 81; i++)
                {
                    bw.Write(text[i + 1].Text == "" ? "0" : text[i + 1].Text);
                }
                bw.Close();
                file.Close();
                myStatus.Text = "Đã lưu ra tập tin \"" + DiagSave.FileName + "\"";
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Có lỗi khi viết ra file !\n" + Ex.Message, "Có lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void mnuSatic_Click(object sender, EventArgs e)
        {
            string da = dapAn == 0 ? "Chưa xem đáp án nào." : "Đang xem đáp án thứ " + dapAn.ToString() + ".";
            int daDien = 0, chuaDien = 0;
            napde();
            for (int i = 0; i < 81; i++)
            {
                if (de[i + 1] != 0) daDien++;
            }
            chuaDien = 81 - daDien;
            SDK a = new SDK(de);
            Application.DoEvents();
            int count = a.ResultCount();
            if (count == 0)
                MessageBox.Show("Sudoku có " + daDien.ToString() + " ô được điền, " + chuaDien.ToString() + " còn trống.\n Sudoku này không có đáp án\n", "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (count > 0) MessageBox.Show("Sudoku có " + daDien.ToString() + " ô được điền, " + chuaDien.ToString() + " còn trống.\n Sudoku này có " + count.ToString() + " đáp án !", "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Sudoku có " + daDien.ToString() + " ô được điền, " + chuaDien.ToString() + " còn trống.\nSudoku này có nhiều hơn 10.000 đáp án !\n" + da, "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Mau_TextChanged(object sender, EventArgs e)
        {
            int i = int.Parse(((TextBox)sender).Tag.ToString());
            int r = ((i - 1) / 9) + 1;
            int c = (i - 1) % 9 + 1;
            text[i].SelectAll();
            text[i].ForeColor = Color.Red;
            text[i].BackColor = Color.Cyan;
            myStatus.Text = "Hàng " + r.ToString() + " Cột " + c.ToString();
        }
        private void Mau_KeyDown(object sender, KeyEventArgs e)
        {
            int Current = int.Parse((((TextBox)sender).Tag.ToString()));
            switch (e.KeyCode.ToString())
            {
                case "Up":
                    if (Current > 9) text[Current - 9].Focus();
                    else text[Current + 72].Focus();
                    break;
                case "Down":
                    if (Current < 73) text[Current + 9].Focus();
                    else text[Current - 72].Focus();
                    break;
                case "Left":
                    if (Current > 1) text[Current - 1].Focus();
                    break;
                case "Return":
                case "Right":
                    if (Current < 81) text[Current + 1].Focus();
                    break;
                case "Home":
                    text[9 * ((Current - 1) / 9) + 1].Focus();
                    break;
                case "End":
                    text[9 * ((Current - 1) / 9) + 9].Focus();
                    break;
                case "Delete":
                    text[Current].Text = "";
                    text[Current].ForeColor = Color.Black;
                    text[Current].BackColor = Color.White;
                    break;
                case "F2":
                    mnuSave_Click(null, null);
                    break;
                case "F3":
                    mnuOpen_Click(null, null);
                    break;
                case "F4":
                    mnuNew_Click(null, null);
                    break;
                case "F8":
                    mnuSatic_Click(null, null);
                    break;
                case "F9":
                    recovery_Click(null, null);
                    break;
                default:
                    break;
            }
        }
        private void Mau_Enter(object sender, EventArgs e)
        {
            int i = int.Parse(((TextBox)sender).Tag.ToString());
            int r = (i - 1) / 9 + 1;
            int c = (i - 1) % 9 + 1;
            myStatus.Text = "Hàng " + r.ToString() + " Cột " + c.ToString();
        }
        private void myPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Mau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ((TextBox)sender).Text = "";
                ((TextBox)sender).ForeColor = Color.Black;
                ((TextBox)sender).BackColor = Color.White;
            }
            if (e.KeyChar > '9' || e.KeyChar < '1') e.KeyChar = '\0';
        }
        private void recovery_Click(object sender, EventArgs e)
        {
            dapAn = 0;
            for (int i = 0; i < 81; i++)
            {
                if ((de[i + 1] == 0))
                {
                    text[i + 1].Text = "";
                    text[i + 1].BackColor = Color.White;
                    text[i + 1].ForeColor = Color.Black;
                }
            }
        }

        private void mnuOpen_Click_1(object sender, EventArgs e)
        {
            dapAn = 0;
            DiagOpen.InitialDirectory = Application.StartupPath;
            if (DiagOpen.ShowDialog() == DialogResult.Cancel) return;
            char x;
            try
            {
                FileStream file = new FileStream(DiagOpen.FileName, FileMode.Open);
                BinaryReader BR = new BinaryReader(file);
                for (int i = 0; i < 81; i++)
                {
                    do
                    {
                        x = BR.ReadChar();
                    } while (x < '0' || x > '9');
                    if (x > '0')
                    {
                        text[i + 1].Text = x.ToString();
                        text[i + 1].ForeColor = Color.Red;
                        text[i + 1].BackColor = Color.Cyan;
                    }
                    else
                    {
                        text[i + 1].Text = "";
                        text[i + 1].ForeColor = Color.Black;
                        text[i + 1].BackColor = Color.White;
                    }
                }
                BR.Close();
                file.Close();
                myStatus.Text = "Mở tập tin " + DiagOpen.FileName;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Lỗi khi đọc file !\n" + Ex.Message, "Đã có lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            };
        }

        private void mnuSave_Click_1(object sender, EventArgs e)
        {
            dapAn = 0;
            DiagSave.Filter = "Sudoku file(*.sdk)|*.sdk|Text file (*.txt)|*.txt";
            if (DiagSave.ShowDialog() == DialogResult.Cancel) return;
            try
            {
                FileStream file = new FileStream(DiagSave.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(file);

                for (int i = 0; i < 81; i++)
                {
                    bw.Write(text[i + 1].Text == "" ? "0" : text[i + 1].Text);
                }
                bw.Close();
                file.Close();
                myStatus.Text = "Đã lưu ra tập tin \"" + DiagSave.FileName + "\"";
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Có lỗi khi viết ra file !\n" + Ex.Message, "Có lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuNew_Click_1(object sender, EventArgs e)
        {
            dapAn = 0;
            myStatus.Text = "Sẳn sàng nhập dữ liệu";
            for (int i = 0; i < 81; i++)
            {
                text[i + 1].Text = "";
                text[i + 1].ForeColor = Color.Black;
                text[i + 1].BackColor = Color.White;
            }
            text[1].Focus();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuSolve_Click(object sender, EventArgs e)
        {
            dapAn = 1;
            this.napde();
            SDK a = new SDK(de);
            if (a.SolveFirst())
            {
                for (int i = 1; i <= 81; i++)
                {
                    text[i].Text = a.Result[i].ToString();
                    if (de[i] == 0)
                    {
                        text[i].BackColor = Color.White;
                        text[i].ForeColor = Color.Black;
                    }
                    myStatus.Text = "Đã giải đề thành công";
                }
            }
            else
            {
                MessageBox.Show("Không có kết quả nào cả !", "Vô nghiệm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                myStatus.Text = "Giải đề không thành công";
            }
        }

        private void mnuSolveTo_Click(object sender, EventArgs e)
        {
            SelectResult frm = new SelectResult();
            frm.TopMost = this.TopMost;
            frm.ShowDialog();
            int select = frm.selected;
            if (select == -1) return;
            napde();
            SDK a = new SDK(de);
            if (select > 10000)
            {
                lblwait.Visible = true;
                myStatus.Text = "Đang giải đề ... ";
                this.Enabled = false;
            }
            Application.DoEvents();
            if (!a.SolveTo(select))
            {
                if (select > 10000)
                {
                    this.Enabled = true;
                    lblwait.Visible = false;
                }
                MessageBox.Show("Bạn muốn xem nghiệm thứ " + select.ToString() + "\nNhưng Sudoku này có ít hơn " + select.ToString() + " nghiệm !\nHãy nhập vào số thích hợp.", "Không có nghiệm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                myStatus.Text = "Giải đề không thành công";
                Application.DoEvents();
                return;
            }
            if (select > 10000)
            {
                this.Enabled = true;
                lblwait.Visible = false;
            }
            dapAn = select;
            for (int i = 1; i <= 81; i++)
            {
                text[i].Text = a.Result[i].ToString();
                if (de[i] == 0)
                {
                    text[i].BackColor = Color.White;
                    text[i].ForeColor = Color.Black;
                }
            }
            myStatus.Text = "Đã giải đề thành công";
        }

        private void mnuSolvePrevious_Click(object sender, EventArgs e)
        {
            switch (dapAn)
            {
                case 0: MessageBox.Show("Bạn chưa giải đề !", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1: MessageBox.Show("Đây là đáp án đầu tiên !", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    dapAn--;
                    napde();
                    SDK a = new SDK(de);
                    if (dapAn > 10000)
                    {
                        lblwait.Visible = true; ;
                        this.Enabled = false;
                    }
                    a.SolveTo(dapAn);
                    if (dapAn > 10000)
                    {
                        lblwait.Visible = false;
                        this.Enabled = true;
                    }
                    for (int i = 1; i <= 81; i++)
                    {
                        text[i].Text = a.Result[i].ToString();
                        if (de[i] == 0)
                        {
                            text[i].BackColor = Color.White;
                            text[i].ForeColor = Color.Black;
                        }
                    }
                    break;
            }
        }

        private void mnuSolveNext_Click(object sender, EventArgs e)
        {
            dapAn++;
            napde();
            SDK a = new SDK(de);
            if (dapAn > 10000)
            {
                lblwait.Visible = true;
                this.Enabled = false;
            }
            Application.DoEvents();
            if (!a.SolveTo(dapAn))
            {
                if (dapAn > 10000)
                {
                    lblwait.Visible = false; ;
                    this.Enabled = true; ;
                }
                MessageBox.Show("Hết đáp án - Không tìm thấy đáp án tiếp theo !", "Không có nghiệm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dapAn--;
                return;
            }
            if (dapAn > 10000)
            {
                lblwait.Visible = false; ;
                this.Enabled = true; ;
            }
            for (int i = 1; i <= 81; i++)
            {
                text[i].Text = a.Result[i].ToString();
                if (de[i] == 0)
                {
                    text[i].BackColor = Color.White;
                    text[i].ForeColor = Color.Black;
                }
            }
        }

        private void recovery_Click_1(object sender, EventArgs e)
        {
            dapAn = 0;
            for (int i = 0; i < 81; i++)
            {
                if ((de[i + 1] == 0))
                {
                    text[i + 1].Text = "";
                    text[i + 1].BackColor = Color.White;
                    text[i + 1].ForeColor = Color.Black;
                }
            }
        }

        private void mnuSatic_Click_1(object sender, EventArgs e)
        {
            string da = dapAn == 0 ? "Chưa xem đáp án nào." : "Đang xem đáp án thứ " + dapAn.ToString() + ".";
            int daDien = 0, chuaDien = 0;
            napde();
            for (int i = 0; i < 81; i++)
            {
                if (de[i + 1] != 0) daDien++;
            }
            chuaDien = 81 - daDien;
            SDK a = new SDK(de);
            Application.DoEvents();
            int count = a.ResultCount();
            if (count == 0)
                MessageBox.Show("Sudoku có " + daDien.ToString() + " ô được điền, " + chuaDien.ToString() + " còn trống.\n Sudoku này không có đáp án\n", "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (count > 0) MessageBox.Show("Sudoku có " + daDien.ToString() + " ô được điền, " + chuaDien.ToString() + " còn trống.\n Sudoku này có " + count.ToString() + " đáp án !", "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Sudoku có " + daDien.ToString() + " ô được điền, " + chuaDien.ToString() + " còn trống.\nSudoku này có nhiều hơn 10.000 đáp án !\n" + da, "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sudokuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Điền đầy đủ các số từ 0~9 vào ô trống cho : \n\tTất cả các hàng :\n\tTất cả các cột : \n\tTất cả các vùng 3x3\nChỉ được điền một ô một số mà thôi !", "Sudoku", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolNew_Click(object sender, EventArgs e)
        {
            dapAn = 0;
            myStatus.Text = "Sẳn sàng nhập dữ liệu";
            for (int i = 0; i < 81; i++)
            {
                text[i + 1].Text = "";
                text[i + 1].ForeColor = Color.Black;
                text[i + 1].BackColor = Color.White;
            }
            text[1].Focus();
        }

        private void toolOpen_Click(object sender, EventArgs e)
        {
            dapAn = 0;
            DiagOpen.InitialDirectory = Application.StartupPath;
            if (DiagOpen.ShowDialog() == DialogResult.Cancel) return;
            char x;
            try
            {
                FileStream file = new FileStream(DiagOpen.FileName, FileMode.Open);
                BinaryReader BR = new BinaryReader(file);
                for (int i = 0; i < 81; i++)
                {
                    do
                    {
                        x = BR.ReadChar();
                    } while (x < '0' || x > '9');
                    if (x > '0')
                    {
                        text[i + 1].Text = x.ToString();
                        text[i + 1].ForeColor = Color.Red;
                        text[i + 1].BackColor = Color.Cyan;
                    }
                    else
                    {
                        text[i + 1].Text = "";
                        text[i + 1].ForeColor = Color.Black;
                        text[i + 1].BackColor = Color.White;
                    }
                }
                BR.Close();
                file.Close();
                myStatus.Text = "Mở tập tin " + DiagOpen.FileName;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Lỗi khi đọc file !\n" + Ex.Message, "Đã có lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            dapAn = 0;
            DiagSave.Filter = "Sudoku file(*.sdk)|*.sdk|Text file (*.txt)|*.txt";
            if (DiagSave.ShowDialog() == DialogResult.Cancel) return;
            try
            {
                FileStream file = new FileStream(DiagSave.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(file);

                for (int i = 0; i < 81; i++)
                {
                    bw.Write(text[i + 1].Text == "" ? "0" : text[i + 1].Text);
                }
                bw.Close();
                file.Close();
                myStatus.Text = "Đã lưu ra tập tin \"" + DiagSave.FileName + "\"";
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Có lỗi khi viết ra file !\n" + Ex.Message, "Có lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolSolve_Click(object sender, EventArgs e)
        {
            dapAn = 1;
            this.napde();
            SDK a = new SDK(de);
            if (a.SolveFirst())
            {
                for (int i = 1; i <= 81; i++)
                {
                    text[i].Text = a.Result[i].ToString();
                    if (de[i] == 0)
                    {
                        text[i].BackColor = Color.White;
                        text[i].ForeColor = Color.Black;
                    }
                    myStatus.Text = "Đã giải đề thành công";
                }
            }
            else
            {
                MessageBox.Show("Không có kết quả nào cả !", "Vô nghiệm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                myStatus.Text = "Giải đề không thành công";
            }
        }

        private void toolSoveTo_Click(object sender, EventArgs e)
        {
            SelectResult frm = new SelectResult();
            frm.TopMost = this.TopMost;
            frm.ShowDialog();
            int select = frm.selected;
            if (select == -1) return;
            napde();
            SDK a = new SDK(de);
            if (select > 10000)
            {
                lblwait.Visible = true;
                myStatus.Text = "Đang giải đề ... ";
                this.Enabled = false;
            }
            Application.DoEvents();
            if (!a.SolveTo(select))
            {
                if (select > 10000)
                {
                    this.Enabled = true;
                    lblwait.Visible = false;
                }
                MessageBox.Show("Bạn muốn xem nghiệm thứ " + select.ToString() + "\nNhưng Sudoku này có ít hơn " + select.ToString() + " nghiệm !\nHãy nhập vào số thích hợp.", "Không có nghiệm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                myStatus.Text = "Giải đề không thành công";
                Application.DoEvents();
                return;
            }
            if (select > 10000)
            {
                this.Enabled = true;
                lblwait.Visible = false;
            }
            dapAn = select;
            for (int i = 1; i <= 81; i++)
            {
                text[i].Text = a.Result[i].ToString();
                if (de[i] == 0)
                {
                    text[i].BackColor = Color.White;
                    text[i].ForeColor = Color.Black;
                }
            }
            myStatus.Text = "Đã giải đề thành công";
        }

        private void toolSolvePre_Click(object sender, EventArgs e)
        {
            switch (dapAn)
            {
                case 0: MessageBox.Show("Bạn chưa giải đề !", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1: MessageBox.Show("Đây là đáp án đầu tiên !", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    dapAn--;
                    napde();
                    SDK a = new SDK(de);
                    if (dapAn > 10000)
                    {
                        lblwait.Visible = true; ;
                        this.Enabled = false;
                    }
                    a.SolveTo(dapAn);
                    if (dapAn > 10000)
                    {
                        lblwait.Visible = false;
                        this.Enabled = true;
                    }
                    for (int i = 1; i <= 81; i++)
                    {
                        text[i].Text = a.Result[i].ToString();
                        if (de[i] == 0)
                        {
                            text[i].BackColor = Color.White;
                            text[i].ForeColor = Color.Black;
                        }
                    }
                    break;
            }
        }

        private void toolSolveNext_Click(object sender, EventArgs e)
        {
            dapAn++;
            napde();
            SDK a = new SDK(de);
            if (dapAn > 10000)
            {
                lblwait.Visible = true;
                this.Enabled = false;
            }
            Application.DoEvents();
            if (!a.SolveTo(dapAn))
            {
                if (dapAn > 10000)
                {
                    lblwait.Visible = false; ;
                    this.Enabled = true; ;
                }
                MessageBox.Show("Hết đáp án - Không tìm thấy đáp án tiếp theo !", "Không có nghiệm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dapAn--;
                return;
            }
            if (dapAn > 10000)
            {
                lblwait.Visible = false; ;
                this.Enabled = true; ;
            }
            for (int i = 1; i <= 81; i++)
            {
                text[i].Text = a.Result[i].ToString();
                if (de[i] == 0)
                {
                    text[i].BackColor = Color.White;
                    text[i].ForeColor = Color.Black;
                }
            }
        }

        private void toolStatic_Click(object sender, EventArgs e)
        {
            string da = dapAn == 0 ? "Chưa xem đáp án nào." : "Đang xem đáp án thứ " + dapAn.ToString() + ".";
            int daDien = 0, chuaDien = 0;
            napde();
            for (int i = 0; i < 81; i++)
            {
                if (de[i + 1] != 0) daDien++;
            }
            chuaDien = 81 - daDien;
            SDK a = new SDK(de);
            Application.DoEvents();
            int count = a.ResultCount();
            if (count == 0)
                MessageBox.Show("Sudoku có " + daDien.ToString() + " ô được điền, " + chuaDien.ToString() + " còn trống.\n Sudoku này không có đáp án\n", "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (count > 0) MessageBox.Show("Sudoku có " + daDien.ToString() + " ô được điền, " + chuaDien.ToString() + " còn trống.\n Sudoku này có " + count.ToString() + " đáp án !", "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Sudoku có " + daDien.ToString() + " ô được điền, " + chuaDien.ToString() + " còn trống.\nSudoku này có nhiều hơn 10.000 đáp án !\n" + da, "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void onTop_Click(object sender, EventArgs e)
        {

        }

        private void toolInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chương trình giải Sudoku - by Lê Mỹ Hoa và Nguyễn Hải Xuân !\n Liên lạc với mình qua lethimyhoa1512@gmail.com hoặc Xuanlary4545@gmail.com", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            dapAn = 0;
            myStatus.Text = "Sẳn sàng nhập dữ liệu";
            for (int i = 0; i < 81; i++)
            {
                text[i + 1].Text = "";
                text[i + 1].ForeColor = Color.Black;
                text[i + 1].BackColor = Color.White;
            }
            text[1].Focus();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            dapAn = 0;
            myStatus.Text = "Sẳn sàng nhập dữ liệu";
            for (int i = 0; i < 81; i++)
            {
                text[i + 1].Text = "";
                text[i + 1].ForeColor = Color.Black;
                text[i + 1].BackColor = Color.White;
            }
            text[1].Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dapAn = 0;
            DiagSave.Filter = "Sudoku file(*.sdk)|*.sdk|Text file (*.txt)|*.txt";
            if (DiagSave.ShowDialog() == DialogResult.Cancel) return;
            try
            {
                FileStream file = new FileStream(DiagSave.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(file);

                for (int i = 0; i < 81; i++)
                {
                    bw.Write(text[i + 1].Text == "" ? "0" : text[i + 1].Text);
                }
                bw.Close();
                file.Close();
                myStatus.Text = "Đã lưu ra tập tin \"" + DiagSave.FileName + "\"";
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Có lỗi khi viết ra file !\n" + Ex.Message, "Có lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            dapAn = 0;
            DiagOpen.InitialDirectory = Application.StartupPath;
            if (DiagOpen.ShowDialog() == DialogResult.Cancel) return;
            char x;
            try
            {
                FileStream file = new FileStream(DiagOpen.FileName, FileMode.Open);
                BinaryReader BR = new BinaryReader(file);
                for (int i = 0; i < 81; i++)
                {
                    do
                    {
                        x = BR.ReadChar();
                    } while (x < '0' || x > '9');
                    if (x > '0')
                    {
                        text[i + 1].Text = x.ToString();
                        text[i + 1].ForeColor = Color.Red;
                        text[i + 1].BackColor = Color.Cyan;
                    }
                    else
                    {
                        text[i + 1].Text = "";
                        text[i + 1].ForeColor = Color.Black;
                        text[i + 1].BackColor = Color.White;
                    }
                }
                BR.Close();
                file.Close();
                myStatus.Text = "Mở tập tin " + DiagOpen.FileName;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Lỗi khi đọc file !\n" + Ex.Message, "Đã có lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnReplay_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}