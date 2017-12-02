using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySudoku
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

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
                text[i].Size = Mau.Size;
                text[i].Font = Mau.Font;
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
            this.Height = myPanel.Top + myPanel.Height + myMenu.Height + space - 2 + myStatus.Height;

        }
        private void LoadPictureTool()
        {
            toolSolve.Image = mnuSolve.Image;
            toolSolveTo.Image = mnuSolveTo.Image;
            toolSolvePrevious.Image = mnuSolvePrevious.Image;
            toolSolveNext.Image = mnuSolveNext.Image;
            toolNew.Image = mnuNew.Image;
            toolOpen.Image = mnuOpen.Image;
            toolSave.Image = mnuSave.Image;
            toolStatic.Image = mnuStatic.Image;
            toolInfo.Image = mnuInformation.Image;

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadText();
            LoadPictureTool();
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
        
        
        private void napde()
        {
            int i;
            for (i = 1; i <= 81; i++)
            {
                if ((text[i].Text == "") || (text[i].ForeColor == Color.Black)) de[i] = 0;
                else de[i] = int.Parse(text[i].Text);
            }
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
    }
}
