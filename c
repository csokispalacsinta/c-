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
            string[,] harcter = new string[10, 20];
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (r.Next(101)<30)
                    {
                        harcter[i, j] = "s";
                    }
                    if (i < 2 && r.Next(101) < 50)
                    {
                        harcter[i, j] = "s";
                    }
                    else
                        if (i<2)
                        harcter[i, j] = "p";
                    if (i>1)
                    {
                        if (r.Next(101)<50)
                        {
                            harcter[i, j] = "p";
                        }
                        else
                        {
                            if (r.Next(101) < 50)
                            {
                                harcter[i, j] = "i";
                            }
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
        }

        static int Pormennyiseg(string[,] forras)
        {
            int db = 0;
            for (int i = 0; i < forras.GetLength(0); i++)
            {
                for (int j = 0; j < forras.GetLength(1); j++)
                {
                    if (forras[i, j] == "p")
                    {
                        db++;
                    }
                }
            }
            return db;
        }

        static string[,] MezotModosit(string[,] forras)
        {
            Console.WriteLine("Kérek 2 koordinátát és egy karaktert");
            int x = int.Parse(Console.ReadLine());
            x--;
            int y = int.Parse(Console.ReadLine());
            y--;
            string k = Console.ReadLine();
            forras[x, y] = k;
            return forras;
        }

        static double[] Pormegoszlas(string[,] forras)
        {
            int összespor = Pormennyiseg(forras); 
            double[] megoszlas = new double[10];
            for (int i = 0; i < forras.GetLength(0); i++)
            {
                int x = 0;
                int db = 0;
                for (int j = 0; j < 20; j++)
                {
                    if (forras[i,j] == "p")
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

        static void Csere(ref double a,ref double b)
        {
            double seged = a;
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
            string[,] harcter = HarcterGeneralas();
            AdatokMegjelenites(harcter);
            int db = Pormennyiseg(harcter);
            Console.WriteLine(db+" db por található");
            for (int i = 0; i < Pormegoszlas(harcter).Length; i++)
            {
                Console.WriteLine(Pormegoszlas(harcter)[i]);
            }
            Console.WriteLine();
            int min = LegkevesebbPor(Pormegoszlas(harcter));
            Console.WriteLine(min);
            CsokkenobeRendez(Pormegoszlas(harcter));
            foreach (var item in Pormegoszlas(harcter))
            {
                Console.Write(item+" ");
            }
            Console.WriteLine();
            MezotModosit(harcter);
            AdatokMegjelenites(harcter);
            Console.ReadKey();
        }
    }
}
