class Gráf
    {
        class Gráfcsúcs
        {
            string név;
            List<int> lista;
            static Random r = new Random();
            Tulaj tulaj;
            public Gráfcsúcs(string név)
            {
                orkok = new Orkok();
                CsúcsGenerálás(név);
            }
            public List<int> Lista { get { return lista; }set { lista = value; } }
            public string Név { get { return név; } set { név = value; } }
            Orkok orkok;

            public int Orkokszáma { get { return orkok.Megszámlálás(); } }
            public string Tulaj { get { return tulaj.Alany(); } }

            void CsúcsGenerálás(string név)
            {
                if (név == "Mordor")
                {
                    tulaj = new Szauron();
                    int a = r.Next(20, 101);
                    for (int i = 0; i < a; i++)
                    {
                        int n = r.Next(0, orknevek.Length);
                        orkok.VégéreBeszúrás(orknevek[n]);
                    }
                }
                else if (név == "Megye")
                    tulaj = new Szabad();
                else
                {
                    int b = r.Next(0, 101);
                    if (b < 50)
                        tulaj = new Szabad();
                    else
                    {
                        tulaj = new Szauron();
                        int a = r.Next(20, 101);
                        for (int i = 0; i < a; i++)
                        {
                            int n = r.Next(0, orknevek.Length);
                            orkok.VégéreBeszúrás(orknevek[n]);
                        }
                    }
                }
            }

            string[] orknevek = new string[] { "Józsi", "Béla", "Gizi", "Csanád", "Sanyi", "Géza", "Vinetu" };
        }

        int N;
        Gráfcsúcs[] L;
        List<string> helyek = new List<string>();
        public Gráf()
        {
            helyek.Add("Megye");
            Beolvas();
            helyek.Add("Mordor");
            N = helyek.Count;
            L = new Gráfcsúcs[N];
            for (int i = 0; i < N; i++)
            {
                L[i] = new Gráfcsúcs(helyek[i]);
                L[i].Lista = new List<int>();
            }
            N = helyek.Count;
            Gráfgenerálás();
        }
        public int Csúcsokszáma { get { return N; } }

        public string Tulaj(int i)
        {
            return L[i].Tulaj;
        }

        public int Orkokszáma(int i) { return L[i].Orkokszáma; }
        public string Helynév(int i) { return helyek[i]; }
        public List<int> Szomszédok(int csúcs)
        {
            List<int> szomszédok = new List<int>();
            List<int> csúcsok = Csúcsok();

            for (int i = 0; i < csúcsok.Count; i++)
            {
                if (VezetÉl(csúcs, csúcsok[i]))
                    szomszédok.Add(csúcsok[i]);
            }
            return szomszédok;
        }

        List<int> Csúcsok()
        {
            List<int> cs = new List<int>();

            for (int i = 0; i < N; i++)
            {
                cs.Add(i);
            }

            return cs;
        }
        void ÉlFElvétel(int honnan, int hova)
        {
            if (!L[honnan].Lista.Contains(hova))
            {
                L[honnan].Lista.Add(hova);
                L[hova].Lista.Add(honnan);
            }
        }
        bool VezetÉl(int honnan, int hova)
        {
            return L[honnan].Lista.Contains(hova);
        }

        static Random rnd = new Random();

        void Gráfgenerálás()
        {
            int élek = rnd.Next(N, N+10);
            for (int i = 0; i < élek; i++)
            {
                int rnd1 = rnd.Next(0, N);
                int rnd2 = rnd.Next(0, N);

                ÉlFElvétel(rnd1, rnd2);
            }
        }

        void Beolvas()
        {
            string[] seged = File.ReadAllText("Helységnevek.txt", Encoding.Default).Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < seged.Length; i++)
            {
                seged[i].Trim();
            }
            foreach (string item in seged)
            {
                if (item != "Megye" && item != "Mordor")
                    helyek.Add(item);
            }
        }

        public List<int> Dijkstra(int start,int end)
        {
            int[] d = new int[N];
            int[] n = new int[N];
            d[start] = 0;
            List<int> Q = new List<int>();
            for (int i = 0; i < N; i++)
            {
                Q.Add(i);
            }
            while (Q.Count!=0)
            {
                int u = LegkisebbKivesz(Q);
                List<int> szomszédok = Szomszédok(u);
                foreach  (int item in szomszédok)
                {
                    if(d[u]+Orkokszáma(u)<d[item])
                    {
                        d[item] = d[u] + Orkokszáma(item);
                        n[item] = u;
                    }
                }
            }
            List<int> útvonal = new List<int>();
            if (n[end] != null)
            {
                int akt = end;
                do
                {

                    útvonal.Add(akt);
                    akt = n[akt];

                } while (!útvonal.Contains(start));
                return útvonal;
            }
            else
                return útvonal;
        }

        int LegkisebbKivesz(List<int> Q)
        {
            int min = Q[0];
            foreach  (int item in Q)
            {
                if (Orkokszáma(item) > Orkokszáma(min))
                    min = item;
            }
            Q.Remove(min);
            return min;
        }
    }
