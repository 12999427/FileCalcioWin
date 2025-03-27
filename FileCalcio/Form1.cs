namespace FileCalcio
{
    public partial class Form1 : Form
    {
        struct Partita
        {
            public string SquadraCasa;
            public string SquadraOspite;
            public int GolCasa;
            public int GolOspite;

            public Partita(string c, string o, int gc, int go)
            {
                SquadraCasa = c;
                SquadraOspite = o;
                GolCasa = gc;
                GolOspite = go;
            }
        }

        struct Squadra //usato per il calcolo di squadra con più gol, anche se si poteva evitare una struct
        {
            public string nome;
            public int golSegnati;

            public Squadra(string n, int g)
            {
                nome = n;
                golSegnati = g;
            }
        }

        //potevo anche mettere sempre lo stesso come stringa ogni volta, ma ritengo, anche a costo di creare una
        //variabile globale (anche se costante), che così sia maglio per la leggibilità
        const string path = @"..\..\..\..\data.csv";

        Partita[] partite;
        int partiteLen = 0;
        int[] ordineDati = new int[4] { 0, 1, 2, 3 }; //ordine di default, può venire sovrascritto

        public Form1()
        {
            InitializeComponent();
            ordineDati = LoadFile(path);
            SaveFile(path, ordineDati);
            ShowData(ordineDati);

        }

        private int[] LoadFile(string path)
        {
            partite = new Partita[30];
            int SquadraCasaIndex = -1, SquadraOspiteIndex = -1, GolCasaIndex = -1, GolOspiteIndex = -1;
            using (StreamReader sr = new StreamReader(path))
            {
                int i = 0;
                while (sr.Peek() != -1)
                {
                    string record = sr.ReadLine();
                    string[] campi = record.Split(';');

                    for (int j = 0; j < campi.Length; j++)
                    {
                        if (i == 0)
                        {
                            switch (campi[j])
                            {
                                case "SquadraCasa":
                                    SquadraCasaIndex = j;
                                    break;
                                case "SquadraOspite":
                                    SquadraOspiteIndex = j;
                                    break;
                                case "GolCasa":
                                    GolCasaIndex = j;
                                    break;
                                case "GolOspite":
                                    GolOspiteIndex = j;
                                    break;
                                default:
                                    MessageBox.Show("Nell'intestazione c'è un nome non riconoscuiuto, ignorato");
                                    break;
                            }
                        }
                        else
                        {
                            try
                            {
                                partite[partiteLen] = new Partita(campi[SquadraCasaIndex], campi[SquadraOspiteIndex], int.Parse(campi[GolCasaIndex]), int.Parse(campi[GolOspiteIndex]));
                            }
                            catch
                            {
                                MessageBox.Show("Uno dei campi del file non e' converitbile in numero intero");
                            }
                        }
                    }
                    if (i == 0)
                    {
                        if (SquadraCasaIndex == -1 || SquadraOspiteIndex == -1 || GolCasaIndex == -1 || GolOspiteIndex == -1)
                        {
                            MessageBox.Show("Nell'intestazione non ci sono tutti i nomi");
                            return (new int[0]);
                        }
                    }
                    else
                    {
                        partiteLen++;
                    }

                    i++;
                }
            }
            return (new int[4] { SquadraCasaIndex, SquadraOspiteIndex, GolCasaIndex, GolOspiteIndex });
        }

        private string getCampo(string[] valori, int[] indici, int indice)
        {
            for (int i = 0; i < valori.Length; i++)
            {
                if (indici[i] == indice)
                {
                    return valori[i];
                }
            }
            MessageBox.Show("Impossibile trovare un campo nella funzione getCampo");
            return "";
        }

        private void SaveFile(string path, int SquadraCasaIndex, int SquadraOspiteIndex, int GolCasaIndex, int GolOspiteIndex)
        {
            try
            {
                File.WriteAllText(path, "");
                using (StreamWriter sw = File.AppendText(path))
                {
                    string[] nomiCampi = new string[4] { "SquadraCasa", "SquadraOspite", "GolCasa", "GolOspite" };
                    int[] indici = new int[4] { SquadraCasaIndex, SquadraOspiteIndex, GolCasaIndex, GolOspiteIndex };
                    for (int i = 0; i < 4; i++)
                    {
                        string elemento = getCampo(nomiCampi, indici, i);
                        sw.Write(elemento);
                        if (i != 3)
                        {
                            sw.Write(";");
                        }
                    }
                    sw.WriteLine();

                    for (int i = 0; i < partiteLen; i++)
                    {
                        string[] valoriCampi = new string[4] { partite[i].SquadraCasa, partite[i].SquadraOspite, partite[i].GolCasa.ToString(), partite[i].GolOspite.ToString() };
                        for (int j = 0; j < 4; j++)
                        {
                            string elemento = getCampo(valoriCampi, indici, j);
                            sw.Write(elemento);
                            if (j != 3)
                            {
                                sw.Write(";");
                            }
                        }
                        sw.WriteLine();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Qualcosa è andato storto nella scrittura del file");
            }
        }

        private void SaveFile(string path, int[] ordine)
        {
            SaveFile(path, ordine[0], ordine[1], ordine[2], ordine[3]);
        }

        private void ShowData(int[] ordine)
        {
            string search = txt_ricerca.Text;
            bool filter = txt_ricerca.Text != "";

            lst_elenco.Items.Clear();
            string[] nomiCampi = new string[4] { "SquadraCasa", "SquadraOspite", "GolCasa", "GolOspite" };
            lst_elenco.Items.Add(PadString(new string[4] {
                getCampo(nomiCampi, ordine, 0),
                getCampo(nomiCampi, ordine, 1),
                getCampo(nomiCampi, ordine, 2),
                getCampo(nomiCampi, ordine, 3),
            }));

            for (int i = 0; i < partiteLen; i++)
            {
                if (!filter || partite[i].SquadraCasa.ToLower().Contains(search.ToLower()) || partite[i].SquadraOspite.ToLower().Contains(search.ToLower())) //se la ricerca è applicata, filtra
                {
                    string[] valori = new string[4] {
                        partite[i].SquadraCasa.ToString(),
                        partite[i].SquadraOspite.ToString(),
                        partite[i].GolCasa.ToString(),
                        partite[i].GolOspite.ToString()
                    };
                    lst_elenco.Items.Add(PadString(new string[4] {
                        getCampo(valori, ordine, 0),
                        getCampo(valori, ordine, 1),
                        getCampo(valori, ordine, 2),
                        getCampo(valori, ordine, 3),
                    }));
                }
            }
        }

        private string PadString(string[] txt)
        {
            string output = "";
            for (int i = 0; i < txt.Length; i++)
            {
                output += txt[i].PadRight(20);
            }
            return output;
        }

        private void Aggiungi(string squadraCasa, string squadraOspite, int golCasa, int golOspite)
        {
            partite[partiteLen++] = new Partita(squadraCasa, squadraOspite, golCasa, golOspite);
            lst_elenco.SelectedIndex = -1;
            SaveFile(path, ordineDati);
            ShowData(ordineDati);
        }

        private int getGolTotali()
        {
            int golTotali = 0;
            for (int i = 0; i < partiteLen; i++)
            {
                golTotali += partite[i].GolCasa + partite[i].GolOspite;
            }
            return golTotali;
        }

        private int getPartitaGolMax()
        {
            int golMax = -1;
            int maxIndex = -1;
            for (int i = 0; i < partiteLen; i++)
            {
                int golPartita = partite[i].GolCasa + partite[i].GolOspite;
                if (golPartita > golMax)
                {
                    golMax = golPartita;
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        private Squadra getSquadaGolMax()
        {
            (Squadra[] squadre, int squadreLen) = creaElencoSquadre();
            int maxIndex = -1;
            int golMax = -1;
            for (int i = 0; i < squadreLen; i++)
            {
                if (squadre[i].golSegnati > golMax)
                {
                    golMax = squadre[i].golSegnati;
                    maxIndex = i;
                }
            }
            return squadre[maxIndex];
        }

        private (Squadra[], int) creaElencoSquadre()
        {
            Squadra[] output = new Squadra[30];
            int squadreLen = 0;
            for (int i = 0; i < partiteLen; i++)
            {
                addSquadra(output, ref squadreLen, partite[i].SquadraCasa, partite[i].GolCasa);
                addSquadra(output, ref squadreLen, partite[i].SquadraOspite, partite[i].GolOspite);
            }
            return (output, squadreLen);
        }

        private void addSquadra(Squadra[] output, ref int squadreLen, string squadra, int gol)
        {
            int index;
            index = findSquadra(output, squadreLen, squadra);
            if (index == -1)
            {
                output[squadreLen++] = new Squadra(squadra, gol);
            }
            else
            {
                output[index].golSegnati += gol;
            }
        }

        private int findSquadra(Squadra[] squadre, int len, string squadra)
        {
            for (int i = 0; i < len; i++)
            {
                if (squadre[i].nome == squadra)
                {
                    return i;
                }
            }
            return -1;
        }

        private void btn_aggiungi_Click(object sender, EventArgs e)
        {
            try
            {
                string squadraCasa = txt_squadraCasa.Text;
                string squadraOspite = txt_squadraOspite.Text;
                int golCasa = int.Parse(txt_golCasa.Text);
                int golOspite = int.Parse(txt_golOspite.Text);
                if (squadraCasa == "" || squadraOspite == "" || golCasa < 0 || golOspite < 0 || squadraCasa == squadraOspite)
                {
                    MessageBox.Show("Alcuni dati sono errati (gol inferiori di 0, squadre senza nome o con lo stesso nome)");
                    return;
                }
                Aggiungi(squadraCasa, squadraOspite, golCasa, golOspite);
            }
            catch
            {
                MessageBox.Show("Impossibile interpretare dati numerici");
            }

        }

        private void txt_ricerca_TextChanged(object sender, EventArgs e)
        {
            ShowData(ordineDati);
        }

        private void btn_squadraMaxGol_Click(object sender, EventArgs e)
        {
            Squadra squadra = getSquadaGolMax();
            MessageBox.Show($"Squadra con più gol: {squadra.nome}, con {squadra.golSegnati} gol");
        }

        private void btn_golTotali_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Totale gol: {getGolTotali()}");
        }

        private void btn_partitaMaxGol_Click(object sender, EventArgs e)
        {
            int index = getPartitaGolMax();
            lst_elenco.SelectedIndex = txt_ricerca.Text == "" ? index + 1 : -1; //per intestazione
            Partita partita = partite[index];
            MessageBox.Show($"La partita con più gol è {partita.SquadraCasa} vs {partita.SquadraOspite} con {partita.GolCasa} : {partita.GolOspite}");
        }
    }
}