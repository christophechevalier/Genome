using Serveur.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Tools
{
    class MessageTreatment : IMessageTraitement
    {
        private string chunk;
        private string contenu;
        private int id;
        private int position;
        private string typeTraitement;

        // Constructeur
        public MessageTreatment(List<string> chunk, int id, string type, int position)
        {
            contenu = SetContenu(chunk);
            this.id = SetId(id);
            this.position = SetPosition(position);
            typeTraitement = SetTypeTraitement(type);
        }

        // Set tous le contenu du génome envoyé et les sépare avec le caractère '/'
        public string SetContenu(List<string> chunk)
        {
            string finalValue = "";
            foreach(string content in chunk)
            {
                // Séparation sur le 4eme champ du chunk
                string[] values = content.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
                // Ajout de la valeur dans la valeur final
                finalValue += values[3] + '/';
            }
            return finalValue;
        }

        // Set l'Id de l'opération
        public int SetId(int id)
        {
            return id;
        }

        // Set la position dans le tableau général de l'opération
        public int SetPosition(int position)
        {
            return position;
        }
        
        // Set le type de traitement demandé par l'utilisateur
        public string SetTypeTraitement(string type)
        {
            return type;
        }

        // Renvois le message final contenant toutes les informations à envoyer
        public string GetMessage()
        {
            string message = id + ";" + position + ";" + typeTraitement + ";" + contenu;
            return message;
        }
    }
}
