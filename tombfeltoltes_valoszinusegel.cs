using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace tombfeltoltes_valoszinusegel
{
    class Program
    {
        // Tomb elérhetővé tétele az osztályon belül
        private double[] tomb; // = this.tomb

        private Program TombMutatas()
        {
            foreach (double elem in this.tomb)
            {
                Console.Write(elem.ToString() + ", ");
            }
            return this;
        }

        private Program TombFeltoltes()
        {
            this.tomb = new double[10];
            Random r = new Random();

            for (int i = 0; i < tomb.Length; i++)
            {
                // random(0, 1) <= X ami (X * 100)% ban lehetséges
                // X egy 0 és 1 közti szám, ami 100 el szorozva a százalék
                // random(0, 1) = 0 és 1 közötti szám
                if (r.NextDouble() <= 0.7)  // 70% eséllyel lesz 1 és 2 közötti szám
                {
                    tomb[i] = r.Next(1, 2 + 1);
                }
                else if (r.NextDouble() <= 0.2) // 20% eséllyel 2 és 3 közötti szám      
                {
                    tomb[i] = r.Next(2, 3 + 1);
                }
                else if (r.NextDouble() <= 0.1)  // 10% eséllyel 3 és 4 közötti szám
                {
                    tomb[i] = r.Next(3, 4 + 1);
                }
            }
            return this;
        }

        static void Main(string[] args)
        {
            new Program()
                .TombFeltoltes()
                .TombMutatas();
            Console.ReadKey();
        }
    }
}
