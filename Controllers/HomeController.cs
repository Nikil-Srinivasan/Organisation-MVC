﻿using Microsoft.AspNetCore.Mvc;
using Organization.Models;
using System.Diagnostics;

namespace Organization.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly OrganisationContext _context;


        public HomeController(ILogger<HomeController> logger, OrganisationContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            var products = _context.Product.ToList();

            int[] RevenueValues = new int[products.Count()];

            string[] Productnames = new string[products.Count()];

            for (int i = 0; i < products.Count(); i++)
            {
                RevenueValues[i] = products[i].ProductRevenue;
            }

            for (int i = 0; i < products.Count(); i++)
            {
                Productnames[i] = products[i].ProductName;
            }

            var viewModel = new DashboardViewModel
            {
                TotalCustomers = _context.Customer.Count(),
                TotalEmployees = _context.Employee.Count(),
                TotalDepartments = _context.Department.Count(),
                TotalProducts = _context.Product.Count(),
                TotalRevenues = RevenueValues,
                Products = Productnames,

                // Calculate total counts for other models and assign them to the corresponding properties
            };

            return View(viewModel);

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