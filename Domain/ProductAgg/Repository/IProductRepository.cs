using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductAgg.Repository
{
    public interface IProductRepository
    {
        void CreateProduct(ProductAgg.Product product);
        void GetById(int id);
        List<Product> FilterProductByUserName(string username);
        List<Product> ReadProductByUserNow(string iduser);
        List<Product> ReadProduct();
        List<Product> ReadProductByUser(string username);

        //List<productModel> ReadProductByUser(string username);
        //List<productModel> FilterProductByUserName(string username);
        //Task<List<productModel>> ReadProductByUserNow();
        //Task<List<productModel>> ReadProduct();
    }
}
