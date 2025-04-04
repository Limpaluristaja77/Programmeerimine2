using System.Collections.ObjectModel;
using System.Windows.Input;
using KooliProjekt.WpfApp.Api;

namespace KooliProjekt.WpfApp
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public ObservableCollection<Panel> Lists { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Predicate<Panel> ConfirmDelete { get; set; }

        private readonly IApiClient _apiClient;

        public MainWindowViewModel() : this(new ApiClient())
        {
        }

        public MainWindowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;

            Lists = new ObservableCollection<Panel>();

            NewCommand = new RelayCommand<Panel>(
                // Execute
                list =>
                {
                    SelectedItem = new Panel();
                }
            );

            SaveCommand = new RelayCommand<Panel>(
                // Execute
                async list =>
                {
                    await _apiClient.Save(SelectedItem);
                    await Load();
                },
                // CanExecute
                list =>
                {
                    return SelectedItem != null;
                }
            );

            DeleteCommand = new RelayCommand<Panel>(
                // Execute
                async list =>
                {
                    if(ConfirmDelete != null)
                    {
                        var result = ConfirmDelete(SelectedItem);
                        if(!result)
                        {
                            return;
                        }
                    }

                    await _apiClient.Delete(SelectedItem.Id);
                    Lists.Remove(SelectedItem);
                    SelectedItem = null;
                },
                // CanExecute
                list =>
                {
                    return SelectedItem != null;
                }
            );
        }

        public async Task Load()
        {
            Lists.Clear();

            var lists = await _apiClient.List();
            foreach(var list in lists)
            {
                Lists.Add(list);
            }
        }

        private Panel _selectedItem;
        public Panel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }
    }
}
