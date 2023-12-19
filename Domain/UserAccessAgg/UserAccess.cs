using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAccessAgg
{
    public class UserAccess
    {
        public UserAccess()
        {
            DeleteStatus = false;
        }
        public int Id { get; set; }
        public int userId { get; set; }
        public int productId { get; set; }
        public string Role { get; set; }
        public string username { get; set; }
        public bool AccessDelete { get; set; }
        public bool AccessUpdate { get; set; }
        public bool DeleteStatus { get; set; }
        public UserAgg.User users { get; set; }
        public ProductAgg.Product products { get; set; }

    }
}
