using Serveur.Applicatifs;
using System.Threading;

namespace Serveur.Tools
{
    public class ThreadManager
    {
        RepertoireTraitement rep = new RepertoireTraitement();
        delegate void ModuleAction(string s);
        ModuleAction mod;
        public enum Job
        {
            AnalyseQuantitative,
            RechercherSequence,
            TransformerSequence,
            TrouverGene,
            PredireYeux
        };

        public void traitement(int taille, string args, Job y)
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
                mod(args);
            });
        }
    }
}
