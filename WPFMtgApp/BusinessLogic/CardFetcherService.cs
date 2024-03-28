using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Windows.Media.Imaging;

namespace WPFMtgApp.BusinessLogic
{
    public class CardFetcherService : ICardFetcherService
    {
        private Dictionary<string, Card> _cards = [];

        public event EventHandler<bool>? Initialized;

        public bool IsInitialized { get; private set; }

        private Task _initTask;

        public CardFetcherService()
        {
            _initTask = Task.Run(InitCards);
        }

        private async Task<List<Uri>> GetCardsPaths(string searchValue)
        {
            await _initTask;
            List<Uri> result = [];

            foreach (var (name, card) in _cards.Where(c => c.Key.Contains(searchValue)))
            {
                
                string imageUrl = card.Image ?? ""; // URL of the image to download
                string destinationPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\cache\\{card.Set_ID}_{card.Card_Num}.jpg"; // Path where the image will be saved

                if (!Directory.Exists("cache"))
                    Directory.CreateDirectory("cache");

                if (File.Exists(destinationPath))
                {
                    result.Add(new Uri(destinationPath, UriKind.Absolute));
                    continue;
                }

                using HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync(imageUrl);

                if (response.IsSuccessStatusCode)
                {
                    using (Stream imageStream = await response.Content.ReadAsStreamAsync())
                    {
                        using Bitmap bitmap = new(imageStream);
                        // Save the Bitmap to a file
                        bitmap.Save(destinationPath, ImageFormat.Jpeg);
                        result.Add(new Uri(destinationPath, UriKind.Absolute));
                    }

                    Console.WriteLine("Image downloaded successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to download image. Status code: {response.StatusCode}");
                }
            }
            return result;
        }

        private static List<BitmapImage> GetCards(List<Uri> imageLinks) 
        {
            var result = new List<BitmapImage>();
            foreach (var imageLink in imageLinks) 
            {
                result.Add(new BitmapImage(imageLink));
            }
            return result;
        }

        public async Task<IEnumerable<BitmapImage>> GetCardImages(string cardId)
        {
            var uris = await GetCardsPaths(cardId);

            return uris.Count == 0 ? ([]) : GetCards(uris);
        }

        public async Task InitCards()
        {
            try
            {
                using HttpClient client = new();
                client.BaseAddress = new Uri(" https://api.lorcana-api.com");
                HttpResponseMessage response = await client.GetAsync("cards/all");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Card[]? cards = JsonConvert.DeserializeObject<Card[]>(responseBody);

                    if (cards is not null)
                        _cards = cards.ToDictionary(c => c.Name, c => c);
                }
                else
                {
                    Console.WriteLine($"Failed to make request. Status code: {response.StatusCode}");
                }

                IsInitialized = true;
                Initialized?.Invoke(this, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
