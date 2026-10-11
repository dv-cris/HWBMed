using HWBMed.Models.Entities;

namespace HWBMed.Repositories.Interfaces
{
    public interface IClientRepository
    {
        bool Add(Client client);
        bool Update(Client client);
        bool Delete(Client client);
        List<Client> All();
        Client FindID(int Id);
        Client FindNIF(string NIF);
        Client FindUN(string UN);
    }
}
