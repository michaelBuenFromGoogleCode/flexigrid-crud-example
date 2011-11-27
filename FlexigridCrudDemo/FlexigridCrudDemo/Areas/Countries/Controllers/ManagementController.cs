using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ienablemuch.ToTheEfnhX;


using FlexigridCrudDemo.Models;
using Ienablemuch.JqueryAjaxComboBox;

namespace FlexigridCrudDemo.Areas.Countries.Controllers
{
    public class ManagementController : Controller
    {
        IRepository<Country> _country;


        public ManagementController(IRepository<Country> country)
        {
            _country = country;
        }

        [HttpPost]
        public JsonResult Lookup(LookupRequest lr)
        {
            lr.q_word = lr.q_word ?? "";

            
            var FilteredCountries = _country.All.Where( x =>
                        lr.q_word == ""
                        ||
                        x.CountryName.Contains(lr.q_word)
                    );


            var PagedFilter =
                FilteredCountries.OrderBy(x => x.CountryName).Skip((lr.page_num - 1) * lr.per_page).Take(lr.per_page)
                .ToList();

            return Json(
                    new
                    {
                        cnt = FilteredCountries.Count(),
                        primary_key = PagedFilter.Select(x => x.CountryId),
                        candidate = PagedFilter.Select(x => x.CountryName),
                        cnt_page = PagedFilter.Count()
                    }
                );

        }

        //
        // GET: /Countries/Management/

        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public string Caption(string q_word)
        {
            
            if (string.IsNullOrEmpty(q_word)) return "";

            Guid countryId;
            bool isOk = Guid.TryParse(q_word, out countryId);

            

            return
                isOk ?
                _country.All
                .Where(x => x.CountryId == countryId)
                .Select(x => x.CountryName)
                .SingleOrDefault() ?? ""
                : "";
            

        }

    }
}
