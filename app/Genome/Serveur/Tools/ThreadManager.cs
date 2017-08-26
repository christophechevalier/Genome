using Serveur.Applicatifs;
using Serveur.Entity;
using System.Collections.Generic;
using System.Threading;

namespace Serveur.Tools
{
    public class ThreadManager
    {
        RepertoireTraitement rep = new RepertoireTraitement();
        delegate string ModuleAction(string s);
        ModuleAction mod;

        public void traitement(List<string> args, Job y)
        {
            foreach (string chunkLine in args)
            {
                switch (y)
                {
                    case Job.AnalyseQuantitative:
                        mod = rep.countLetters;
                        break;
                    case Job.RechercherSequence:
                        break;
                    case Job.TransformerSequence:
                        break;
                    case Job.TrouverGene:
                        break;
                    case Job.PredireYeux:
                        break;
                }

                //Démarrage de la mise à la queue du pool
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    string resultat = mod(chunkLine);
                });
            }
        }
    }
}
