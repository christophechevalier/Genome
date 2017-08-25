using Serveur.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Entity
{
    [Serializable]
    public class FileData
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public float FileSize { get; set; }
        public byte[] Content { get; set; }
        //public Job Job;
    }
}
