using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCards.Controllers
{
    public class BaseCardController : ControllerBase
    {
        public object Status(dynamic dataItem)
        {
            if (dataItem is Boolean)
            {
                if (Convert.ToBoolean(dataItem))
                    Response.StatusCode = 200;
                else
                    Response.StatusCode = 304;
            }
            else
            {
                if (dataItem == null)
                {
                    Response.StatusCode = 400;
                    return new EmptyResult();
                }
            }
            return dataItem;
        }
    }
}
