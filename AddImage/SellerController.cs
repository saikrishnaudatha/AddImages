using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AddingImages.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddingImages.Controllers
{
    public class SellerController : Controller
    {

        private readonly SellerContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;
        public SellerController(SellerContext context, IWebHostEnvironment hostingEnvironment)
        {
            this._context = context;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: Seller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SellerViewCreateModel sv)
        {
            // TODO: Add insert logic here

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (sv.PhotoPath != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + sv.PhotoPath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    sv.PhotoPath.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Seller newSeller = new Seller()
                {
                    Name = sv.Name,
                    Email = sv.Email,
                    Password = sv.Password,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    PhotoPath = uniqueFileName
                };

                _context.Add(newSeller);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = newSeller.Id });
            }

            return View();
        }


        public ActionResult Details(int id)
        {
            Seller seller=_context.sellerdata.FirstOrDefault(m => m.Id == id);
            if (seller == null)
            {
                ViewBag.message = "Not Valid Seller";
                return View();
            }

            return View(seller);

        
         
        }
    }

    }
