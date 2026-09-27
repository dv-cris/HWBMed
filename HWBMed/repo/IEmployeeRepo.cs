using HWBMed.Models;
using HWBMed.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace HWBMed.repo
{
    public interface IEmployeeRepo
    {
        IEnumerable<Employee> All();
        Employee FindId(string id);
        Task<IdentityResult> AddAsync(EmployeeCreateViewModel employee);

        Task<IdentityResult> UpadatePassAsync(EmployeeCreateViewModel employeeModel);
        Task<IdentityResult> UpdateAsync(EmployeeCreateViewModel employeeModel);
        bool Delete(string id);
    }
}
