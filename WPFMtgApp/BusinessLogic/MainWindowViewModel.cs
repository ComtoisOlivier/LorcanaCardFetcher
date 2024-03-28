using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WPFMtgApp.BusinessLogic
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ICardFetcherService _cardService;
        public ICommand SearchCommand { get; }

        private ObservableCollection<BitmapImage> _images = [];
        public ObservableCollection<BitmapImage> Images 
        { 
            get => _images; 
            set => SetProperty(ref _images, value); 
        }

        private bool _initialized;
        public bool Initialized
        {
            get { return _initialized; }
            set => SetProperty(ref _initialized, value);
        }

        private string _searchText = "";
        public string SearchText 
        { 
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                ((RelayCommand)SearchCommand).RaiseCanExecuteChanged();
            }
        }

        public MainWindowViewModel(ICardFetcherService cardService)
        {
            _cardService = cardService;
            Initialized = _cardService.IsInitialized;
            _cardService.Initialized += CardService_Initialized;
            SearchCommand = new RelayCommand(async () => await SearchCard(), CanExecuteSeachCards);
        }

        private bool CanExecuteSeachCards()
        {
            return !string.IsNullOrEmpty(SearchText);
        }

        private void CardService_Initialized(object? sender, bool e)
        {
            Initialized = true;
        }

        private async Task SearchCard() 
        {
            var images = await _cardService.GetCardImages(SearchText);
            Images = [];
            foreach (var image in images)
            {
                AddImage(image);
            }
        }

        private void AddImage(BitmapImage image)
        {
            Images.Add(image);
            OnPropertyChanged(nameof(Images));
        }
    }
}
