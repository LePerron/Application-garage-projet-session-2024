using ProjetSession.Data;
using ProjetSession.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetSession.ViewModels
{
    public class MainViewModelHistoriqueEntretiens : ViewModelBase
    {
        private IEntretiensDataProvider _entretienDataProvider;
        private IVehiculeTerrestreDataProvider _vehiculeTerrestreDataProvider;

        public ObservableCollection<EntretienViewModel> EntretiensMoteur { get; set; }
        public ObservableCollection<EntretienViewModel> EntretiensHuile { get; set; }
        public ObservableCollection<EntretienViewModel> EntretiensTransmission { get; set; }
        public ObservableCollection<EntretienViewModel> EntretiensPneus { get; set; }

        public ObservableCollection<VehiculeTerrestreViewModel> ListeVehicules { get; set; }
        public ObservableCollection<string> ListeVinVehicules { get; set; }

        private EntretienViewModel _entretienMoteurSelectionnee;
        public EntretienViewModel EntretienMoteurSelectionnee
        {
            get => _entretienMoteurSelectionnee;
            set
            {
                if (_entretienMoteurSelectionnee != value)
                {
                    _entretienMoteurSelectionnee = value;
                    RaisePropertyChanged();
                }
            }
        }

        private EntretienViewModel _entretienHuileSelectionnee;
        public EntretienViewModel EntretienHuileSelectionnee
        {
            get => _entretienHuileSelectionnee;
            set
            {
                if (_entretienHuileSelectionnee != value)
                {
                    _entretienHuileSelectionnee = value;
                    RaisePropertyChanged(nameof(EntretienHuileSelectionnee));
                }
            }
        }

        private EntretienViewModel _entretienTransmissionSelectionnee;
        public EntretienViewModel EntretienTransmissionSelectionnee
        {
            get => _entretienTransmissionSelectionnee;
            set
            {
                if (_entretienTransmissionSelectionnee != value)
                {
                    _entretienTransmissionSelectionnee = value;
                    RaisePropertyChanged();
                }
            }
        }

        private EntretienViewModel _entretienPneusSelectionnee;
        public EntretienViewModel EntretienPneusSelectionnee
        {
            get => _entretienPneusSelectionnee;
            set
            {
                if (_entretienPneusSelectionnee != value)
                {
                    _entretienPneusSelectionnee = value;
                    RaisePropertyChanged();
                }
            }
        }


        private VehiculeTerrestreViewModel _vehiculeSelectionnee;
        public VehiculeTerrestreViewModel VehiculeSelectionnee
        {
            get => _vehiculeSelectionnee;
            set
            {
                _vehiculeSelectionnee = value;
                RaisePropertyChanged(nameof(VehiculeSelectionnee));
                EntretienMoteurSelectionnee = null;
                EntretienHuileSelectionnee = null;
                EntretienTransmissionSelectionnee = null;
                EntretienPneusSelectionnee = null;
                RefreshListesEntretiens();
            }
        }

        private ObservableCollection<EntretienViewModel> _listeEntretiensMoteur;
        public ObservableCollection<EntretienViewModel> ListeEntretiensMoteur
        {
            get => _listeEntretiensMoteur;
            set
            {
                if (_listeEntretiensMoteur != value)
                {
                    _listeEntretiensMoteur = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<EntretienViewModel> _listeEntretiensHuile;
        public ObservableCollection<EntretienViewModel> ListeEntretiensHuile
        {
            get => _listeEntretiensHuile;
            set
            {
                if (_listeEntretiensHuile != value)
                {
                    _listeEntretiensHuile = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<EntretienViewModel> _listeEntretiensTransmission;
        public ObservableCollection<EntretienViewModel> ListeEntretiensTransmission
        {
            get => _listeEntretiensTransmission;
            set
            {
                if (_listeEntretiensTransmission != value)
                {
                    _listeEntretiensTransmission = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<EntretienViewModel> _listeEntretiensPneus;
        public ObservableCollection<EntretienViewModel> ListeEntretiensPneus
        {
            get => _listeEntretiensPneus;
            set
            {
                if (_listeEntretiensPneus != value)
                {
                    _listeEntretiensPneus = value;
                    RaisePropertyChanged();
                }
            }
        }


        public MainViewModelHistoriqueEntretiens(IEntretiensDataProvider entretiensDataProvider, IVehiculeTerrestreDataProvider vehiculeTerrestreDataProvider)
        {
            _entretienDataProvider = entretiensDataProvider;
            _vehiculeTerrestreDataProvider = vehiculeTerrestreDataProvider;


            EntretiensMoteur = new ObservableCollection<EntretienViewModel>();
            EntretiensHuile = new ObservableCollection<EntretienViewModel>();
            EntretiensTransmission = new ObservableCollection<EntretienViewModel>();
            EntretiensPneus = new ObservableCollection<EntretienViewModel>();

            ListeEntretiensMoteur = new ObservableCollection<EntretienViewModel>();
            ListeEntretiensHuile = new ObservableCollection<EntretienViewModel>();
            ListeEntretiensTransmission = new ObservableCollection<EntretienViewModel>();
            ListeEntretiensPneus = new ObservableCollection<EntretienViewModel>();


            ListeVehicules = new ObservableCollection<VehiculeTerrestreViewModel>();
            ListeVinVehicules = new ObservableCollection<string>();


        }

        public void RefreshListesEntretiens(int? typeEntretien = null)
        {
            switch (typeEntretien)
            {
                case 1:
                    ListeEntretiensMoteur.Clear();
                    foreach (EntretienViewModel entretien in EntretiensMoteur
                        .Where(e => e.Vin == VehiculeSelectionnee.Vin)
                        .OrderByDescending(e => DateTimeOffset.Parse(e.Date)))
                        ListeEntretiensMoteur.Add(entretien);
                    RaisePropertyChanged(nameof(ListeEntretiensMoteur));
                    break;
                case 2:
                    ListeEntretiensHuile.Clear();
                    foreach (EntretienViewModel entretien in EntretiensHuile
                        .Where(e => e.Vin == VehiculeSelectionnee.Vin)
                        .OrderByDescending(e => DateTimeOffset.Parse(e.Date)))
                        ListeEntretiensHuile.Add(entretien);
                    RaisePropertyChanged(nameof(ListeEntretiensHuile));
                    break;
                case 3:
                    ListeEntretiensTransmission.Clear();
                    foreach (EntretienViewModel entretien in EntretiensTransmission
                        .Where(e => e.Vin == VehiculeSelectionnee.Vin)
                        .OrderByDescending(e => DateTimeOffset.Parse(e.Date)))
                        ListeEntretiensTransmission.Add(entretien);
                    RaisePropertyChanged(nameof(ListeEntretiensTransmission));
                    break;
                case 4:
                    ListeEntretiensPneus.Clear();
                    foreach (EntretienViewModel entretien in EntretiensPneus
                        .Where(e => e.Vin == VehiculeSelectionnee.Vin)
                        .OrderByDescending(e => DateTimeOffset.Parse(e.Date)))
                        ListeEntretiensPneus.Add(entretien);
                    RaisePropertyChanged(nameof(ListeEntretiensPneus));
                    break;
                default:
                    RefreshListesEntretiens(1);
                    RefreshListesEntretiens(2);
                    RefreshListesEntretiens(3);
                    RefreshListesEntretiens(4);
                    break;
            }
        }



        private async Task ChargerEntretiensAsync(int typeEntretien, ObservableCollection<EntretienViewModel> listeEntretiens)
        {
            listeEntretiens.Clear();
            List<Entretiens> entretiensData = await Task.Run(() => _entretienDataProvider.GetEntretiens(typeEntretien));
            entretiensData.ForEach(entretien => listeEntretiens.Add(new EntretienViewModel(entretien)));
        }

        public Task ChargerEntretiensMoteurAsync() => ChargerEntretiensAsync(1, EntretiensMoteur);
        public Task ChargerEntretiensHuileAsync() => ChargerEntretiensAsync(2, EntretiensHuile);
        public Task ChargerEntretiensTransmissionAsync() => ChargerEntretiensAsync(3, EntretiensTransmission);
        public Task ChargerEntretiensPneusAsync() => ChargerEntretiensAsync(4, EntretiensPneus);


        public void ChargerVehiculeTerrestres()
        {
            ListeVehicules.Clear();
            ListeVinVehicules.Clear();
            List<VehiculeTerrestre> vTerrestreData = _vehiculeTerrestreDataProvider.GetVehiculesTerrestres();
            foreach (VehiculeTerrestre vTerrestre in vTerrestreData)
            {
                VehiculeTerrestreViewModel vehiculeViewModel = new VehiculeTerrestreViewModel(vTerrestre);
                ListeVehicules.Add(vehiculeViewModel);
                ListeVinVehicules.Add(vehiculeViewModel.Vin);
                VehiculeSelectionnee = vehiculeViewModel;
                RaisePropertyChanged(nameof(VehiculeSelectionnee));
            }
        }

        public async Task ChargerDonneesAsync()
        {
            ChargerVehiculeTerrestres();
            await Task.WhenAll(
                   ChargerEntretiensMoteurAsync(),
                   ChargerEntretiensHuileAsync(),
                   ChargerEntretiensTransmissionAsync(),
                   ChargerEntretiensPneusAsync()
               );
            RefreshListesEntretiens();

        }

        public void SetVehiculeSelectionnee(VehiculeTerrestreViewModel vehiculeSelectionnee)
        {
            VehiculeSelectionnee = vehiculeSelectionnee;
            RaisePropertyChanged(nameof(VehiculeSelectionnee));
            RaisePropertyChanged(nameof(RefreshListesEntretiens));
        }

        public DateTimeOffset ObtenirDateActuelle() => DateTimeOffset.Now;


        public void AjouterEntretien(EntretienViewModel entretienViewModel)
        {
            _vehiculeTerrestreDataProvider.ModifierVehiculeTerrestre(VehiculeSelectionnee.VehiculeTerrestre);
            _entretienDataProvider.AjouterEntretien(entretienViewModel.Entretien);
            VehiculeSelectionnee.NouvelEntretienEtatVehicule(entretienViewModel.TypeEntretien);
            switch (entretienViewModel.TypeEntretien)
            {

                case 1:
                    EntretiensMoteur.Add(entretienViewModel);
                    RefreshListesEntretiens(1);
                    break;
                case 2:
                    EntretiensHuile.Add(entretienViewModel);
                    RefreshListesEntretiens(2);
                    break;
                case 3:
                    EntretiensTransmission.Add(entretienViewModel);
                    RefreshListesEntretiens(3);
                    break;
                case 4:
                    EntretiensPneus.Add(entretienViewModel);
                    RefreshListesEntretiens(4);
                    break;
            }
            RaisePropertyChanged(nameof(VehiculeSelectionnee));
        }

        public void SupprimerEntretien(EntretienViewModel entretienViewModel)
        {
            _entretienDataProvider.RetirerEntretien(entretienViewModel.Entretien);
            RaisePropertyChanged(nameof(RefreshListesEntretiens));
            switch (entretienViewModel.TypeEntretien)
            {
                case 1:
                    EntretiensMoteur.Remove(entretienViewModel);
                    RefreshListesEntretiens(1);
                    break;
                case 2:
                    EntretiensHuile.Remove(entretienViewModel);
                    RefreshListesEntretiens(2);
                    break;
                case 3:
                    EntretiensTransmission.Remove(entretienViewModel);
                    RefreshListesEntretiens(3);
                    break;
                case 4:
                    EntretiensPneus.Remove(entretienViewModel);
                    RefreshListesEntretiens(4);
                    break;
                default:
                    break;
            }
        }
    }
}
