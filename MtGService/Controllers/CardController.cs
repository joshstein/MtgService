using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Melek.Models;
using MtGService.Models;
using MtGService.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MtGService.Controllers
{
    public class CardController : ApiController
    {
        private CardRepository cardRepository;

        public CardController()
        {
            this.cardRepository = new CardRepository();
        }

        public Card Get(string name)
        {
            return cardRepository.GetCard(name);
        }
    }
}