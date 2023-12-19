using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserProductAgg.Repository
{
    public interface IUserProductRepository
    {
        void UpdateProduct(int ProductId, int UserId, ProductAgg.Product product);
        bool ReadAccessToUpdate(UserProduct userProduct);
        void DeleteProduct(int ProductId, int UserId);
        bool RaedAccessToDelete(UserProduct userProduct);
        void UserAccessToDelete(UserProduct userProduct);
        void UserAccessToUpdate(UserProduct userProduct);
        void EnableAccessToDelete(UserProduct userProduct);
        void EnableAccessToUpdate(UserProduct userProduct);
        void DisableAccess(UserProduct userProduct);
        UserProduct ReadById(int productid, int userid);
    }
}
