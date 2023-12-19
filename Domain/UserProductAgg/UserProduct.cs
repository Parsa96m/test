using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserProductAgg
{
    public class UserProduct
    {
        public UserProduct() 
        {
            DeleteStatus = false; 
        }
        //public UserProduct( bool accessdelete, bool accessupdate, bool deletestatus)
        //{
        //    //productId = productid;
        //    //userId = userid;
        //    AccessDelete = accessdelete;
        //    AccessUpdate = accessupdate;
        //    DeleteStatus = deletestatus;
        //}
        public int Id { get;  set; }
        public int product_Id { get;  set; }
        public int user_Id { get;  set; }
        public string username { get;  set; }
        public bool DeleteStatus { get; set; }
        public ProductAgg.Product products { get;  set; }
        public UserAgg.User users { get;  set; }

    }
}
