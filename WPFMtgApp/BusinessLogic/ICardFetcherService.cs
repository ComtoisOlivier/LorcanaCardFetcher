using System.Windows.Media.Imaging;

namespace WPFMtgApp.BusinessLogic
{
    public interface ICardFetcherService
    {
        Task InitCards();
        Task<IEnumerable<BitmapImage>> GetCardImages(string cardId);
        bool IsInitialized { get;}
        event EventHandler<bool> Initialized;
    }
}