using HWBMed.Models;
using HWBMed.Models.ViewModel;

namespace HWBMed.repo
{
    public interface IServiceRepo
    {
        List<Service> All();
        Service FindId(int id);
        Service Add(ServiceCreateViewModel service);
        Service Update(ServiceCreateViewModel service);
        bool Delete(int id);
    }
}
