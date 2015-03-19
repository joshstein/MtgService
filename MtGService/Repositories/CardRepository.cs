using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Melek.Models;
using MtGService.Models;
using MtGService.Repositories;

namespace MtGService.Services
{
    public class CardRepository
    {
        public Card GetCard(string name)
        {
            SingletonRepository repo = new SingletonRepository();

            return repo.MelekDataStore.Search(name).FirstOrDefault();
        }
    }
}