using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Week7Context _context;
        public UserRepository(Week7Context context)
        {
            _context = context;
        }

        public User AddNew(User enity)
        {
            if (enity == null)
                throw new ArgumentNullException(nameof(enity));

            _context.Add(enity);
            var status = _context.SaveChanges();
            if (status > 0)
                return enity;

            return null;
        }

        //public user addnew(user[] entities)
        //{
        //    if (enities.count() > 0)
        //        throw new argumentnullexception(nameof(entities));

        //    _context.addrangeasync(enity);
        //    var status = _context.savechanges();
        //    if (status > 0)
        //        return enity;

        //    return null;
        //}

        public bool Delete(User enity)
        {
            if (enity == null)
                throw new ArgumentNullException(nameof(enity));

            _context.Remove(enity);
            var status = _context.SaveChanges();
            if (status > 0)
                return true;

            return false;
        }

        public bool Delete(List<User> enities)
        {
            if (enities.Count() < 1)
                throw new ArgumentNullException(nameof(enities));

            _context.Users.RemoveRange(enities);
            var status = _context.SaveChanges();
            if (status > 0)
                return true;

            return false;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(string Id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == Id);
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User Update(User enity)
        {
            if (enity == null)
                throw new ArgumentNullException(nameof(enity));

            _context.Update(enity);
            var status = _context.SaveChanges();
            if (status > 0)
                return enity;

            return null;
        }

        public IEnumerable<User> Paginate(List<User> list, int perpage, int page)
        {
            page = page < 1 ? 1 : page;
            perpage = perpage < 1 ? 5 : perpage;

            if(list.Count > 0)
            {
                // list.Skip((page - 1) * perpage).Take(perpage)
                var paginated = list.Skip((page - 1) * perpage).Take(perpage).ToList();
                return paginated;
            }

            return new List<User>();
        }
    }
}
