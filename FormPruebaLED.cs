using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;


namespace ArduinoTemperaturaFormG4_2020_II
{
    public partial class FormPruebaLED : Form
    {
        private SerialPort serialPort;
        private bool prendeApaga;
        public FormPruebaLED(SerialPort serialPort)
        {
            InitializeComponent();
            this.serialPort = serialPort;
        }

        private void btnPrende_Click(object sender, EventArgs e)
        {
            if (!prendeApaga)
            {
                serialPort.Write("p");
                prendeApaga = true;
                btnPrende.Text = "Apaga";
            }else
            {
                serialPort.Write("a");
                prendeApaga = false;
                btnPrende.Text = "Prende";
            }          
            
        }
    }
}
