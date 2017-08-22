using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Serveur.Applicatifs
{
    class RepertoireTraitement
    {
        public void countLetters(string chaine)
        {
            Console.WriteLine("Passage");

            int countA = 0;
            int countC = 0;
            int countG = 0;
            int countT = 0;
            int countS = 0;
            int countI = 0;
            foreach(char c in chaine)
            {
                switch (c)
                {
                    case 'A':
                        countA++;
                        break;
                    case 'C':
                        countC++;
                        break;
                    case 'G':
                        countG++;
                        break;
                    case 'T':
                        countT++;
                        break;
                    case '-':
                        countS++;
                        break;
                    default:
                        countI++;
                        break;
                }
            }
            Thread.Sleep(2000);
            Console.WriteLine("Nombre de A dans la chaine : " + countA);
            Console.WriteLine("Nombre de C dans la chaine : " + countC);
            Console.WriteLine("Nombre de G dans la chaine : " + countG);
            Console.WriteLine("Nombre de T dans la chaine : " + countT);
            Console.WriteLine("Nombre d'inconnu dans la chaine : " + countS);
            Console.WriteLine("Nombre de caractère ignoré dans la chaine : " + countI);
        }

        public void searchSequence(string chaine)
        {

        }
    }
}
