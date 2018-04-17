using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Xml.Serialization;

namespace Sterczewski_Laboratorium_9
{
    public partial class Form1 : Form
    {
        private List<Samochod> samochody = new List<Samochod>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nr_rej = textBox1.Text;
            string marka = textBox2.Text;
            int rok = Convert.ToInt32(textBox3.Text);
            string kolor = textBox4.Text;
            int ile_osob = Convert.ToInt32(textBox5.Text);

            Samochod s1 = new Samochod(nr_rej, marka, rok, kolor, ile_osob);
            samochody.Add(s1);
            dataGridView1.Rows.Add(nr_rej, marka, rok, kolor, ile_osob);

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button2_Click(object sender, EventArgs e) // wyczysc
        {
            samochody.Clear();
            dataGridView1.Rows.Clear();
        }

        private void button3_Click(object sender, EventArgs e)  // zapisz
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"./";
            sfd.Title = "Wybierz gdzie chcesz zapisać plik .txt";
            sfd.Filter = "Plik txt (*.txt)|*.txt|Wszystkie pliki (bez rozszerzenia)|*.*";
            sfd.FilterIndex = 1;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs1 = new FileStream(sfd.FileName, FileMode.Create);
                    StreamWriter sw1 = new StreamWriter(fs1);

                    for (int i = 0; i < samochody.Count(); i++)
                    {
                        sw1.WriteLine("[SAMOCHOD]");
                        sw1.WriteLine("[nr_rejestracyjny]");
                        sw1.WriteLine(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        sw1.WriteLine("[marka]");
                        sw1.WriteLine(dataGridView1.Rows[i].Cells[1].Value.ToString());
                        sw1.WriteLine("[rok_produkcji]");
                        sw1.WriteLine(dataGridView1.Rows[i].Cells[2].Value.ToString());
                        sw1.WriteLine("[kolor]");
                        sw1.WriteLine(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        sw1.WriteLine("[ilosc_pasazerow]");
                        sw1.WriteLine(dataGridView1.Rows[i].Cells[4].Value.ToString());
                        sw1.WriteLine("[END_SAMOCHOD]");
                        sw1.WriteLine("\n");
                    }
                    sw1.Close();
                    MessageBox.Show("Zapisanie się powiodło.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)  // wczytaj
        {
            Stream str = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"./";
            ofd.Title = "Wybierz plik tekstowy, z którego chcesz wczytać dane";
            ofd.Filter = "Pliki txt (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*";
            ofd.FilterIndex = 1;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((str = ofd.OpenFile()) != null)
                    {
                        using (str)
                        {
                            StreamReader sr1 = new StreamReader(str);
                            string linia;
                            List<string> linie = new List<string>();

                            while ((linia = sr1.ReadLine()) != null)
                            {
                                linie.Add(linia);
                            }

                            if (linie[0].StartsWith("[") && linie[1].StartsWith("["))
                            {
                                for (int i = 0; i < linie.Count(); i++)
                                {
                                    if (linie[i].StartsWith("[") && linie[i + 1].StartsWith("["))
                                    {
                                        samochody.Add(new Samochod(linie[i + 2], linie[i + 4], int.Parse(linie[i + 6]), linie[i + 8], int.Parse(linie[i + 10])));
                                        dataGridView1.Rows.Add(samochody[samochody.Count() - 1].nrRejestracyjny, samochody[samochody.Count() - 1].marka, samochody[samochody.Count() - 1].rokProdukcji, samochody[samochody.Count() - 1].kolor, samochody[samochody.Count() - 1].iloscPasazerow);
                                    }
                                }
                                sr1.Close();
                                MessageBox.Show("Wczytanie się powiodło.");
                            }

                            else
                            {
                                MessageBox.Show("Błędna struktura danych pliku");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)  // serializuj
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"./";
            sfd.Title = "Wybierz gdzie chcesz zapisać plik .xml";
            sfd.Filter = "Plik xml (*.xml)|*.xml|Wszystkie pliki (bez rozszerzenia)|*.*";
            sfd.FilterIndex = 1;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Samochod>));
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    StreamWriter sw1 = new StreamWriter(fs);

                    serializer.Serialize(fs, samochody);
                    MessageBox.Show("Serializacja się powiodła.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void button6_Click(object sender, EventArgs e)  // deserializuj
        {
            Stream str = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"./";
            ofd.Title = "Wybierz plik tekstowy, z którego chcesz wczytać dane";
            ofd.Filter = "Pliki xml (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
            ofd.FilterIndex = 1;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((str = ofd.OpenFile()) != null)
                    {
                        using (str)
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<Samochod>));
                            samochody = (List<Samochod>)serializer.Deserialize(str);
                            dataGridView1.Rows.Clear();

                            for (int i = 0; i < samochody.Count(); i++)
                            {
                                dataGridView1.Rows.Add(samochody[i].nrRejestracyjny, samochody[i].marka, samochody[i].rokProdukcji, samochody[i].kolor, samochody[i].iloscPasazerow);
                            }
                        }
                    }
                    MessageBox.Show("Deserializacja się powiodła.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
