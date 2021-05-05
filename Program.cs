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
             Console.WriteLine($"Ho scritto {c.Count} comuni");

            //rileggere il file binario in una list<Comune>
            c.Load();
            Console.WriteLine($"Ho letto {c.Count} comuni");
            
            Console.WriteLine(c.RicercaComune(Convert.ToInt32(100)));
        }
    }
}
