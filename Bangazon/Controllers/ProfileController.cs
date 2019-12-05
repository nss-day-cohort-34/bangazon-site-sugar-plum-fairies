using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bangazon.Controllers
{

    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        // GET: Products/Details/5
        public async Task<IActionResult> ProfileDetails()
        {
      
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            user.PaymentTypes =  _context.PaymentType
               .Where(p => p.UserId == user.Id).ToList();
            
            return View(user);
        } 
    }
} 