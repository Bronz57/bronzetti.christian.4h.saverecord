using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Collections.Generic;

using System.Security.Cryptography;
using System;


namespace bronzetti.christian._4h.SaveRecord.Models
{
    public class Comune
    {
        private string _nomeComune;
        private string _codiceCatastale;
        public int ID { get; set; }
        public string NomeComune 
        { 
            get=>_nomeComune; 
            set
            {
               _nomeComune = ControlloInserimento(value, 22);
            }
        }
        public string CodiceCatastale 
        { 
            get => _codiceCatastale; 
            set
            {
                _codiceCatastale = ControlloInserimento(value, 4);
            }
        }

        private string ControlloInserimento(string str, int lunghezza)
        {
            if(str.Length==lunghezza)
                return str;

            if(str.Length<lunghezza)
                str=str.PadRight(lunghezza);
            
            if(str.Length<lunghezza)
                str=str.Substring(0,lunghezza);

            return str;
                
        }
        public Comune(){}
        public Comune(string riga, int id)
        {
            string[] colonna = riga.Split(',');
            
            ID = id;
            CodiceCatastale = colonna[0];
            NomeComune = colonna[1];

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Codice catastale:\t{_codiceCatastale}\t");
            sb.Append($"Nome comune:\t{_nomeComune}\t");

            return sb.ToString();
        }
    }

    public class Comuni : List<Comune> //comuni è una List<Comune>
    {
        public string NomeFile {get;}
        public Comuni(){}

        public Comuni(string fileName)
        {
            NomeFile = fileName;
            using (FileStream fin = new  FileStream(fileName, FileMode.Open))
            {
                using(StreamReader reader = new StreamReader(fin))
                    { 
                        int i =0;

                        while( !reader.EndOfStream )
                        {
                            string riga = reader.ReadLine();
                            this.Add(new Comune(riga, ++i)); //this ridondante
                        }
                    }
            }
        }

        public void Save()
        {
            string fileName = NomeFile.Split('.')[0]+".bin";
            Save(fileName);
        }
        
        public void Save(string fileName)
        {
            
            using(FileStream fout = new FileStream(fileName, FileMode.Create))
            {
                using(BinaryWriter writer = new BinaryWriter(fout))
                {
                    foreach(Comune c in this)
                    {
                        // writer.Seek((c.ID - 1) * 32, SeekOrigin.Begin);
                        writer.Write(c.ID);
                        writer.Write(c.CodiceCatastale);
                        writer.Write(c.NomeComune);
                    }

                    writer.Flush();
                }
            }
        }

        public void Load()
        {
            string fileName = NomeFile.Split('.')[0]+".bin";
            
            Load(fileName);
        }

        public void Load(string fileName)
        {
            this.Clear();

            using(FileStream fin = new FileStream(fileName, FileMode.Open))
            {
                using(BinaryReader reader = new BinaryReader(fin))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        Add(new Comune
                        {
                            ID = reader.ReadInt32(), 
                            CodiceCatastale = reader.ReadString(), 
                            NomeComune = reader.ReadString()
                        });
                    }
                }
            }
        }
        public Comune RicercaComune(int numero)
        {
            Comune c1;
            using(FileStream fin = new FileStream("CodiciComuni.bin", FileMode.Open))
            {
                c1 = new Comune();
                using(BinaryReader reader = new BinaryReader(fin))
                {                   
                    fin.Seek((numero-1)*32, SeekOrigin.Begin);
                    c1.ID = reader.ReadInt32();
                    c1.CodiceCatastale = reader.ReadString();
                    c1.NomeComune = reader.ReadString();
                }
    
            }

            return c1;
        }
    }
}