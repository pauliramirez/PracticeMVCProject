using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Models;
using PracticeProject.Repository;

namespace PracticeProject.Controllers
{
    public class EmployeesController : Controller
    {
        // Constructor dependency injection for ICompanyRepository and IEmployeeRepository
        private readonly ICompanyRepository _compRepo;
        private readonly IEmployeeRepository _empRepo;

        // Bind Employee Model so that Employee property is targeted
        [BindProperty]
        public Employee Employee { get; set; }

        public EmployeesController(ICompanyRepository compRepo, IEmployeeRepository empRepo)
        {
            _compRepo = compRepo;
            _empRepo = empRepo;
        }

        //// GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(_empRepo.GetAll());
        }

        //// GET: Employees/Create
        public IActionResult Create()
        {
            // Creates IEnumerable collection of Company names
            IEnumerable<SelectListItem> companyList = _compRepo.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CompanyId.ToString()
            });

            // Displays company names on dropdown in UI
            ViewBag.CompanyList = companyList;
            return View();
        }

        //// POST: Employees/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePOST()
        {
            if (ModelState.IsValid)
            {
                _empRepo.Add(Employee); // Employee does not have to be passed as a parameter to this method because it is binded
               
                return RedirectToAction(nameof(Index));
            }

            return View(Employee);
        }

        //// GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = _empRepo.Find(id.GetValueOrDefault()); // Prevents an error from being thrown 
                                                              // if no ID is specified (null)
            // Creates IEnumerable collection of Company names
            IEnumerable<SelectListItem> companyList = _compRepo.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CompanyId.ToString()
            });

            // Displays company names on dropdown in UI
            ViewBag.CompanyList = companyList;
            return View();

            if (Employee == null)
            {
                return NotFound();
            }

            return View(Employee);
        }

        //// POST: Employees/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != Employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _empRepo.Update(Employee);

                return RedirectToAction(nameof(Index));
            }

            return View(Employee);
        }

        //// GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _empRepo.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index)); // Redirects to Index action
        }
    }
}
