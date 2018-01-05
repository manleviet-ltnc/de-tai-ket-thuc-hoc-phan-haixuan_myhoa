using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class SelectResult : Form
    {
        public int selected = -1;
        public SelectResult()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                selected = int.Parse(input.Text);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Hãy nhập một số hợp lệ !", "Nhập số", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                button1_Click(sender, e);
            }
            else if (e.KeyCode.ToString() == "Escape") this.Close();
        }
    }
}
