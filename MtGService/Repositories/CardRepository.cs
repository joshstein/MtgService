using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Melek.Models;

namespace MtGService.Repositories
{
    public class CardRepository
    {
        public Card GetCard(string name)
        {
            SingletonRepository repo = new SingletonRepository();

            return repo.MelekDataStore.Search(name).FirstOrDefault();
        }

        public async Task<Uri> GetCardImageUri(Card card)
        {
            SingletonRepository repo = new SingletonRepository();
            return (await repo.MelekDataStore.GetCardImageUri(card.GetLastPrinting()));
        }
    }
}