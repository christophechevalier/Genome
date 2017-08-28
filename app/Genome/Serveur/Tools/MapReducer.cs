using Serveur.Entity;
using Serveur.Systems;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Serveur.Tools
{
    class MapReducer
    {
        SystemOrchestrateur systemOrch;
        string file;
        int index = 0;
        int indexCalculateur = 0;

        public MapReducer(string file, SystemOrchestrateur systemOrch)
        {
            this.file = file;
            this.systemOrch = systemOrch;
            while (index < 999)
            {
                CreateChunk();
                Thread.Sleep(250);
            }
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
            chunkPrepared.Job = systemOrch.jobAsked;
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
                byte[] endEnvoi = Encoding.ASCII.GetBytes("<EOF>");
                //Thread.Sleep(10000);
                byte[] content = new byte[objectSerialized.Length + endEnvoi.Length];
                for (int i = 0; i < objectSerialized.Length; i++)
                {
                    content.SetValue(objectSerialized[i], i);
                }
                for (int i = 0; i < endEnvoi.Length; i++)
                {
                    content.SetValue(endEnvoi[i], objectSerialized.Length + i);
                }

                connexion.SendMessage(calculateur, content);
            }
            else
            {
                return;
            }
            
        }

        Calculateur FindCalculateur()
        {
            bool send = false;
            if(systemOrch.listeCalculateurs.Count != 0)
            {
                while (!send)
                {
                    if (systemOrch.listeCalculateurs[indexCalculateur].Status == Status.Connecte)
                    {
                        int realIndex = indexCalculateur;
                        if (indexCalculateur < systemOrch.listeCalculateurs.Count - 1)
                        {
                            indexCalculateur++;
                        }
                        else
                        {
                            indexCalculateur = 0;
                        }
                        send = true;
                        return systemOrch.listeCalculateurs[realIndex];
                    }
                    else
                    {
                        if (indexCalculateur < systemOrch.listeCalculateurs.Count - 1)
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
