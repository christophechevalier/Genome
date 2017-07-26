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
        private int idCalculateur;
        private string job;

        #region Constructeur
        public Message(string message, int idCalculateur, string job)
        {
            this.message = message;
            this.idCalculateur = idCalculateur;
            this.job = job;
        }

        public string GetMessage()
        {
            return message;
        }

        public int GetIdCalculateur()
        {
            return idCalculateur;
        }

        public string GetJob()
        {
            return job;
        }
        #endregion
    }
}
