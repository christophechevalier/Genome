using Serveur.Entity;
using Serveur.Systems;

namespace Serveur.Tools
{
    public class InterpretMessageCalculateur
    {
        SystemCalculateur systemCalc;

        #region Constructeur
        public InterpretMessageCalculateur(SystemCalculateur system)
        {
            this.systemCalc = system;
        }
        #endregion

        public void RetrieveMessage(ChunkData chunk)
        {
            systemCalc.threadM.traitement(chunk.Chunk, chunk.Job);
        }
    }
}
