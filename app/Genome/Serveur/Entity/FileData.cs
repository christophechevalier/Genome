using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Entity
{
    public class FileData
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public float FileSize { get; set; }
        public byte[] Content { get; set; }
    }
}
