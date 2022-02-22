using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop2022.Data;
using OnlineShop2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OnlineShop2022.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShop2022.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Manager")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        private IWebHostEnvironment _webHostEnvironment;
        private Images _images;

        public ProductController(AppDbContext db, IWebHostEnvironment webHostEnvironment, Images images)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
            _images = images;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _db.Products.ToListAsync();
            return View(products);
        }


        public IActionResult Create()
        {
            var vm = new ProductViewModel()
            {
                Categories = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Product = new ProductModel()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel vm)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    var file = Request.Form.Files[0];

                    if (file != null)
                    {
                        vm.Product.ImagePath = _images.Upload(file, $"/images/products/");

                        await _db.Products.AddAsync(vm.Product);
                        await _db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {

                    vm.Categories = _db.Categories.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                    return View(vm);
                }

            }

            vm.Categories = _db.Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductViewModel vm)
        {

            if (id != vm.Product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (Request.Form.Files.Count() > 0)
                    {
                        var file = Request.Form.Files[0];
                        _images.Delete(vm.Product.ImagePath);
                        vm.Product.ImagePath = _images.Upload(file, $"/images/products/");
                    }


                    _db.Products.Update(vm.Product);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return NotFound();
                }
            }

            vm.Categories = _db.Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(vm);
        }

        //GET
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductModel currentProduct = await _db.Products.FindAsync(id);

            if (currentProduct == null)
            {
                return NotFound();
            }

            var vm = new ProductViewModel()
            {
                Categories = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Product = currentProduct
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, ProductModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductModel productToDelete = await _db.Products.FindAsync(id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            _db.Products.Remove(productToDelete);
            await _db.SaveChangesAsync();

            _images.Delete(productToDelete.ImagePath);

            return RedirectToAction(nameof(Index));

        }



        //GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductModel currentProduct = await _db.Products.FindAsync(id);

            if (currentProduct == null)
            {
                return NotFound();
            }

            return View(currentProduct);
        }



    }
}
