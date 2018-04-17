using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sterczewski_Laboratorium_9
{
    public class Samochod
    {
        public string nrRejestracyjny { get; set; }
        public string marka { get; set; }
        public int rokProdukcji { get; set; }
        public string kolor { get; set; }
        public int iloscPasazerow { get; set; }

        public Samochod()
        {
            nrRejestracyjny = "-";
            marka = "-";
            rokProdukcji = 0;
            kolor = "-";
            iloscPasazerow = 0;
        }

        public Samochod(string nrRejestracyjny, string marka, int rokProdukcji, string kolor, int iloscPasazerow)
        {
            this.nrRejestracyjny = nrRejestracyjny;
            this.marka = marka;
            this.rokProdukcji = rokProdukcji;
            this.kolor = kolor;
            this.iloscPasazerow = iloscPasazerow;
        }

    }
}
