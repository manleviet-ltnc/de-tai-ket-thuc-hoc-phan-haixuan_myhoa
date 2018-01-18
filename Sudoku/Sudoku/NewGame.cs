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

namespace Sudoku
{
    public partial class NewGame : Form
    {
        Thread thread;
        public NewGame()
        {
            InitializeComponent();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            Close();
            thread = new Thread(OpenGameBoard);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void OpenGameBoard()
        {
            Application.Run(new GameBoard());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}