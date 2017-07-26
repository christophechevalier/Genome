using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Framework
{
    interface ISerializer
    {
        byte[] Serialize(Object obj);
        Object Deserialize<T>(byte[] arrayBytes);
    }
}
