using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;
using Bangazon.Models.OrderViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Bangazon.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //// GET: Orders
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Order.Include(o => o.PaymentType).Include(o => o.User);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        // GET: Orders/Details/5
        public async Task<IActionResult> Details()
        {
            //Get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //Get the current user's order
            var currentOrder = GetCurrentOrder(user.Id);
            if (currentOrder != null)
            {

                //Get the products associated with this order
                var products = await _context.Product
                                                .Include(p => p.OrderProducts)
                                                .Where(p =>
                                                       p.OrderProducts.Any(op => op.OrderId == currentOrder.OrderId)
                                                    )
                                                .ToListAsync();
                //Instantiate a new OrderDetailViewModel and set the Order to the currentOrder
                var orderDetails = new OrderDetailViewModel()
                {
                    Order = currentOrder
                };

                //Loop over the products and create a new line item for each product and set the LineItem's product to the current product
                int id = 0;
                foreach (Product product in products)
                {
                    var orderProducts = await _context.OrderProduct.Where(op => op.ProductId == product.ProductId && op.OrderId == currentOrder.OrderId).ToListAsync();
                    if (id == product.ProductId)
                    {
                        orderDetails.LineItems[id].Units = orderProducts.Count;
                    }
                    else
                    {
                        id = product.ProductId;
                        orderDetails.LineItems.Add(
                            new OrderLineItem()
                            {
                                Product = product,
                                Units = orderProducts.Count
                            });
                    }
                }
                //Get the payment types for the user
                orderDetails.PaymentTypes = await _context.PaymentType.Where(pt => pt.UserId == user.Id).ToListAsync();
                
                return View(orderDetails);
            }
            else
            {
                return View();
            }
        }

        // POST: Orders/AddToOrder
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToOrder(int ProductId, int Quantity)
        {
            //Get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //Check if Order exists for the current user
            var currentOrder = GetCurrentOrder(user.Id);

            //If yes, add the product to OrderProducts for the Quantity amount of times
            if (currentOrder != null)
            {
                for (int i = 0; i < Quantity; i++)
                {
                    OrderProduct orderProduct = new OrderProduct()
                    {
                        OrderId = currentOrder.OrderId,
                        ProductId = ProductId
                    };
                    _context.Update(orderProduct);
                }
                await _context.SaveChangesAsync();
            }
            //Otherwise, create a new order and add the products to OrderProducts for the Quantity amount of times
            else
            {
                Order newOrder = new Order()
                {
                    DateCreated = DateTime.Now,
                    UserId = user.Id,
                    User = user
                };
                _context.Update(newOrder);
                await _context.SaveChangesAsync();
                var newOrderFromDB = GetCurrentOrder(user.Id);
                for (int i = 0; i < Quantity; i++)
                {
                    OrderProduct orderProduct = new OrderProduct()
                    {
                        OrderId = newOrderFromDB.OrderId,
                        ProductId = ProductId
                    };
                    _context.Update(orderProduct);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details");
        }

        //POST: Orders/PlaceOrder
        //Process an order for a user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(int OrderId)
        {
            var currentOrder = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == OrderId);
            currentOrder.DateCompleted = DateTime.Now;
            _context.Update(currentOrder);
            await _context.SaveChangesAsync();

            return View();
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,DateCreated,DateCompleted,UserId,PaymentTypeId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber", order.PaymentTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.PaymentType)
                .Include(o => o.OrderProducts)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productsToDelete = await _context.OrderProduct.Where(op => op.OrderId == id).ToListAsync();
            foreach (var p in productsToDelete){
                _context.OrderProduct.Remove(p);
            }
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFromCart(string title, int ProductId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var currentOrder = GetCurrentOrder(user.Id);

            var itemsToDelete = await _context.OrderProduct.Where(op => op.ProductId == ProductId && op.OrderId == currentOrder.OrderId).ToListAsync();
            foreach (var item in itemsToDelete)
            {
                _context.OrderProduct.Remove(item);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Details");
        }
        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
        private Order GetCurrentOrder(string userId)
        {
            var currentOrder = _context.Order.Where(o => o.UserId == userId && o.DateCompleted == null).ToList();
            if (currentOrder.Count > 0)
            {
                return (currentOrder[0]);
            }
            return null;
        }
    }
}
