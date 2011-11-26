using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ienablemuch.ToTheEfnhX;
using FlexigridCrudDemo.Models;

namespace FlexigridCrudDemo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/


        IRepository<Person> Persons { get; set; }

        public HomeController(IRepository<Person> persons)
        {
            Persons = persons;
        }


        public ViewResult Index()
        {
            return View();
        }

        /*
        public string Index()
        {
            // return Persons.All.Single(x => x.FavoriteNumber == 9).Firstname;

            var p = Persons.All.Single(x => x.FavoriteNumber == 9);

            return p.Firstname + " " + p.Country.CountryName;
            // return View();
        }*/

    }
}
