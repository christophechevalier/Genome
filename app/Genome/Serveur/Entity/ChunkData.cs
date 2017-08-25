using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Entity
{
    [Serializable]
    class ChunkData
    {
        public long Id { get; set; }
        public List<string> Chunk { get; set; }
        public string Job { get; set; }
    }
}
