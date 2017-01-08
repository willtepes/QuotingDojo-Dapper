using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using quotingDojo2.Models;
using quotingDojo2.Factory;
using Microsoft.AspNetCore.Http;
using quotingDojo2.Controllers;



namespace quotingDojo2
{
    public class QuoteController : Controller
    {
        private readonly QuoteFactory quoteFactory;
        public QuoteController(QuoteFactory quote)
        {
            //Instantiate a UserFactory object that is immutable (READONLY)
            //This is establish the initial DB connection for us.
            quoteFactory = quote;
        }
        
        [HttpGet]
        [Route("addquote")]
        public IActionResult Addquote()
        {
            if(HttpContext.Session.GetInt32("id")!=null){
               ViewBag.Errors = "";
               return View(); 
            }
            else{
                ViewBag.Errors = ModelState.Values;
                return View("index");
            }
            
        }
        [HttpGet]
        [Route("quotes")]
        public IActionResult Quotes()
        {
            if(HttpContext.Session.GetInt32("id")!=null)
            {
                ViewBag.Quotes = quoteFactory.QuotesWithUser();
                ViewBag.UserId = (int)HttpContext.Session.GetInt32("id");
                return View("Quotes");
            }
            else{
                ViewBag.Errors = ModelState.Values;
                return View("index");
            }
        }
        [HttpPost]
        [Route("quotes")]
        public IActionResult AddQuotes(Quote newquote)
        {
            if(ModelState.IsValid)
            {
            long user = (long)HttpContext.Session.GetInt32("id");
            quoteFactory.Add(newquote, user);
            return RedirectToAction("Quotes");
            }
            
            ViewBag.Errors = ModelState.Values;
            return View("addquote");
            
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.Errors = ModelState.Values;
            return View("index", "LoginController");
        }
        [HttpGet]
        [Route("delete/{parameter}")]
        public IActionResult Delete(long parameter)
        {
            System.Console.WriteLine("This is the parameter" + parameter);
            quoteFactory.delete(parameter);
            return RedirectToAction("Quotes");
        }
    }
}