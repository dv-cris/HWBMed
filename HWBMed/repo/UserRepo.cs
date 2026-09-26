using HWBMed.Data;
using HWBMed.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HWBMed.repo
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;
        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public User Update(User user)
        {
            User userDB = ListId(user.Id);
            if (userDB == null) throw new Exception("Houve um erro na atualização");
            userDB.Name = user.Name;
            userDB.BirthDate = user.BirthDate;
            userDB.Address = user.Address;
            userDB.Local = user.Local;
            userDB.PostalCode = user.PostalCode;
            userDB.PhoneNumber = user.PhoneNumber;
            userDB.UtenteNumber = user.UtenteNumber;
            userDB.Email = user.Email;
            userDB.ContactPhone = user.ContactPhone;
            userDB.ContactEmail = user.ContactEmail;
            _context.Users.Update(userDB);
            _context.SaveChanges();
            return userDB;
        }
        public bool Delete(int id)
        {
            User userDB = ListId(id);
            if (userDB == null) throw new Exception("Houve um erro na atualização");
            _context.Users.Remove(userDB);
            _context.SaveChanges();
            return true;
        }
        public List<User> All()
        {
            return _context.Users.ToList();
        }
        public User ListId(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User FindNIF(string NIF)
        {
            return _context.Users.FirstOrDefault(x => x.NIF == NIF);
        }

        public User FindUtente(string UtenteNumber)
        {
            return _context.Users.FirstOrDefault(x => x.UtenteNumber == UtenteNumber);
        }
    }
}
