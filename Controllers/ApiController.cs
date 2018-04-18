using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aftermarket_WebApp.Controllers
{
    public class AbelApiController : ApiController
    {

        Aftermarket_Entity ae = new Aftermarket_Entity();


        [HttpGet]
        public List<GetListOfMakes_Result> GetMakes()
        {
           return ae.GetListOfMakes().ToList();

        }

         [HttpGet]
        public List<PartsVehicleXReference> GetX()
        {
            return ae.PartsVehicleXReferences.ToList(); 

        }




    }
}
