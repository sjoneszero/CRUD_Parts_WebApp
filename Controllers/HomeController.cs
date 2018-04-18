using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aftermarket_WebApp.Controllers
{
    public class HomeController : Controller
    {

        Aftermarket_Entity ae = new Aftermarket_Entity(); 
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPartManu()
        {
            var manuData = ae.PartsManufacturers.Select(x => new { x.PartManufacturerId, x.PartManufacturerDescription }); 
            return Json(manuData, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetPartNumber(int partmanu)
        {
            var partData = ae.PartsMasters.Where(x => x.PartManufacturerId == partmanu).Select(x => new {x.PartMasterId, x.PartNumber});
            return Json(partData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMake()
        {
            var makeData = ae.GetListOfMakes(); 
            return Json(makeData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetModel(int makeid)
        {
            var modelData = ae.GetListOfModelNames(makeid).OrderBy(x => x.ModelName);
            return Json(modelData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult VehicleList()
        {
            ViewBag.Saved = 0;

            return View("_VehicleListPartial");

        }

        [HttpPost]
        public ActionResult VehicleList(int partnumber = 0, int model = 0)
        {
            ViewBag.Saved = 0;

            ViewBag.PartNumber = ae.PartsMasters.Where(x => x.PartMasterId == partnumber).Select(x => x.PartNumber).First().ToString();
            ViewBag.PartMasterId = partnumber; 
            ViewBag.PartManu = (from t in ae.PartsTypes
                               join m in ae.PartsMasters on t.PartTypeId equals m.PartTypeId
                               where m.PartMasterId.Equals(partnumber)
                               select t.PartTypeDescription).First().ToString(); 


            var list = ae.GetListOfSubModels(model, partnumber, 0);


            return View("_VehicleListPartial", list.ToList());
        }


        [HttpPost]
        public ActionResult WritetoXref(List<GetListOfSubModels_Result> toassign, int partid)
        {
                       
            foreach (GetListOfSubModels_Result g in toassign)

            {
                int modelId = g.ModelId;
                bool assigned = g.Assigned; 

                ae.UpdateXRef(modelId, partid, assigned); 
               
            }

            ViewBag.Saved = 1; 

            return View("_CommitSaved");
        }





    }
}