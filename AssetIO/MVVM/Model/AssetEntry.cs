using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetIO.MVVM.Model
{
    public enum IO
    {
        Out,
        In
    }
    public class AssetEntry
    {
        public AssetEntry()
        {
            
        }
        public AssetEntry(DateTime DateTime, string Name, string Identifier, IO Direction)
        {
            this.datetime = DateTime;
            this.technician = Name;
            this.identifier = Identifier;
            this.direction = Direction;
        }
        public DateTime datetime { get; set; }
        public string technician { get; set; }
        public string identifier { get; set; }
        public IO direction { get; set; }

        public string ToString()
        {
            string e = String.Join(",", datetime.ToString().TrimEnd('\r','\n'), technician.TrimEnd('\r', '\n'), identifier.TrimEnd('\r', '\n'), direction);
            //Debug.WriteLine(e);
            return e;
        }
    }
}
