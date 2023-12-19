using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ProductAgg;

namespace Infrastucture.Persistent.Ef.UserAccessAgg
{
    public class UserAccessModel
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public int userId { get; set; }
        public string Role { get; set; }
        public string username { get; set; }
        public bool AccessDelete { get; set; }
        public bool AccessUpdate { get; set; }
        public bool DeleteStatus { get; set; }
        public Infrastucture.Persistent.Ef.UesrAgg.UserModel users { get; set; }
        public Infrastucture.Persistent.Ef.ProductAgg.ProductModel products { get; set; }
    }
}
