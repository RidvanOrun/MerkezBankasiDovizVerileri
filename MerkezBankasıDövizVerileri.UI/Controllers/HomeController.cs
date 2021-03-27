using MerkezBankasıDövizVerileri.Data.Context;
using MerkezBankasıDövizVerileri.Data.Entitiy;
using MerkezBankasıDövizVerileri.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace MerkezBankasıDövizVerileri.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger,
                               ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
            _logger = logger;
        }

        public IActionResult Index(Entities entities)
        {
            var currencyInf = "http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(currencyInf);
            entities.CurrencyDate = Convert.ToString(xmlDoc.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);
            entities.USD = xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='0']/BanknoteSelling").InnerText;
            entities.AUD = xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='1']/BanknoteSelling").InnerText;
            entities.EUR = xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='9']/BanknoteSelling").InnerText;
            entities.GBP = xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='10']/BanknoteSelling").InnerText;
            _db.Add(entities);
            _db.SaveChanges();

            return View(entities);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
