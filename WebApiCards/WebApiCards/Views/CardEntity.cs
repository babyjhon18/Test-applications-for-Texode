using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCards.Views
{
    public class CardEntity
    {
        private const string FileName = "cardsData.json";

        public Object GetCards(int cardId = 0)
        {
            return JsonDeserializeCards(cardId);
        }

        public bool CreateCard(int cardId, Card card) 
        {
            if (cardId != 0 && card.Id != 0 && cardId == card.Id)
                return UpdateCard(card);
            else
                return false;
        }

        private bool UpdateCard(Card card)
        {
            return false;
        }

        public bool DeleteCard(int cardId)
        {
            try
            {
                var cards = JsonDeserializeCards() as List<Card>;
                cards.Remove(cards.Where(c => c.Id == cardId).FirstOrDefault());
                JsonSerializeCards(cards);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private Object JsonDeserializeCards(int cardId = 0)
        {
            var jsonCardData = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + FileName);
            List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(jsonCardData.ToString());
            //foreach (var card in cards)
            //{
            //    var imageDataByteArray = Convert.FromBase64String(card.ImageSourceString);
            //    var imageDataStream = new MemoryStream(imageDataByteArray);
            //    imageDataStream.Position = 0;
            //    card.ImageSourceString = imageDataByteArray.ToString();
            //}
            if (cardId != 0)
                return cards.Where(c => c.Id == cardId).FirstOrDefault();
            return cards;
        }

        private void JsonSerializeCards(List<Card> Cards)
        {
            var serializedContent = JsonConvert.SerializeObject(Cards);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + FileName, serializedContent);
        }
    }
}
