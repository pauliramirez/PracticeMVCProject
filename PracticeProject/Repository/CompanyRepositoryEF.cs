using PracticeProject.Data;
using PracticeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeProject.Repository
{
    public class CompanyRepositoryEF : ICompanyRepository
    {
        // Constructor dependency injection for AppDbContext
        private readonly AppDbContext _db;

        public CompanyRepositoryEF(AppDbContext db)
        {
            _db = db;
        }
        public Company Add(Company company)
        {
            _db.Companies.Add(company);
            _db.SaveChanges();
            return company;
        }

        public Company Find(int id)
        {
            //return _db.Companies.Find(id);
            return _db.Companies.FirstOrDefault(u => u.CompanyId == id); // Returns either the 1st value found or a null
                                                                         // (the default value)
        }

        public List<Company> GetAll()
        {
            return _db.Companies.ToList();
        }

        public void Remove(int id)
        {
            var company = _db.Companies.FirstOrDefault(u => u.CompanyId == id);
            _db.Remove(company);
            _db.SaveChanges();
            return;
        }

        public Company Update(Company company)
        {
            _db.Companies.Update(company);
            _db.SaveChanges();
            return company;
        }
    }
}
