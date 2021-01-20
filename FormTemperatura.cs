using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace ArduinoTemperaturaFormG4_2020_II
{
    public partial class FormTemperatura : Form
    {
        Form1 formpadre;
        int tiempo;
        int temperatura;
        public FormTemperatura(Form1 formpadre)
        {
            InitializeComponent();
            this.formpadre = formpadre;
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo de Texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            saveFileDialog.DefaultExt = ".txt";
            StreamWriter sw = null;

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sw = new StreamWriter(saveFileDialog.FileName);

                    for(int i=0; i < richTexto.Lines.Length;  i++)
                    {
                        string cadena = richTexto.Lines[i];
                        if( cadena.Contains(","))
                        {
                            cadena += cadena;
                        }

                        sw.WriteLine(cadena);
                    }

                }
                catch(IOException error  )
                {
                    MessageBox.Show("Error", error.Message);
                }
                catch(Exception error)
                {
                    MessageBox.Show("Error", error.Message);
                }
                finally
                {
                    //Siempre cerrar el flujo
                    sw.Close();
                }

            }


        }
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivo de Texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            openFileDialog.DefaultExt = ".txt";
            StreamReader sr = null;

            if( openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                try
                {
                    sr = new StreamReader(openFileDialog.FileName);

                    List<string> lineas = new List<string>();
                    string aux = sr.ReadLine();                    

                    while( aux != null)
                    {

                        lineas.Add(aux);
                        aux = sr.ReadLine();

                    }

                    string[] cadenas = new string[lineas.Count];
                    for( int i=0; i < cadenas.Length;i++)
                    {
                        cadenas[i] = lineas[i];
                    }

                    richTexto.Lines = cadenas;


                }
                catch(IOException error)
                {
                    MessageBox.Show("Error", error.Message);
                }
                catch(Exception error)
                {
                    MessageBox.Show("Error", error.Message);
                }
                finally
                {
                    sr.Close();
                }            

            }

        }

        private void tiempoTemperatura_Tick(object sender, EventArgs e)
        {
            temperatura = formpadre.serialPort.ReadByte();
            chtTemperatura.Series[0].Points.AddXY(tiempo++, temperatura);

        }

        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tiempoTemperatura.Start();
        }

        private void pararToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tiempoTemperatura.Stop();
        }

        private void exportarAPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document docPdf = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(docPdf, new FileStream(saveFileDialog.FileName, FileMode.Append));
                docPdf.Open();
                MemoryStream imagenGraficar = new MemoryStream();
                chtTemperatura.SaveImage(imagenGraficar, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                iTextSharp.text.Image GrafPdf = iTextSharp.text.Image.GetInstance(imagenGraficar.GetBuffer());
                docPdf.Add(GrafPdf);
                docPdf.Close();
            }

        }
    }
}
