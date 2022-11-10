using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiCards.Views;

namespace WebApiCards.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : BaseCardController
    {
        [HttpGet]
        public Object Get(int cardId = 0)
        {
            return Status(new CardEntity().GetCards(cardId));
        }

        [HttpPost]
        public void Post([FromBody] Card card, int cardId)
        {
            Status(new CardEntity().CreateCard(cardId, card));
        }

        [HttpDelete]
        public void Delete(int cardId)
        {
            Status(new CardEntity().DeleteCard(cardId));
        }
    }
}
