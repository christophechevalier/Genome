using Serveur.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.CAD
{
    #region Constructeur
    public class OrchestrateurCAD : INotifyPropertyChanged
    {
        #region Propriétés
        private ObservableCollection<Calculateur> _calculateurs;
        public ObservableCollection<Calculateur> Calculateurs
        {
            get
            {
                if (this._calculateurs == null)
                {
                    this._calculateurs = new ObservableCollection<Calculateur>();
                }
                return this._calculateurs;
            }
            set { _calculateurs = value; }
        }
        #endregion

        #region Events
        private void Calculateurs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Calculateurs"));
            }
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Calculateurs"));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructeur
        public OrchestrateurCAD()
        {
            // Instanciations
            Calculateurs = new ObservableCollection<Calculateur>();
            _calculateurs.CollectionChanged += Calculateurs_CollectionChanged;
        }
        #endregion
    }
    #endregion
}