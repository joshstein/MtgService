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
    public class CardWebController : Controller
    {
        private CardRepository cardRepository;

        public CardWebController()
        {
            this.cardRepository = new CardRepository();
        }

        public JsonResult Get(string cardName)
        {
            //JsonSerializer serializer = new JsonSerializer();
            return Json(cardRepository.GetCard(cardName), JsonRequestBehavior.AllowGet);
        }
    }
}
