using System;
using bronzetti.christian._4h.SaveRecord.Models;
namespace bronzetti.christian._4h.SaveRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SaveRecord - 2021 - christian bronzetti");

            //leggere file cvs voi comuni e trasformarlo in una list <comune>
            Comuni c = new Comuni("CodiciComuni.csv");
           
            // scrivere la list in file.bin
            c.Save();

            Console.WriteLine($"Ecco la riga 1000 dopo il dave\n{c[5]}\n");

            //rileggere il file binario in una list<Comune>
            c.Load();

            Console.WriteLine($"Ecco la riga 1000 dopo load \n{c[5]}\n");
            Console.WriteLine(c.RicercaComune(Convert.ToInt32(100)));
        }
    }
}
