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
    public class CoverTypeRepository:Repository<CoverType>,ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db):base(db)
        {
            _db= db;
        }

        public void Update(CoverType coverType)
        {
            var objFromDb=_db.CoverTypes.FirstOrDefault(s=>s.Id==coverType.Id);
            if(objFromDb!=null)
            {
                objFromDb.Name=coverType.Name;
            }
        }
    }
}
