using Serveur.Entity;
using Serveur.Framework;
using Serveur.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Tools
{
    class MapReducer
    {
        SystemOrchestrateur connexionOrchestrateur;
        string file;
        int index = 0;
        int indexCalculateur = 0;
        bool canCreateChunk = true;
        public MapReducer(string file, SystemOrchestrateur connexionOrchestrateur)
        {
            this.file = file;
            this.connexionOrchestrateur = connexionOrchestrateur;
            while (index < 999)
            {
                CreateChunk();
            }
            //for(int i = 0; i <= 1000 ; i++)
            //{
            //    canCreateChunk = true;
            //    CreateChunk();
            //}
            //canCreateChunk = false;
        }

        void CreateChunk()
        {
            List<string> chunk = new List<string>();
            string[] lines = File.ReadAllLines(file);
            int numberMaxLine = lines.Length;
            int chunkPart = numberMaxLine / 1000;
            int chunkState = chunkPart * index;
            for (int i = chunkState; i <= chunkState + chunkPart; i++)
            {
                chunk.Add(lines[i]);
            }
            ChunkData chunkPrepared = new ChunkData();
            chunkPrepared.Id = index;
            chunkPrepared.Chunk = chunk;
            chunkPrepared.Job = connexionOrchestrateur.jobAsked;
            SendChunck(chunkPrepared);
            index++;
        }

        void SendChunck(ChunkData chunkData)
        {
            Calculateur calculateur = FindCalculateur();
            if(calculateur != null)
            {
                Connexion connexion = new Connexion();
                ObjectSerializer serializer = new ObjectSerializer();
                byte[] objectSerialized = serializer.Serialize(chunkData);
                connexion.SendMessage(calculateur, objectSerialized);
            }
            else
            {
                return;
            }
            
        }

        Calculateur FindCalculateur()
        {
            bool send = false;
            if(connexionOrchestrateur.listeCalculateurs.Count != 0)
            {
                while (!send)
                {
                    if (connexionOrchestrateur.listeCalculateurs[indexCalculateur].Status == Status.Connecte)
                    {
                        int realIndex = indexCalculateur;
                        if (indexCalculateur < connexionOrchestrateur.listeCalculateurs.Count - 1)
                        {
                            indexCalculateur++;
                        }
                        else
                        {
                            indexCalculateur = 0;
                        }
                        send = true;
                        return connexionOrchestrateur.listeCalculateurs[realIndex];
                    }
                    else
                    {
                        if (indexCalculateur < connexionOrchestrateur.listeCalculateurs.Count - 1)
                        {
                            indexCalculateur++;
                        }
                        else
                        {
                            indexCalculateur = 0;
                        }
                    }
                }
            }
            return null;
        }
    }
}
