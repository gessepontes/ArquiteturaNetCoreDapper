﻿using AspnetCore.EFCore_Dapper.Domain.Entities;
using AspnetCore.EFCore_Dapper.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCore.EFCore_Dapper.MVC.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorEFRepository _autorEFRepository;

        public AutorController(IAutorEFRepository autorEFRepository) =>
            _autorEFRepository = autorEFRepository;

        // GET: Autor
        public IActionResult Index() =>
            View(_autorEFRepository.GetAll());

        // GET: Autor/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var autor = _autorEFRepository.GetById(id);
            if (autor == null)
                return NotFound();

            return View(autor);
        }

        // GET: Autor/Create
        public IActionResult Create() =>
            View();

        // POST: Autor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Nome")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                _autorEFRepository.Add(autor);
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autor/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var autor = _autorEFRepository.GetById(id);
            if (autor == null)
                return NotFound();

            return View(autor);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Nome")] Autor autor)
        {
            if (id != autor.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _autorEFRepository.Update(autor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.ID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autor/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var autor = _autorEFRepository.GetById(id);
            if (autor == null)
                return NotFound();

            return View(autor);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var autor = _autorEFRepository.GetById(id);
            _autorEFRepository.Remove(autor);
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id) =>
            _autorEFRepository.GetById(id) != null;
    }
}
