using System;
using System.Linq;
using System.Threading.Tasks;
using Melek.DataStore;
using Melek.Models;

namespace MtGService.Repositories
{
    public class CardRepository
    {
        public Card GetCard(string name)
        {
            SingletonRepository repo = new SingletonRepository();

            MelekDataStore store = repo.MelekDataStore;

            return store.Search(name).FirstOrDefault();
        }

        public async Task<Uri> GetCardImageUri(Card card)
        {
            SingletonRepository repo = new SingletonRepository();
            return (await repo.MelekDataStore.GetCardImageUri(card.GetLastPrinting()));
        }
    }
}