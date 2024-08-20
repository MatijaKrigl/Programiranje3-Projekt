using System.Diagnostics;

namespace ClovekNeJeziSe
{
    public class Igra
    {
        public Igralec[] Igralci { get; private set; }
        private int trenutniIgralecIndeks;
        private Random nakljucno;

        private static readonly int[][] ZacetnePozicije =
        {
            new int[] { 1 },    // Začetne pozicije za igralca 0
            new int[] { 13 }, // Začetne pozicije za igralca 1
            new int[] { 25 },
            new int[] { 37 }
        };

        private static readonly int[][] HisicaPozicije =
        {
            new int[] { -11, -12, -13, -14 }, // Hišica za igralca 0
            new int[] { -21, -22, -23, -24 }, // Hišica za igralca 1
            new int[] { -31, -32, -33, -34 }, // Hišica za igralca 2
            new int[] { -41, -42, -43, -44 }  // Hišica za igralca 3
        };

        private static readonly int[][] BazaPozicije =
        {
            new int[] { -111, -112, -113, -114 }, // Baza za igralca 0
            new int[] { -211, -212, -213, -214 },
            new int[] { -311, -312, -313, -314 },
            new int[] { -411, -412, -413, -414 }
        };

        /// <summary>
        /// Inicializira novo igro z določenim številom igralcev.
        /// Nastavi igralce, njihove figure in določi, kateri igralec je na vrsti.
        /// </summary>
        /// <param name="steviloIgralcev">Število igralcev v igri.</param>
        public Igra(int steviloIgralcev)
        {
            Igralci = new Igralec[steviloIgralcev];
            for (int i = 0; i < steviloIgralcev; i++)
            {
                Igralci[i] = new Igralec(i, steviloIgralcev == 1 || i == steviloIgralcev - 1); // pogoj ce bi zeleli igrati proti racunalniku
                for (int j = 0; j < 4; j++)
                {
                    Igralci[i].Figure[j] = BazaPozicije[i][j];
                }
            }
            trenutniIgralecIndeks = 0;
            nakljucno = new Random();
        }

        public int VrziKocko()
        {
            return nakljucno.Next(1, 7);
        }

        public void NaslednjiKorak(int rezultatKocke)
        {
            // Če igralec ni vrgel 6, premaknemo na naslednjega igralca
            if (rezultatKocke != 6)
            {
                trenutniIgralecIndeks = (trenutniIgralecIndeks + 1) % Igralci.Length;
            }
            else
            {
                // Igralec vrže še enkrat
                Debug.WriteLine($"Igralec {Igralci[trenutniIgralecIndeks].Id} vrže še enkrat, ker je vrgel 6.");
            }
        }


        public Igralec VrniTrenutnegaIgralca()
        {
            return Igralci[trenutniIgralecIndeks];
        }

        /// <summary>
        /// Premakne izbrano figuro določenega igralca glede na število vrženih korakov.
        /// Obdeluje različne scenarije, kot so premik figure na polje, vstop v hišico in odstranitev nasprotnikove figure.
        /// </summary>
        /// <param name="igralec">Igralec, katerega figuro premikamo.</param>
        /// <param name="indeksFigure">Indeks figure, ki jo želimo premakniti.</param>
        /// <param name="koraki">Število korakov, ki jih je potrebno izvesti.</param>
        public void PremakniFiguro(Igralec igralec, int indeksFigure, int koraki)
        {
            int trenutnaPozicija = igralec.Figure[indeksFigure];
            int prejsnaPozicija = trenutnaPozicija; // Shrani prejšnjo pozicijo
            Debug.WriteLine($"Igralec {igralec.Id} je vrgel {koraki}.");

            if (trenutnaPozicija < -10) // Če je figura v bazi ali v hišici
            {
                if (koraki == 6) // Igralec vrže šestico
                {
                    igralec.Figure[indeksFigure] = ZacetnePozicije[igralec.Id][0];
                }
            }
            else if (trenutnaPozicija >= 0) // Če je figura na polju
            {
                int novaPozicija = trenutnaPozicija + koraki;

                // Če presežemo 48, nadaljujemo iz začetnih polj
                if (novaPozicija > 48)
                {
                    novaPozicija = novaPozicija % 48;
                }

                // Preverimo, če je figura naredila cel krog in mora iti v hišico
                if (novaPozicija > ZacetnePozicije[igralec.Id][0] && prejsnaPozicija < ZacetnePozicije[igralec.Id][0])
                {
                    int korakiVHišico = novaPozicija - ZacetnePozicije[igralec.Id][0] - 1;
                    int pozicijaHisice = HisicaPozicije[igralec.Id][korakiVHišico];
                    Debug.WriteLine("V HIŠICO");
                    Debug.WriteLine("Koraki v hišico:" +korakiVHišico);
                    Debug.WriteLine("Pozicija hišice:" +pozicijaHisice);
                    igralec.Figure[indeksFigure] = HisicaPozicije[igralec.Id][korakiVHišico];

                    //if (pozicijaHisice >= 0 && pozicijaHisice < HisicaPozicije[igralec.Id].Length)
                    //{
                    //    // Preverimo, če koraki ne presegajo dolžine poti v hišico
                    //    if (pozicijaHisice + koraki <= HisicaPozicije[igralec.Id].Length)
                    //    {
                    //        igralec.Figure[indeksFigure] = HisicaPozicije[igralec.Id][pozicijaHisice];
                    //    }
                    //    else
                    //    {
                    //        Debug.WriteLine($"Igralec {igralec.Id} ne more vstopiti v hišico z vrženimi koraki.");
                    //        // Preveri, če obstaja druga figura za premik
                    //        if (igralec.Figure.Any(f => f >= 0 && f < 48))
                    //        {
                    //            Debug.WriteLine($"Igralec {igralec.Id} izbere drugo figuro za premik.");
                    //            // Tu bi se klicala funkcija za izbiro druge figure
                    //            int novaFigura = IzberiDrugoFiguro(igralec);
                    //            if (novaFigura != -1)
                    //            {
                    //                PremakniFiguro(igralec, novaFigura, koraki);
                    //            }
                    //            return; // Igralec izbere drugo figuro, ne nadaljuj z obdelavo trenutne
                    //        }
                    //        else
                    //        {
                    //            Debug.WriteLine($"Igralec {igralec.Id} nima druge figure za premik. Preskočen.");
                    //            return; // Igralec se preskoči, ker nima druge figure za premik
                    //        }
                    //    }
                    //}
                }
                else
                {
                    igralec.Figure[indeksFigure] = novaPozicija;
                }

                // Preverimo, če je figura na polju drugega igralca in jo pojejmo
                foreach (var drugiIgralec in Igralci)
                {
                    if (drugiIgralec.Id != igralec.Id)
                    {
                        for (int i = 0; i < drugiIgralec.Figure.Length; i++)
                        {
                            if (drugiIgralec.Figure[i] == igralec.Figure[indeksFigure])
                            {
                                // Pojejmo figuro drugega igralca in jo vrnimo na začetno pozicijo
                                Debug.WriteLine($"Figura igralca {drugiIgralec.Id} na poziciji {drugiIgralec.Figure[i]} je bila pojedena.");
                                drugiIgralec.Figure[i] = BazaPozicije[drugiIgralec.Id][i];
                                break; // Ne nadaljuj preverjanja, ker smo že pojedli figuro
                            }
                        }
                    }
                }
            }

            // Zabeleži premik
            Debug.WriteLine($"Igralec {igralec.Id} premaknil figuro {indeksFigure + 1} iz {prejsnaPozicija} na {igralec.Figure[indeksFigure]}.");

            // Zabeleži trenutne pozicije vseh figur
            foreach (var igr in Igralci)
            {
                Debug.WriteLine($"Igralec {igr.Id}: Pozicije figur - {string.Join(", ", igr.Figure)}");
            }

            
        }

        /// <summary>
        /// Izbere drugo figuro igralca, ki je trenutno na igralnem polju.
        /// </summary>
        /// <param name="igralec">Igralec, katerega figuro želimo izbrati.</param>
        /// <returns>Indeks izbrane figure ali -1, če ni na voljo nobene druge figure.</returns>
        private int IzberiDrugoFiguro(Igralec igralec)
        {
            var availableFigures = igralec.Figure
                .Select((value, index) => new { value, index })
                .Where(x => x.value >= 0 && x.value < 48)
                .Select(x => x.index)
                .ToList();

            if (availableFigures.Count == 0)
                return -1; // Ni na voljo druge figure

            // Tu dodamo logiko za izbor figure, na primer prikaz okna za uporabnika

            return availableFigures[0]; // Privzeto vrnemo prvo razpoložljivo figuro
        }

        /// <summary>
        /// Pridobi trenutno stanje igre, vključno z informacijami o vseh igralcih, njihovih figurah in ali so računalniški nasprotniki.
        /// </summary>
        /// <returns>Vrnjen je niz, ki vsebuje podrobnosti o vsakem igralcu in njihovih figurah.</returns>
        public string PridobiStanjeIgre()
        {
            string stanjeIgre = "";

            foreach (var igralec in Igralci)
            {
                stanjeIgre += $"Igralec {igralec.Id}:\n";
                stanjeIgre += $"Figure: {string.Join(", ", igralec.Figure)}\n";
                stanjeIgre += $"Je računalnik: {igralec.JeRacunalnik}\n";
                stanjeIgre += "\n";
            }

            return stanjeIgre;
        }

        /// <summary>
        /// Naloži shranjeno stanje igre iz podanega niza in nastavi stanje igre na podlagi teh podatkov.
        /// </summary>
        /// <param name="stanjeIgre">Niz, ki vsebuje podatke o shranjenem stanju igre.</param>
        public void NaloziStanjeIgre(string stanjeIgre)
        {
            // Razdelimo stanje igre na vrstice
            var vrstice = stanjeIgre.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;

            while (i < vrstice.Length)
            {
                if (vrstice[i].StartsWith("Igralec"))
                {
                    // Izvlečemo ID igralca
                    int id = int.Parse(vrstice[i].Split(' ')[1].Trim(':'));
                    var igralec = Igralci[id];

                    // Naslednja vrstica vsebuje figure
                    string[] figureStr = vrstice[++i].Replace("Figure: ", "").Split(',');
                    for (int j = 0; j < figureStr.Length; j++)
                    {
                        igralec.Figure[j] = int.Parse(figureStr[j].Trim());
                    }

                    // Naslednja vrstica pove, če je igralec računalnik
                    igralec.JeRacunalnik = vrstice[++i].Contains("true");
                }

                i++;
            }
        }
    }

    public class Igralec
    {
        public int Id { get; set; }
        public int[] Figure { get; set; }
        public bool JeRacunalnik { get; set; }

        public Igralec(int id, bool jeRacunalnik = false)
        {
            Id = id;
            Figure = new int[4] { -1, -1, -1, -1 };
            JeRacunalnik = jeRacunalnik;
        }
    }
}