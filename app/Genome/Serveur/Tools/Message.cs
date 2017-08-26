using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Tools
{
    [Serializable]
    public class Message
    {
        public string message;
        public string ipCalculateur;
        public string job;
        public int actionKey;

        #region Constructeur
        public Message(string message, string job, int key, string ipCalculateur)
        {
            this.message = message;
            this.ipCalculateur = ipCalculateur;
            this.job = job;
            this.actionKey = key;
        }
        #endregion

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
    }
}
