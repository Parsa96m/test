using Infrastucture.Persistent.Ef.ProductAgg;
using Infrastucture.Persistent.Ef.UesrAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Persistent.Ef.UserProductAgg
{
    public class UserProductModel
    {
        public UserProductModel()
        {
            DeleteStatus = false;
        }
        public int Id { get;  set; }
        public int product_Id { get;  set; }
        public int user_Id { get;  set; }
        public bool DeleteStatus { get; set; }
        public ProductModel products { get;  set; }
        public UserModel users { get;  set; }
    }
}
