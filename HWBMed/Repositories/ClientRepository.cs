using HWBMed.Data;
using HWBMed.Models.Entities;
using HWBMed.Repositories.Interfaces;

namespace HWBMed.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Client client)
        {
            _context.Clients.Add(client);
            var addClient = _context.SaveChanges();
            return addClient > 0;
        }
        public bool Update(Client client)
        {
            _context.Clients.Update(client);
            var updateClient = _context.SaveChanges();
            return updateClient > 0;
        }
        public bool Delete(Client client)
        {
            _context.Clients.Remove(client);
            var deleteClient = _context.SaveChanges();
            return deleteClient > 0;
        }
        public List<Client> All()
        {
            return _context.Clients.ToList();
        }

        public Client FindID(int Id)
        {
            return _context.Clients.FirstOrDefault(c => c.Id == Id);
        }

        public Client FindNIF(string NIF)
        {
            return _context.Clients.FirstOrDefault(c => c.NIF == NIF);
        }

        public Client FindUN(string UN)
        {
            return _context.Clients.FirstOrDefault(c => c.UtenteNumber == UN);
        }
    }
}
