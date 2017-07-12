using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Entity
{
    class Calculateur
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public int NbCore { get; set; }
        public int Cpu { get; set; }
        public string Status { get; set; }
        public int Task { get; set; }
    }
}
