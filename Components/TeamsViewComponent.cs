using Microsoft.AspNetCore.Mvc;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        public BowlingDbContext _context { get; set; }

        //Load up the DB to access in this models view file
        public TeamsViewComponent(BowlingDbContext temp)
        {
            _context = temp;
        }

        //Invoking to return a record of team DB (bowlers), using SQL syntax: 
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeam = RouteData?.Values["teamName"];

            var record = _context.Bowlers
                .Select(i => i.Team.TeamName)
                .Distinct()
                .OrderBy(i => i);

            return View(record);
        }
    }
}

