using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace nagyzhminta
{
    class Program
    {
        static string[,] HarcterGeneralas()
        {
            // Nem muszály tipusadás a C# ban a változó neve elött, elég a 'var' kulcsszó,
            // var esetében a gép az '=' operátor jobb oldalából kitalálja a tipust.
            var harcter = new string[10, 20];  // Ami ebben az esetben elég egyértelmű szemmel is

            /* 
				A változó nevek egyértelműek kellenek hogy legyenek (CleanCode - Robert C. Martin),
				kivéve a ciklus indexeket meg itt a Random classt hiszen ezek a hosszabb változónév nélkül
				is egyértelműek. (Bár tudom milyen vizsgázni, de ezt érdemes megfogadni.)
			*/
            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (r.Next(101) < 30)
                        harcter[i, j] = "s";
                    else if (i < 2 && r.Next(101) < 50)
                        harcter[i, j] = "s";
                    else if(i < 2)
                        harcter[i, j] = "p";

                    if (i > 1)
                    {
                        if (r.Next(101) < 50)
                        {
                            harcter[i, j] = "p";
                        }
                        else
                        {
                            if (r.Next(101) < 50)
                                harcter[i, j] = "i";
                            else
                                harcter[i, j] = "e";
                        }
                    }
                }
            }
            return harcter;
        }

        static string AdatokMegjelenites(string[,] forras)
        {
            /*
             * Ez lehet kicsit túl lett bonyolítva.
         
            string s = "";
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    s += forras[i, j];
                }
            }
            for (int i = 0; i < s.Length; i++)
            {
                if (i % 20 == 19)
                {
                    Console.Write(s[i] + "\r\n");
                }
                else
                    Console.Write(s[i] + " ");
            }
            return s;
            */

            // Mivel a forras paramter magában egy string mátrix ezért nincs semmi értelme 
            // stringbe ömleszeni. Én ezt az algoritmust szoktam használni mátrix adatok kiírására:
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                    Console.Write(forras[i, j] + " ");
                Console.WriteLine();
            }
            // És ömlesztésére:
            return string.Join("\r\n", string.Join("  ", forras));  // Stringé alakítja a mátrixot (egy sorban!)

            // Tipp: Ha megtetszik egy beépített metódus mint a string.Join akkor ezeket ne magold be, hanem
            // tarts magadnál egy 'mindenes' VS projektet és minnél hamarabb ültesd a gyakorlatba.
        }

        static int Pormennyiseg(string[,] forras)
        {
            int db = 0;
            for (int i = 0; i < forras.GetLength(0); i++)
                for (int j = 0; j < forras.GetLength(1); j++)
                    if (forras[i, j] == "p") db++;
            return db;
        }

        static string[,] MezotModosit(string[,] forras)
        {
            Console.Write("Kérem az X koordinátát: ");
            int x = int.Parse(Console.ReadLine());
            x--;
            Console.Write("Kérem az Y koordinátát: ");
            int y = int.Parse(Console.ReadLine());
            y--;
            Console.Write("Kérek egy karaktert: ");
            string k = Console.ReadLine();
            forras[x, y] = k;
            return forras;
        }

        static double[] Pormegoszlas(string[,] forras)
        {
            int összespor = Pormennyiseg(forras);
            var megoszlas = new double[10];

            for (int i = 0; i < forras.GetLength(0); i++)
            {
                int x = 0;
                int db = 0;
                for (int j = 0; j < 20; j++)
                {
                    if (forras[i, j] == "p")
                    {
                        db++;
                    }
                }
                megoszlas[x] = (db / összespor) * 100;
                x++;
            }
            return megoszlas;
        }

        static int LegkevesebbPor(double[] forras)
        {
            int min = 0;
            for (int i = 1; i < forras.Length; i++)
            {
                if (forras[i] > forras[min])
                    min = i;
            }
            return min;
        }

        static void CsokkenobeRendez(double[] forras)
        {
            for (int i = 0; i < forras.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < forras.Length; j++)
                {
                    if (forras[min] > forras[j])
                    {
                        min = j;
                    }
                }
                Csere(ref forras[min], ref forras[i]);
            }
        }

        static void Csere(ref double a, ref double b)
        {
            // var kulcsszó, segít a vizsgán hogy gyorsan beírd a tipusnevet.
            // (erröl lehet hogy valakit meg kell kérdezned hátha nem fogadják el.)
            var seged = a;
            a = b;
            b = seged;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("p = por");
            Console.WriteLine("s = szikla");
            Console.WriteLine("i = idegen");
            Console.WriteLine("e = ember");
            Console.WriteLine();

            var harcter = HarcterGeneralas();
            AdatokMegjelenites(harcter);
            
            Console.WriteLine(Pormennyiseg(harcter) + " db por található");

            // Teljesítménybeli gondot okozhat ha 
            // cikluson belül hívsz meg egy metódust.
            // Ilyenkor érdemes egy változóba helyezni a metódus értékét
            // legalább a ciklus idejére.
            var por = Pormegoszlas(harcter);
            for (int i = 0; i < por.Length; i++)
            {
                Console.WriteLine(por[i]);
            }
            Console.WriteLine();

            // Vagy az ellenkezője, ha csak egyszer használom egy metódus
            // kimenetelét akkor minek mentsem el? Ráadásul ez vizsgán időt is spórol.
            Console.WriteLine(LegkevesebbPor(por));
            CsokkenobeRendez(por);

            foreach (var p in por)
            {
                Console.Write(p + " ");

            }
            Console.WriteLine();
            MezotModosit(harcter);
            AdatokMegjelenites(harcter);
            Console.ReadKey();
        }
    }
}
