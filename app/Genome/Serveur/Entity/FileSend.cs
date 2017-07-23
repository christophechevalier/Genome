using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Entity
{
    class FileSend
    {
        public int id { get; set; }
        float fileSize { get; set; }
        byte[] content { get; set; }
    }
}
