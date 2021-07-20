using PracticeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeProject.Repository
{
    public interface ICompanyRepository
    {
        // C.reate
        Company Add(Company company);

        // R.ead
        Company Find(int id);
        List<Company> GetAll();

        // U.pdate
        Company Update(Company company);

        // D.elete
        void Remove(int id);
    }
}
