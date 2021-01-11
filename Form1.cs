using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArduinoTemperaturaFormG4_2020_II
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void temperaturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTemperatura formTemperatura = new FormTemperatura(this);
            formTemperatura.MdiParent = this;
            formTemperatura.Show();

        }
    }
}
