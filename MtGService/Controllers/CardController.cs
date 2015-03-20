using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Bazam.Modules;
using Melek.Models;
using MtGService.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MtGService.Controllers
{
    public class CardController : ApiController
    {
        private CardRepository cardRepository;
        private static readonly string _SlackEndpoint = "https://hooks.slack.com/services/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";

        public CardController()
        {
            this.cardRepository = new CardRepository();
        }

        public async void Get(string text)
        {
            Card theCard = cardRepository.GetCard(text);

            SingletonRepository repo = new SingletonRepository();

            //return cardRepository.GetCard(text);
            var httpWebRequest = HttpWebRequest.Create(_SlackEndpoint);

            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                using (JsonWriter writer = new JsonTextWriter(streamWriter))
                {
                    writer.Formatting = Formatting.Indented;

                    //format the types properly
                    StringBuilder typesBuilder = new StringBuilder();
                    foreach (CardType type in theCard.CardTypes)
                    {
                        typesBuilder.Append(StringBeast.Capitalize(type.ToString(), true));
                        typesBuilder.Append(" ");
                    }
                    string types = typesBuilder.ToString().TrimEnd();

                    writer.WriteStartObject();
                    writer.WritePropertyName("attachments");
                    writer.WriteStartArray();
                    writer.WriteStartObject();
                    writer.WritePropertyName("fallback");
                    writer.WriteValue(theCard.Name);
                    writer.WritePropertyName("color");
                    writer.WriteValue("#8C8C8C");
                    writer.WritePropertyName("title");
                    writer.WriteValue(theCard.Name);
                    writer.WritePropertyName("title_link");
                    writer.WriteValue("http://gatherer.wizards.com/Pages/Card/Details.aspx?multiverseid=" + theCard.GetLastPrinting().MultiverseID);
                    writer.WritePropertyName("image_url");
                    writer.WriteValue((await cardRepository.GetCardImageUri(theCard)).ToString());
                    writer.WritePropertyName("text");
                    writer.WriteValue(types + (theCard.Tribe.Length > 0 ? " - " + theCard.Tribe : " ") + ", " + theCard.Cost + ": " + theCard.Text.Replace("\\n", " "));
                    writer.WriteEndObject();
                    writer.WriteEndArray();
                    writer.WriteEndObject();
                }
            }

            var httpResponse = httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
            }
        }
    }
}