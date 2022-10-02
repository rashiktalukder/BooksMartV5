using BooksMartV5.DataAccess.Data;
using BooksMartV5.DataAccess.Repository.IRepository;
using BooksMartV5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksMartV5.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db):base(db)
        {
            _db=db;
        }

        public void Update(Company company)
        {
            _db.Update(company);
        }
    }
}
