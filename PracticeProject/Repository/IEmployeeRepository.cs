using PracticeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeProject.Repository
{
    public interface IEmployeeRepository
    {
        // C.reate
        Employee Add(Employee employee);

        // R.ead
        Employee Find(int id);
        List<Employee> GetAll();

        // U.pdate
        Employee Update(Employee employee);

        // D.elete
        void Remove(int id);
    }
}
