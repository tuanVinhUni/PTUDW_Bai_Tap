﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TH_Harmic.Models;

namespace TH_Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TbMenusController : Controller
    {
        private readonly TH_HarmicContext _context;

        public TbMenusController(TH_HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/TbMenus
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbMenus.ToListAsync());
        }

        // GET: Admin/TbMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMenu = await _context.TbMenus
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (tbMenu == null)
            {
                return NotFound();
            }

            return View(tbMenu);
        }

        // GET: Admin/TbMenus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TbMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,Title,Alias,Description,Levels,ParentId,Position,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] TbMenu tbMenu)
        {
            if (ModelState.IsValid)
            {
                tbMenu.Alias = TH_Harmic.Utilities.Function.TitleSlugGenerationAlias(tbMenu.Title);
                _context.Add(tbMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbMenu);
        }

        // GET: Admin/TbMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMenu = await _context.TbMenus.FindAsync(id);
            if (tbMenu == null)
            {
                return NotFound();
            }
            return View(tbMenu);
        }

        // POST: Admin/TbMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuId,Title,Alias,Description,Levels,ParentId,Position,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] TbMenu tbMenu)
        {
            if (id != tbMenu.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbMenuExists(tbMenu.MenuId))
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
            return View(tbMenu);
        }

        // GET: Admin/TbMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMenu = await _context.TbMenus
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (tbMenu == null)
            {
                return NotFound();
            }

            return View(tbMenu);
        }

        // POST: Admin/TbMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbMenu = await _context.TbMenus.FindAsync(id);
            if (tbMenu != null)
            {
                _context.TbMenus.Remove(tbMenu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbMenuExists(int id)
        {
            return _context.TbMenus.Any(e => e.MenuId == id);
        }
    }
}
