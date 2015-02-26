using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private NdtvViewModel _ndtvModel;
       private TimesOfIndiaViewModel _timesOfIndiaModel;
       private IndiaTodayViewModel _indiaTodayModel;
       private SifySportsViewModel _sifySportsModel;
       private TheHinduViewModel _theHinduModel;
       private IbnLiveViewModel _ibnLiveModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = NdtvModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public NdtvViewModel NdtvModel
        {
            get { return _ndtvModel ?? (_ndtvModel = new NdtvViewModel()); }
        }
 
        public TimesOfIndiaViewModel TimesOfIndiaModel
        {
            get { return _timesOfIndiaModel ?? (_timesOfIndiaModel = new TimesOfIndiaViewModel()); }
        }
 
        public IndiaTodayViewModel IndiaTodayModel
        {
            get { return _indiaTodayModel ?? (_indiaTodayModel = new IndiaTodayViewModel()); }
        }
 
        public SifySportsViewModel SifySportsModel
        {
            get { return _sifySportsModel ?? (_sifySportsModel = new SifySportsViewModel()); }
        }
 
        public TheHinduViewModel TheHinduModel
        {
            get { return _theHinduModel ?? (_theHinduModel = new TheHinduViewModel()); }
        }
 
        public IbnLiveViewModel IbnLiveModel
        {
            get { return _ibnLiveModel ?? (_ibnLiveModel = new IbnLiveViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            NdtvModel.ViewType = viewType;
            TimesOfIndiaModel.ViewType = viewType;
            IndiaTodayModel.ViewType = viewType;
            SifySportsModel.ViewType = viewType;
            TheHinduModel.ViewType = viewType;
            IbnLiveModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadData(bool isNetworkAvailable)
        {
            var loadTasks = new Task[]
            { 
                NdtvModel.LoadItems(isNetworkAvailable),
                TimesOfIndiaModel.LoadItems(isNetworkAvailable),
                IndiaTodayModel.LoadItems(isNetworkAvailable),
                SifySportsModel.LoadItems(isNetworkAvailable),
                TheHinduModel.LoadItems(isNetworkAvailable),
                IbnLiveModel.LoadItems(isNetworkAvailable),
            };
            await Task.WhenAll(loadTasks);
        }

        /// <summary>
        /// Refresh ViewModel items asynchronous
        /// </summary>
        public async Task RefreshData(bool isNetworkAvailable)
        {
            var refreshTasks = new Task[]
            { 
                NdtvModel.RefreshItems(isNetworkAvailable),
                TimesOfIndiaModel.RefreshItems(isNetworkAvailable),
                IndiaTodayModel.RefreshItems(isNetworkAvailable),
                SifySportsModel.RefreshItems(isNetworkAvailable),
                TheHinduModel.RefreshItems(isNetworkAvailable),
                IbnLiveModel.RefreshItems(isNetworkAvailable),
            };
            await Task.WhenAll(refreshTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await RefreshData(NetworkInterface.GetIsNetworkAvailable());
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
