using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBApi;
using CBApi.Models;
using CBApi.Models.Responses;
using CBApi.Models.Service;

namespace FastClickJobs.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CB()
        {
            var svc = API.GetInstance("WDHR4SN696HYPP3GYBH3");
            //Make a call to https://api.careerbuilder.com/v1/categories
            List<Category> codes = svc.GetCategories()
                                      .WhereCountryCode(CountryCode.US)
                                      .ListAll();

            //Make a call to https://api.careerbuilder.com/v1/employeetypes
            List<EmployeeType> emps = svc.GetEmployeeTypes()
                                         .WhereCountryCode(CountryCode.NL)
                                         .ListAll();
            //Search for Jobs
            var search = svc.JobSearch()
             .WhereKeywords("Sales")
             .WhereLocation("Atlanta,GA")
             .WhereCountryCode(CountryCode.US)
             .OrderBy(OrderByType.Title)
             .Ascending()
             .Search();
            var jobs = search.Results;
            foreach (JobSearchResult item in jobs)
            {
                Console.WriteLine(item.JobTitle);
            }
            return View(jobs);
        }
    }
}