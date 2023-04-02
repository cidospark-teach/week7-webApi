using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        User AddNew(User enity);
        User Update(User enity);
        bool Delete(User enity);
        public bool Delete(List<User> enities);
        User GetById(string Id);
        User GetByEmail(string email);
        IEnumerable<User> GetAll();
        IEnumerable<User> Paginate(List<User> list, int perpage, int page);
    }
}
