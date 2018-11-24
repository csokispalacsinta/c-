using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace LabirintusAlpha
{
    class Program
    {
        static void Pályamenü()
        {
            Console.Clear();
            Console.CursorVisible = false;
            string[] pályák = new string[6];
            pályák[0] = "Labirintus 1";
            pályák[1] = "Labirintus 2";
            pályák[2] = "Labirintus 3";
            pályák[3] = "Labirintus 4";
            pályák[4] = "Labirintus 5";
            pályák[5] = "Vissza";
            int válasz;
            do
            {
                válasz = Irányítás(pályák);
                switch (válasz)
                {
                    case 0:
                        Console.Clear();
                        string[,] labirintus1 = Beolvas(pályák[0]);
                        Játék(labirintus1,pályák[0]);
                        break;
                    case 1:
                        Console.Clear();
                        string[,] labirintus2 = Beolvas(pályák[1]);
                        Játék(labirintus2, pályák[1]);
                        break;
                    case 2:
                        Console.Clear();
                        string[,] labirintus3 = Beolvas(pályák[2]);
                        Játék(labirintus3, pályák[2]);
                        break;
                    case 3:
                        Console.Clear();
                        string[,] labirintus4 = Beolvas(pályák[3]);
                        Játék(labirintus4, pályák[3]);
                        break;
                    case 4:
                        Console.Clear();
                        string[,] labirintus5 = Beolvas(pályák[4]);
                        Játék(labirintus5, pályák[4]);
                        break;
                }
            } while (válasz != 5);
            Console.Clear();
        }

        static void Főmenü()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
            string[] menüpontok = new string[4];
            menüpontok[0] = " Új Játék ";
            menüpontok[1] = " Toplista ";
            menüpontok[2] = " Súgó ";
            menüpontok[3] = " Kilépés ";
            int válasz;
            do
            {
                válasz = Irányítás(menüpontok);
                switch (válasz)
                {
                    case 0:
                        Console.Clear();
                        Pályamenü();
                        break;
                    case 1:
                        Console.Clear();
                        string[] toplista = ToplistaBeolvas();
                        ToplistaKiír(toplista);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Súgó lenne");
                        Console.ReadLine();
                        break;
                }
            } while (válasz != 3);
        }

        static int Irányítás(string[] menüpontok)
        {
            int aktuálismenüpont = 0;
            ConsoleKeyInfo gomb;
            do
            {
                for (int i = 0; i < menüpontok.Length; i++)
                {
                    Console.SetCursorPosition(0, i);
                    if (aktuálismenüpont == i)
                        Console.BackgroundColor = ConsoleColor.Magenta;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(
                        );
                    Console.Write(menüpontok[i]);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                gomb = Console.ReadKey(true);
                if (gomb.Key == ConsoleKey.DownArrow)
                    aktuálismenüpont++;
                else if (gomb.Key == ConsoleKey.UpArrow)
                    aktuálismenüpont--;
                else if (gomb.Key == ConsoleKey.Enter)
                    return aktuálismenüpont;

                if (aktuálismenüpont < 0)
                    aktuálismenüpont = menüpontok.Length - 1;
                else if (aktuálismenüpont > menüpontok.Length - 1)
                    aktuálismenüpont = 0;
            } while (gomb.Key != ConsoleKey.Enter);
            Console.Clear();
            return -1;
        }

        static string[,] Beolvas(string labirintusnév)
        {
            string[,] labirintus = new string[21, 71];
            string[] seged = File.ReadAllLines(labirintusnév += ".txt");
            for (int i = 0; i < labirintus.GetLength(0); i++)
            {
                for (int j = 0; j < labirintus.GetLength(1); j++)
                {
                    labirintus[i, j] = seged[i].Substring(j, 1);
                }
            }
            return labirintus;
        }

        static void Kiír(string[,] labirintus)
        {
            for (int i = 0; i < labirintus.GetLength(0); i++)
            {
                for (int j = 0; j < labirintus.GetLength(1); j++)
                {
                    if (labirintus[i, j] == "*")
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;
                    if (j == labirintus.GetLength(1) - 1)
                        Console.Write(labirintus[i, j] + "\r\n");
                    else
                        Console.Write(labirintus[i, j]);
                }
            }
            Console.WriteLine();
        }

        static string[,] Kezdőállás(string[,] labirintus)
        {
            int i = 0;
            while (i < labirintus.GetLength(0) && labirintus[i, 0] != " ")
            {
                i++;
            }
            labirintus[i, 0] = "*";
            return labirintus;
        }

        static int Keres(string[,] labirintus)
        {
            int i = 0;
            while (i < labirintus.GetLength(0) && labirintus[i, 0] != "*")
            {
                i++;
            }
            return i;
        }

        static void Játék(string[,] labirintus,string pályanév)
        {
            Kezdőállás(labirintus);
            int x = Keres(labirintus);
            int y = 0;
            ConsoleKeyInfo gomb;
            Kiír(labirintus);
            Stopwatch stopper = new Stopwatch();
            stopper.Start();
            do
            {
                Console.Clear();
                Kiír(labirintus);
                Console.WriteLine("Eddigi időd: {0}", stopper.Elapsed);
                gomb = Console.ReadKey(true);
                if (gomb.Key == ConsoleKey.UpArrow)
                {
                    if (x - 1 >= 0 && labirintus[x - 1, y] == " ")
                    {
                        x--;
                        labirintus[x + 1, y] = " ";
                        labirintus[x, y] = "*";

                    }
                }
                if (gomb.Key == ConsoleKey.DownArrow)
                {
                    if (x + 1 <= labirintus.GetLength(0) && labirintus[x + 1, y] == " ")
                    {
                        x++;
                        labirintus[x - 1, y] = " ";
                        labirintus[x, y] = "*";

                    }
                }
                if (gomb.Key == ConsoleKey.LeftArrow)
                {
                    if (y - 1 >= 0 && labirintus[x, y - 1] == " ")
                    {
                        y--;
                        labirintus[x, y + 1] = " ";
                        labirintus[x, y] = "*";

                    }
                }
                if (gomb.Key == ConsoleKey.RightArrow)
                {
                    if (labirintus[x, y + 1] == " ")
                    {
                        y++;
                        labirintus[x, y - 1] = " ";
                        labirintus[x, y] = "*";

                    }
                }
                if (gomb.Key == ConsoleKey.Escape)
                {
                    stopper.Stop();
                    ConsoleKeyInfo esc;
                    do
                    {
                        esc = Console.ReadKey(true);
                        Console.Clear();
                        Console.WriteLine("A játék szünetel!");
                        Console.WriteLine("A folytatáshoz nyomj Esc-et!");
                    } while (esc.Key != ConsoleKey.Escape);
                    stopper.Start();
                }

            } while (y != labirintus.GetLength(1) - 1);
            stopper.Stop();
            string idő = stopper.Elapsed.ToString();
            Console.Clear();
            Console.WriteLine("Győztél!");
            Console.WriteLine("Nyomj egy gombot!");
            Console.ReadKey();
            string[] toplista=ToplistaBeolvas();
            string[,] pályatoplista = ToplistaPályánként(pályanév, toplista);
            bool van = ToplistábaKerülhetE(pályatoplista,idő);
            int hely = HolAToplistában(pályatoplista, idő);
            if (van==true)
            {
                Console.WriteLine("Kérem a nevedet, mert felkerültél a toplistára!(Szóköz ne legyen benne!)");
                string név = Console.ReadLine();
                string időésnév = idő+":"+név;
                ToplistaÁtír(toplista, pályanév, időésnév, hely);
                Fájlbaír(toplista);
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }

        static string[] ToplistaBeolvas()
        {
            string[] toplista = File.ReadAllLines("Toplista.txt",Encoding.Default);
            return toplista;
        }

        static void ToplistaKiír(string[] toplista)
        {
            for (int i = 0; i < toplista.Length; i++)
            {
                Console.WriteLine(toplista[i]);
            }
        }

        static string[,] ToplistaPályánként(string pályanév,string[] toplista)
        {
            string[,] pályatoplista = new string[5, 2];
            string[] seged = new string[5];
            for (int i = 0; i < toplista.Length; i++)
            {
                if (toplista[i]==pályanév)
                {
                    for (int j = i+1; j < i+6; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                           seged[k] = toplista[j];
                        }
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                string[] seged2=seged[i].Split(' ');
                for (int j = 0; j < 2; j++)
                {
                    pályatoplista[i, j] = seged2[j];
                }
            }
            return pályatoplista;
        }

        static bool ToplistábaKerülhetE(string[,] pályatoplista,string idő)
        {
            if (DateTime.ParseExact(idő,"HH:mm:ss.fffffff",CultureInfo.InvariantCulture) < DateTime.ParseExact(pályatoplista[0, 0], "HH:mm:ss.fffffff", CultureInfo.InvariantCulture))
            {
                return true;
            }
            else
                return false;
        }

        static int HolAToplistában(string[,] pályatoplista, string idő)
        {
            int i = 0;
            while (DateTime.ParseExact(pályatoplista[i,0], "HH:mm:ss.fffffff", CultureInfo.InvariantCulture) >DateTime.ParseExact(idő,"HH:mm:ss.fffffff",CultureInfo.InvariantCulture))
            {
                i++;
            }
            return i;
        }

        static string[] ToplistaÁtír(string[] toplista,string pályanév,string időésnév, int hely)
        {
            for (int i = 0; i < toplista.Length; i++)
            {
                if(toplista[i]==pályanév)
                {
                    toplista[i + hely] = időésnév;
                }
            }
            return toplista;
        }

        static void Fájlbaír(string[] toplista)
        {
            File.WriteAllLines("Toplista.txt", toplista);
        }

        static void Main(string[] args)
        {
            Főmenü();
        }
    }
}
