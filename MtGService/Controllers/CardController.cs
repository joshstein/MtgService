using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Melek.Models;
using MtGService.Models;
using MtGService.Repositories;
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

        public void Get(string text)
        {
            Card theCard = cardRepository.GetCard(text);

            SingletonRepository repo = new SingletonRepository();

            //return cardRepository.GetCard(text);
            var httpWebRequest = HttpWebRequest.Create(repo.SlackEndpoint);

            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                using (JsonWriter writer = new JsonTextWriter(streamWriter))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartObject();
                    writer.WritePropertyName("text");
                    writer.WriteValue(theCard.CardTypes.ToString() + ", " + theCard.Cost + ": " + theCard.Text);
                    writer.WritePropertyName("attachments");
                    writer.WriteStartArray();
                    writer.WriteStartObject();
                    writer.WritePropertyName("title_link");
                    writer.WriteValue("http://gatherer.wizards.com/Pages/Card/Details.aspx?multiverseid=244313");
                    writer.WritePropertyName("image_url");
                    writer.WriteValue("http://gatherer.wizards.com/Handles/Image.ashx?multiverseid=244313&type=card");
                    writer.WriteEndObject();
                    writer.WriteEnd();
                    writer.WriteEndObject();
                }
            }
            
            var request = httpWebRequest.Headers;

            var httpResponse = httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
            }
        }
    }
}