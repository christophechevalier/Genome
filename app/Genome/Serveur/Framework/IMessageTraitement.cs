using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Framework
{
    public interface IMessageTraitement
    {
        string SetTypeTraitement(string type);
        string SetContenu(List<string> chunk);
        int SetPosition(int position);
        int SetId(int id);
    }
}
