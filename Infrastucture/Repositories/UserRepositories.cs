using Domain.UserAgg;
using Domain.UserAgg.Repository;
using Infrastucture.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repositories
{
    public class UserRepositories : IUserRepository
    {
        private readonly MyDBContex _contex;
        public UserRepositories(MyDBContex contex)
        {
            _contex = contex;
        }
        public void Create(User userme)
        {
            _contex.user.Add(userme);
            _contex.SaveChangesAsync();
        }

        //public void Create(User userme)
        //{
        //    _contex.user.Add(userme);
        //    _contex.SaveChangesAsync();
        //}

        public void Delete(int id)
        {
            var q = _contex.user.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                q.DeleteStatus = true;
                _contex.SaveChanges();
            }
        }
        public void Update(int id,User userme)
        {
            var q = _contex.user.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                q.Name = userme.Name;
                q.UserName = userme.UserName;
                q.PhoneUser = userme.PhoneUser;
                q.Email = userme.Email;
                q.Password = userme.Password;
                _contex.SaveChanges();
            }
        }
        public User GetByusername(string username)
        {
           return _contex.user.Where(i => i.UserName == username).FirstOrDefault();
        }
        public List<User> Read()
        {
            var q = _contex.user.Where(i => i.DeleteStatus == false).Select(i => i).ToList();
            if (q != null)
            {
                return q;
            }
            return null;
        
        }
    }
}
