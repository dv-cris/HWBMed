using HWBMed.Data;
using HWBMed.Models;
using HWBMed.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HWBMed.repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;
        public EmployeeRepo(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddAsync(EmployeeCreateViewModel employeeModel)
        {
            Employee employee = new Employee()
            {
                UserName = employeeModel.Email,
                Email = employeeModel.Email,
                PhoneNumber = employeeModel.PhoneNumber,
                IdProfile = employeeModel.ProfileID,
                IdUser = employeeModel.UserID,
                EmailConfirmed = true
            };
            return await _userManager.CreateAsync(employee, employeeModel.Password); 
        }
        public async Task<IdentityResult> UpdateAsync(EmployeeCreateViewModel employeeModel)
        {
            Employee employeeDB = FindId(employeeModel.Id);
            if (employeeDB == null) throw new Exception("Houve um erro na atualização");
            employeeDB.Email = employeeModel.Email;
            employeeDB.PhoneNumber = employeeModel.PhoneNumber;
            employeeDB.IdProfile = employeeModel.ProfileID;            
            return await _userManager.UpdateAsync(employeeDB);
        }
        public async Task<IdentityResult> UpadatePassAsync(EmployeeCreateViewModel employeeModel)
        {
            Employee employeeDB = FindId(employeeModel.Id);
            if (employeeDB == null) throw new Exception("Houve um erro na atualização");
            var token = await _userManager.GeneratePasswordResetTokenAsync(employeeDB);
            return await _userManager.ResetPasswordAsync(employeeDB, token, employeeModel.Password);
        }

        public bool Delete(string id)
        {
            Employee employeeDB = FindId(id);
            if (employeeDB == null) throw new Exception("Houve um erro na atualização");
            _context.Employees.Remove(employeeDB);
            _context.SaveChanges();
            return true;
        }
        IEnumerable<Employee> IEmployeeRepo.All()
        {
            return _context.Employees.Include(e => e.User).Include(e => e.Profile).ToList();
        }
        public Employee FindId(string id)
        {
            return _context.Employees.Include(e => e.User).Include(e => e.Profile).FirstOrDefault(x => x.Id == id);
        }        
    }
}
