﻿using ebay.Data;
using ebay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ebay.Controllers{

    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var order = _db.OrderItems
                .Include(x=>x.Order)
                .ThenInclude(x=>x.Customer)
                .Include(x=>x.Product)
                .ThenInclude(x=>x.Category)
                .ToList();
            return Json(order.Select(x=>new 
            {
                Id= x.Order.CustomerId,
                // product = x.Product.Select(y=> new{
                //     id=y.id,
                //     name= y.Name,
                //     customer=y.Category
                // }),
                firstName = x.Order.Customer.FirstName,
                lastName =x.Order.Customer.LastName,
                productName=x.Product.Name,
                productPrice=x.Product.Price,
                productCategory=x.Product.Category.Name
                
            }));
        }

        // var sales = _context.Sales
        //     .Include(x => x.Details)
        //     .ThenInclude(x => x.Product)
        //     .ToList();

        // return Json(sales.Select(x => new
        // {
        //     Id = x.Id,
        //     CustomerName = x.CustomerName,
        //     Details = x.Details.Select(y => new
        //     {
        //         Id = y.Id,
        //         ProductName = y.Product.Name,
        //         NetAmount = y.NetAmount
        //     })
        // }));
    }
}