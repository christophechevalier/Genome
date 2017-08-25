using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Tools
{
    class Message
    {
        private string message;
        private string ipCalculateur;
        private string job;
        private int actionKey;

        #region Constructeur
        public Message(string message, string job, int key, string ipCalculateur)
        {
            this.message = message;
            this.ipCalculateur = ipCalculateur;
            this.job = job;
            this.actionKey = key;
        }

        public string GetMessage()
        {
            return message;
        }

        public string GetIpCalculateur()
        {
            return ipCalculateur;
        }

        public string GetJob()
        {
            return job;
        }

        public int GetKey()
        {
            return actionKey;
        }
        #endregion
    }
}
