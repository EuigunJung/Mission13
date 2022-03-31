using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {

        private BowlingDbContext _context { get; set; }
       
        //Constructor 
        public HomeController(BowlingDbContext temp)
        {
            _context = temp;
        }

        //Index Page that displays the teamname and the bowlers information
        public IActionResult Index(string tName)
        {
            HttpContext.Session.Remove("id");
            ViewBag.TeamName = tName ?? "Home";

            var tList = _context.Bowlers
                .Include(i => i.Team)
                .Where(i => i.Team.TeamName == tName || tName == null)
                .ToList();
            return View(tList);
        }


     

        // CRUD - Creation Part 
        //Creating a form that adds bowler's information 
        [HttpGet]
        public IActionResult ResponseForm()
        {
            //Use ViewBag to load a list of teams
            ViewBag.Teams = _context.Teams.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult ResponseForm(Bowler b)
        {
            //We need to make sure each time response form is created, the ID increments by 1. 
            int num1 = 0;

            foreach (var i in _context.Bowlers)
            {
                if (num1 < i.BowlerID)
                {
                    num1 = i.BowlerID;
                }
            }
            
            b.BowlerID = num1 + 1; 

            //Validating the model
            if (ModelState.IsValid)
            {
                _context.Add(b);
                _context.SaveChanges();
                HttpContext.Session.Remove("id");
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Teams = _context.Teams.ToList();

                return View("ResponseForm", b);
            }
        }

        // CRUD - Edit Part 

        [HttpGet]
        public IActionResult Edit (int bID)
        {
            //Using ViewBag, get the bowler DB matching with the bowler ID:
            ViewBag.Teams = _context.Teams.ToList();
            HttpContext.Session.SetString("id", bID.ToString());
            var list = _context.Bowlers.Single(i => i.BowlerID == bID);

            return View("ResponseForm", list);

        }

        [HttpPost]
        public IActionResult Edit (Bowler b)
        {
            //Pass the bowler ID and assign as the updated DB for the bowler:
            string id = HttpContext.Session.GetString("id");
            int tId = int.Parse(id);
            b.BowlerID = tId;

            //Validating the Response Form  
            if (ModelState.IsValid)
            {
                _context.Update(b);
                _context.SaveChanges();
                HttpContext.Session.Remove("id");
                return RedirectToAction("Index");
            }

            else
            {
                ViewBag.Teams = _context.Teams.ToList();

                return View("ResponseForm", b);
            }
           
        }

        // CRUD - Delete Part 
        public IActionResult Remove(int bID)
        {
            //get the bowler id in the DB and remove from the list. Save changes and redirect to index to see the changes:
            var list = _context.Bowlers.Single(i => i.BowlerID == bID);
            _context.Bowlers.Remove(list);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }   

    }
}
