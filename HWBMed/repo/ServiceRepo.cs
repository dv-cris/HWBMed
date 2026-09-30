using HWBMed.Data;
using HWBMed.Models;
using HWBMed.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HWBMed.repo
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly ApplicationDbContext _context;
        public ServiceRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public Service Add(ServiceCreateViewModel serviceModel)
        {
            Service service = new Service()
            {
                Name = serviceModel.Name,
                Price = serviceModel.Price,
                IVA = serviceModel.IVA,
                IdArea = serviceModel.IdArea
            };
            _context.Services.Add(service);
            _context.SaveChanges();
            return service;
        }
        public Service Update(ServiceCreateViewModel service)
        {
            Service serviceDB = FindId(service.Id);
            if (serviceDB == null) throw new Exception("Houve um erro na atualização");
            serviceDB.Price = service.Price;
            serviceDB.IVA = service.IVA;
            _context.Services.Update(serviceDB);
            _context.SaveChanges();
            return serviceDB;
        }
        public bool Delete(int id)
        {
            Service serviceDB = FindId(id);
            if (serviceDB == null) throw new Exception("Houve um erro na atualização");
            _context.Services.Remove(serviceDB);
            _context.SaveChanges();
            return true;
        }
        
        public Service FindId(int id)
        {
            return _context.Services
                .Include(a => a.Area)
                .FirstOrDefault(x => x.Id == id);
        }
        public List<Service> All()
        {
            return _context.Services
                .Include(s => s.Area)
                .ToList();
        }
    }
}
