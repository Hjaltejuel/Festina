using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sommerhuse.Data;
using Sommerhuse.Models;

namespace Sommerhus.Controllers
{
    [Authorize]
    public class DatoesController : Controller
    {
        private readonly SommerhusContext _context;

        public DatoesController(SommerhusContext context)
        {
            _context = context;
        }

        // GET: Datoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dato.ToListAsync());
        }

        // GET: Datoes/Details/5
        public async Task<IActionResult> Details(int Id)
        {

            var dato = await _context.Dato
                .SingleOrDefaultAsync(m => m.Id == Id);
            if (dato == null)
            {
                return NotFound();
            }

            return View(dato);
        }


        public class Data
        {
            public DateTime Date { get; set; }
        }

        [HttpPost]
        public void SetTempData([FromBody]Data tempDataValue)
        {
            // Set your TempData key to the value passed in
            var formated = tempDataValue.Date.ToString("yyyy-MM-dd");
            TempData["Fra"] = formated;
        }
        public class Data2
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
        [HttpPost]
        public void SetTempData2([FromBody]Data2 tempDataValue)
        {
            // Set your TempData key to the value passed in
            var Fra = tempDataValue.Start.ToString("yyyy-MM-dd");
            var To = tempDataValue.End.ToString("yyyy-MM-dd");
            TempData["Fra"] = Fra;
            TempData["Til"] = To;
        }



        // GET: Datoes/Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        // POST: Datoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fra,Til,Title,Personer")] Dato dato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dato);
        }

        // GET: Datoes/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {


            var dato = await _context.Dato.SingleOrDefaultAsync(m => m.Id == Id);
            if (dato == null)
            {
                return NotFound();
            }
            return View(dato);
        }

        // POST: Datoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,Fra,Til,Title,Personer")] Dato dato)
        {
            if (Id != dato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatoExists(dato.Id))
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
            return View(dato);
        }

        // GET: Datoes/Delete/5
        public async Task<IActionResult> Delete(int Id)
        {


            var dato = await _context.Dato
                .SingleOrDefaultAsync(m => m.Id == Id);
            if (dato == null)
            {
                return NotFound();
            }

            return View(dato);
        }

        // POST: Datoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var dato = await _context.Dato.SingleOrDefaultAsync(m => m.Id == Id);
            _context.Dato.Remove(dato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatoExists(int Id)
        {
            return _context.Dato.Any(e => e.Id == Id);
        }
    }
}
