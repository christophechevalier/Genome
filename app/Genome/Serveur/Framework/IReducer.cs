using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Framework
{
    interface IReducer
    {
        List<string> CreateChunk();
    }
}
