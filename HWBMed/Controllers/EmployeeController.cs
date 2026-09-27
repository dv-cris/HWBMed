using HWBMed.Models;
using HWBMed.Models.ViewModel;
using HWBMed.repo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HWBMed.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IProfileRepo _profileRepo;
        private readonly IEmployeeRepo _employeeRepo;
        
        public EmployeeController(IUserRepo userRepo, IProfileRepo profileRepo, IEmployeeRepo employeeRepo)
        {
            _userRepo = userRepo;
            _profileRepo = profileRepo;
            
            _employeeRepo = employeeRepo;
        }
        public IActionResult Index()
        {
            var userDB = _employeeRepo.All();
            return View(userDB);
        }

        public IActionResult Create(string NIF)
        {
            if (NIF is null) NotFound();
            var userDB = _userRepo.FindNIF(NIF);
            ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
            return View(
                new EmployeeCreateViewModel                
                {
                    Name = userDB.Name,
                    NIF = userDB.NIF,
                    PhoneNumber = userDB.PhoneNumber,
                    Email = userDB.Email,
                    UserID = userDB.Id
                });
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateViewModel employeeModel)
        {
            try
            {
                if (employeeModel is null)
                {
                    TempData["ErrorMenssage"] = "Ocorreu algum erro!";
                    ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                    return View(employeeModel);
                }

                if (ModelState.GetFieldValidationState("Name") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("NIF") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("Email") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("Password") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("ProfileID") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("UserID") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid)
                {
                    var result = await _employeeRepo.AddAsync(employeeModel);
                    if(!result.Succeeded)
                    {
                        TempData["ErrorMenssage"] = $"Algo deu errado!{string.Join(", ", result.Errors.Select(e => e.Description))}";
                        ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                        return View(employeeModel);
                    }
                    TempData["SuccessMessage"] = $"Cadastrado com sucesso!";
                    return RedirectToAction("Index", "User");
                    
                    
                }
                ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                return View(employeeModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMenssage"] = $"Algo deu errado! {ex.Message}";
                return RedirectToAction("index");
            }
        }
        public IActionResult Edit(string id)
        {
            Employee employee = _employeeRepo.FindId(id);
            ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
            return View(new EmployeeCreateViewModel            {
                Name = employee.User.Name,
                NIF = employee.User.NIF,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email!,
                UserID = employee.IdUser,
                ProfileID = employee.IdProfile
            });
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(EmployeeCreateViewModel employeeModel)
        {
            try
            {
                if (ModelState.GetFieldValidationState("Name") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("NIF") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("Email") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("Id") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("ProfileID") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid
                    && ModelState.GetFieldValidationState("UserID") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid)
                {
                    var result = await _employeeRepo.UpdateAsync(employeeModel);
                    if (!result.Succeeded)
                    {
                        TempData["ErrorMenssage"] = $"Algo deu errado no password!{string.Join(", ", result.Errors.Select(e => e.Description))}";
                        ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                        return View(employeeModel);
                    }
                    if (ModelState.GetFieldValidationState("Password") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid)
                    {
                        result = await _employeeRepo.UpadatePassAsync(employeeModel);
                        if (!result.Succeeded)
                        {
                            TempData["ErrorMenssage"] = $"Algo deu errado!{string.Join(", ", result.Errors.Select(e => e.Description))}";
                            ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                            return View(employeeModel);
                        }
                    }
                    TempData["SuccessMessage"] = $"Atualizado com sucesso!";
                    return RedirectToAction("index");
                }
                return View(employeeModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMenssage"] = $"Algo deu errado!: {ex.Message}";
                return RedirectToAction("index");
            }
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            Employee employee = _employeeRepo.FindId(id);
            return View(employee);
        }
        [HttpPost]
        public IActionResult ExeDelete(string id)
        {
            try
            {
                bool ConfirmDelete = _employeeRepo.Delete(id);
                if (ConfirmDelete) TempData["SuccessMessage"] = $"Excluido com sucesso!";
                else TempData["ErrorMenssage"] = $"Algo deu errado!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMenssage"] = $"Algo deu errado!: {ex.Message}";
                return RedirectToAction("index");
            }
        }
    }
}
