using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ienablemuch.ToTheEfnhX;
using FlexigridCrudDemo.Models;
using FlexigridCrudDemo.ViewsModels;


using Ienablemuch.JsValid;

namespace FlexigridCrudDemo.Areas.People.Controllers
{
    public class ManagementController : Controller
    {
        //
        // GET: /People/Management/

        IRepository<Person> _person;        

        public ManagementController(IRepository<Person> person)
        {
            _person = person;            
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(FlexigridParam p)
        {            
            var json = new JsonResult();
            

            var filtered =
                from r in _person.All
                select r;

            


            json.Data = new
                {
                    page = p.page,
                    total = filtered.Count(),
                    rows = 
                        filtered
                        .OrderBy(x => x.Username)
                        .Skip((p.page - 1) * p.rp).Take(p.rp)
                        .ToList()
                        .Select(x =>
                            new 
                            {
                                id = x.PersonId.ToString(),
                                cell = new string[] { x.Username, x.Firstname, x.Lastname, x.FavoriteNumber.ToString(), x.Country.CountryName, Convert.ToBase64String(x.RowVersion) }
                            })
                };


            return json;
        }//List


        public JsonResult GetUpdated(Guid id)
        {
            var js = new JsonResult();

            js.Data =
                new
                {
                    Record = (from p in _person.All
                              where p.PersonId == id // this guarantees single
                              select new { 
                                  p.PersonId, 
                                  p.Username,
                                  p.Firstname, 
                                  p.Lastname,
                                  p.FavoriteNumber, 
                                  p.Country.CountryId,                                  
                                  p.RowVersion
                              } )    
                              .ToList()  
                              
                              .Select(x => 
                                  new { x.PersonId, x.Username, x.Firstname, x.Lastname, x.FavoriteNumber, 
                                      x.CountryId, 
                                      RowVersion = Convert.ToBase64String(x.RowVersion ?? new byte[] { }) })
                              .Single()                             
                };

            return js;
        }

        [HttpPost]
        public JsonResult SaveViaAjax(Person p)
        {
            // p.Country = _country.LoadStub(new Guid("C589CED9-0E0E-481F-8EFF-51D4E6FE18E0"));
            
            // ModelState.Remove("Country.CountryId");
            SaveCommon(p);
            

            var json = new JsonResult();
            json.Data =
                new
                {
                    ModelState = ModelState.ToJsonValidation(),
                    PersonId = p.PersonId,
                    RowVersion = Convert.ToBase64String(p.RowVersion ?? new byte[]{})
                };
            return json;
        }

        void SaveCommon(Person p)
        {
            

            try
            {

                ModelState.Remove("PersonId"); // so no validation would occur on primary key
                if (ModelState.IsValid)
                {
                    _person.Save(p, p.RowVersion);
                }

            }
            catch (Exception ex)
            {
                // ModelState.AddModelError("",ex.Message + "\n\n" + ex.InnerException.InnerException.Message);
                ModelState.AddModelError("", ex.Message );
                
            }
        }

        [HttpPost]
        public JsonResult Delete(Guid pk, byte[] version)
        {

            try
            {
                _person.Delete(pk, version);

                return Json(new { IsOk = true });
            }
            catch (Exception)
            {
                return Json(new { IsOk = false });
                
            }
        }


        public ViewResult Test()
        {
            return View();
        }
    }
}
