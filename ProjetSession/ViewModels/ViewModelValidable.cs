using ProjetSession.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ProjetSession.ViewModels
{
    /// <summary>
    /// Classe de base pour les ViewModels de l'application qui implémente l'interface INotifyDataErrorInfo.
    /// Une partie du code vient des notes de cours.
    /// PS: J'avais commencé à écrire cette classe avant de voir qu'il l'avait dans les notes de cours.
    /// </summary>
    public abstract class ViewModelValidable : ViewModelBase, INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected Dictionary<string, ObservableCollection<ValidationResult>> _errors = new Dictionary<string, ObservableCollection<ValidationResult>>();

        /// <summary>
        /// Retourne si des erreurs sont présentes dans le dictionnaire
        /// </summary>
        /// <value>Vrai si le dictionnaire n'est pas vide</value>
        public bool HasErrors
        {
            get
            {
                bool hasErrors = false;
                foreach (ObservableCollection<ValidationResult> listeErreurs in _errors.Values)
                {
                    if (listeErreurs.Any())
                    {
                        hasErrors = true;
                        break;
                    }
                }
                return hasErrors;
            }
        }

        public bool EstValide => !HasErrors;

        /// <summary>
        /// Retourne la collection d'erreurs de la propriété passée en paramètre à partir du dictionnaire d'erreurs
        /// </summary>
        /// <param name="propriete">Propriété pour laquelle on veut obtenir la collection d'erreurs</param>
        public IEnumerable GetErrors(string propriete)
        {
            if (_errors.ContainsKey(propriete))
            {
                return _errors[propriete];
            }
            else
            {
                return new ObservableCollection<ValidationResult>();
            };
        }

        /// <summary>
        /// Retourne les erreurs associées à la propriété passée en paramètre sous forme d'une collection observable
        /// </summary>
        public ObservableCollection<ValidationResult> GetObservableErrors(string propriete)
        {
            return (ObservableCollection<ValidationResult>)GetErrors(propriete);
        }

        /// <summary>
        /// Ajoute au dictionnaire des erreurs la liste d'erreurs (passée en paramètre) à la propriété passée en paramètre
        /// </summary>
        /// <param name="erreursValidation">Liste des erreurs de validation retournée par le validateur</param>
        /// <param name="propriete">Propriété pour laquelle on ajoute des erreurs</param>
        private void AjoutErreurs(List<ValidationResult> erreursValidation, [CallerMemberName] string propriete = "")
        {
            if (!_errors.ContainsKey(propriete))
            {
                _errors.Add(propriete, new ObservableCollection<ValidationResult>());
            }
            foreach (ValidationResult erreur in erreursValidation)
            {
                _errors[propriete].Add(erreur);
            }
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propriete));
            RaisePropertyChanged(nameof(EstValide));
        }

        /// <summary>
        /// Efface dans le dictionnaire d'erreurs la collection d'erreurs pour la propriété passée en paramètre
        /// </summary>
        /// <param name="propriete">La propriété pour laquelle on veut vider la collection d'erreurs</param>
        private void EffacerErreurs([CallerMemberName] string propriete = "")
        {
            if (_errors.ContainsKey(propriete))
            {
                _errors[propriete].Clear();
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propriete));
                RaisePropertyChanged(nameof(EstValide));
            }
        }

        /// <summary>
        /// Permet de valider la valeur passée en paramètre selon les annotations de validation de la propriété passée en paramètre
        /// </summary>
        /// <param name="value">Valeur qu'on veut attribuer à la propriété</param>
        /// <param name="propriete">Propriété (qui contient les annotations de validation)</param>
        public bool Validate<T>(T value, [CallerMemberName] string propriete = "")
        {
            EffacerErreurs(propriete);
            List<ValidationResult> erreursValidation = new List<ValidationResult>();
            ValidationContext contexte = new ValidationContext(this, null, null) { MemberName = propriete };
            bool proprieteValide = Validator.TryValidateProperty(value, contexte, erreursValidation);

            if (!proprieteValide)
            {
                AjoutErreurs(erreursValidation, propriete);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
