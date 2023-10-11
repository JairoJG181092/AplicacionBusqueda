using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Busqueda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string[] archivosEncontradosAMover;
        private void LlenarListBox()
        {
            string[] datos = archivosEncontradosAMover;

            foreach (string itemText in datos)
            {
                listBox1.Items.Add(itemText);
            }
        }

        private string[] archivosExistentesAMover;
        private void LlenarListBox2()
        {
            string[] datos = archivosExistentesAMover;

            foreach (string itemText in datos)
            {
                listBox2.Items.Add(itemText);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                string rutaOrigen = folder.SelectedPath;
                textBox3.Text = rutaOrigen;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                string rutaDestino = folder.SelectedPath;
                textBox4.Text = rutaDestino;
            }
        }

        private void btnMover_Click_1(object sender, EventArgs e)
        {
            if (archivosEncontradosAMover != null && archivosEncontradosAMover.Length > 0)
            {
                string carpetaDestino = textBox4.Text + "/" + textBox1.Text;

                if (!Directory.Exists(carpetaDestino))
                    Directory.CreateDirectory(carpetaDestino);

                try
                {
                    foreach (string archivo in archivosEncontradosAMover)
                    {
                        string nombreArchivo = Path.GetFileName(archivo);
                        string rutaDestino = Path.Combine(carpetaDestino, nombreArchivo);

                        if (!File.Exists(rutaDestino))
                        {
                            File.Move(archivo, rutaDestino);
                            
                        }
                        else
                        {
                            listBox2.Items.Add(archivo);
                        }
                    }

                    MessageBox.Show("Archivos movidos exitosamente.");

                    listBox1.Items.Clear();
                    //listBox2.Items.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al mover archivos: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("No se han encontrado archivos para mover.");
            }
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            string textoBusqueda = textBox1.Text;
            string ruta = textBox3.Text;
            string ruta2 = textBox4.Text;
            string[] archivosEncontrados = Directory.GetFiles(ruta, $"*{textoBusqueda}*", SearchOption.AllDirectories);
            //string[] archivosExistentes = Directory.GetFiles(ruta2, $"*{textoBusqueda}*", SearchOption.AllDirectories);

            // Mostrar los archivos encontrados (esto es solo un ejemplo, puedes mostrarlos en un ListBox, etc.)
            MessageBox.Show($"Se encontraron {archivosEncontrados.Length} archivos que coinciden con '{textoBusqueda}'.");

            // Guardar los archivos encontrados para moverlos después
            archivosEncontradosAMover = archivosEncontrados;
            //archivosExistentesAMover = archivosExistentes;
            LlenarListBox();
            //LlenarListBox2();
        }
    }
}
