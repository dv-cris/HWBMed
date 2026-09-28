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
            var result = await _userManager.CreateAsync(employee, employeeModel.Password);
            if (result.Succeeded)
            {
                foreach (var areaId in employeeModel.ListAreaIds)
                {
                    _context.employeeAreas.Add(new EmployeeArea
                    {
                        IdEmployee = employee.Id,
                        IdArea = areaId
                    });
                }
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //var mensagem = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    await _userManager.DeleteAsync(employee);
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "EmployeeAreaSaveError",
                        Description = $"O funcionário foi validado, mas ocorreu um erro ao vincular as áreas: {ex.Message}"
                    });
                }
            }
            return result;
        }
        public async Task<IdentityResult> UpdateAsync(EmployeeCreateViewModel employeeModel)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Employee employeeDB = FindId(employeeModel.Id);
                if (employeeDB == null)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Houve um erro na atualização");
                }
                employeeDB.Email = employeeModel.Email;
                employeeDB.PhoneNumber = employeeModel.PhoneNumber;
                employeeDB.IdProfile = employeeModel.ProfileID;
                var result =  await _userManager.UpdateAsync(employeeDB);
                if (!result.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return result; 
                }
                if(!string.IsNullOrWhiteSpace(employeeModel.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(employeeDB);
                    result = await _userManager.ResetPasswordAsync(employeeDB, token, employeeModel.Password);
                    if (!result.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return result;
                    }
                }

                
                _context.employeeAreas.RemoveRange(employeeDB.EmployeeAreas);
                foreach (var areaId in employeeModel.ListAreaIds)
                {                    
                    _context.employeeAreas.Add(new EmployeeArea
                    {
                        IdEmployee = employeeDB.Id,
                        IdArea = areaId
                    });
                }
               
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                //var mensagem = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                await transaction.RollbackAsync();
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "EmployeeAreaSaveError",
                    Description = $"O funcionário foi validado, mas ocorreu um erro: {ex.Message}"
                });
            }
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
            return _context.Employees.Include(e => e.User).Include(e => e.Profile).Include(e => e.EmployeeAreas).FirstOrDefault(x => x.Id == id);
        }
    }
}
