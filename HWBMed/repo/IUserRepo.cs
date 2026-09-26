using HWBMed.Models;

namespace HWBMed.repo
{
    public interface IUserRepo
    {
        List<User> All();
        User FindNIF(string NIF);
        User FindUtente(string UtenteNumber);
        User ListId(int id);
        User Add(User user);
        User Update(User user);
        bool Delete(int id);
    }
}
