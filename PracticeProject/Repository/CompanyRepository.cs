using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PracticeProject.Data;
using PracticeProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeProject.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private IDbConnection db; // Implement IDbConnection interface

        public CompanyRepository(IConfiguration configuration) // Configure DB connection
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Company Add(Company company)
        {
            var sql = "INSERT INTO Companies (Name, Address, City, State, PostalCode)" +
                "VALUES(@Name, @Address, @City, @State, @PostalCode);" + // added ";" but may be wrong
                "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = db.Query<int>(sql, company).Single();
            company.CompanyId = id;
            return company;
        }

        public Company Find(int id)
        {
            var sql = "SELECT * FROM Companies " +
                "WHERE CompanyId = @CompanyId";
            return db.Query<Company>(sql, new { @CompanyId = id }).Single(); // Ensures we only have one ID
                                                                             // Prevents conversion from IEnumerable
                                                                             // error
        }

        public List<Company> GetAll()
        {
            var sql = "SELECT * FROM Companies";
            return db.Query<Company>(sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Companies " +
                "WHERE CompanyId = @Id";
            db.Execute(sql, new { id });
        }

        public Company Update(Company company)
        {
            var sql = "UPDATE Companies " +
                "SET Name = @Name, Address = @Address, City = @City, State = @State, PostalCode = @PostalCode " +
                "WHERE CompanyId = @CompanyId";
            db.Execute(sql, company);
            return company; 
        }
    }
}
