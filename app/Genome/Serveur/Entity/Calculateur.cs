using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Entity
{
    public class Calculateur
    {

        private string _ip;
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
        private Status _status;
        public Status Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}