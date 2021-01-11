using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ArduinoTemperaturaFormG4_2020_II
{
    public partial class FormTemperatura : Form
    {
        Form formpadre;
        public FormTemperatura(Form formpadre)
        {
            InitializeComponent();
            formpadre = this;
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
    }
}
