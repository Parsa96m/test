using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAgg.Repository
{
    public interface IUserRepository
    {
        public void Create(User userme);
        public void Delete(int id);
        public void Update(int id, User userme);
        public List<User> Read();
        User GetByusername(string username);
    }
}
