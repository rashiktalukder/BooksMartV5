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
    public class ProductRepository:Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db=db;
        }

        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(s=>s.Id==product.Id);
            if(objFromDb!=null)
            {
                if(product.ImageUrl!=null)
                {
                    objFromDb.ImageUrl=product.ImageUrl;
                }
                objFromDb.CoverTypeId=product.CoverTypeId;
                objFromDb.CategoryId=product.CategoryId;
                objFromDb.ISBN=product.ISBN;
                objFromDb.Price=product.Price;
                objFromDb.Description=product.Description;
                objFromDb.Price50=product.Price50;
                objFromDb.Price100=product.Price100;
                objFromDb.ListPrice=product.ListPrice;
                objFromDb.Title=product.Title;
                objFromDb.Author=product.Author;
            }
        }
    }
}
