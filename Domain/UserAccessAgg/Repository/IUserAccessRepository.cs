using Domain.ProductAgg;
using Domain.UserAccessAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAccessAgg.Repository
{
    public interface IUserAccessRepository
    {
        void CreateUserAccessRole(UserAccess userAccess);
        List<UserAccess> ReadUserAccessRole();
        void UpdateUserAccessRole(int id, UserAccess userAccess);
        void DeleteUserAccessRole(int id);
        void UpdateProduct(int ProductId, int UserId, Product product);
        bool ReadAccessToUpdate(UserAccess userAccess);
        void DeleteProduct(int ProductId, int UserId);
        bool RaedAccessToDelete(UserAccess userAccess);
        void UserAccessToDelete(UserAccess userAccess);
        void UserAccessToUpdate(UserAccess userAccess);
        void EnableAccessToDelete(UserAccess userAccess);
        void EnableAccessToUpdate(UserAccess userAccess);
        void DisableAccessToDelete(UserAccess userAccess);
        void DisableAccessToUpdate(UserAccess userAccess);
        UserAccess ReadById(int productid, int userid);
    }
}
