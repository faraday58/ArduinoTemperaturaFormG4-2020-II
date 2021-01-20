using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace ArduinoTemperaturaFormG4_2020_II
{
    public partial class Form1 : Form
    {
        public SerialPort serialPort;
        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPort();
            selecciona();
        }

        private void selecciona()
        {
            string[] v = SerialPort.GetPortNames();
            Array.Sort(v);
            toolStripCMBpuertos.Items.AddRange(v);
            toolStripCMBvelocidad.SelectedIndex = 2;
        }


        private void temperaturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTemperatura formTemperatura = new FormTemperatura(this);
            formTemperatura.MdiParent = this;
            formTemperatura.Show();

        }

        private void verificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.PortName = toolStripCMBpuertos.SelectedItem.ToString();
                serialPort.BaudRate = int.Parse(toolStripCMBvelocidad.SelectedItem.ToString());
            }
            catch(IOException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void conectarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort.Open();
        }

        private void pruebaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPruebaLED formPruebaLED = new FormPruebaLED(serialPort);
            formPruebaLED.MdiParent = this;
            formPruebaLED.Show();

        }
    }
}
