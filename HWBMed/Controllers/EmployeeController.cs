using HWBMed.Models;
using HWBMed.Models.ViewModel;
using HWBMed.repo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HWBMed.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IProfileRepo _profileRepo;
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IAreaRepo _areaRepo;

        public EmployeeController(IUserRepo userRepo, IProfileRepo profileRepo, IEmployeeRepo employeeRepo, IAreaRepo areaRepo)
        {
            _userRepo = userRepo;
            _profileRepo = profileRepo;            
            _employeeRepo = employeeRepo;
            _areaRepo = areaRepo;
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
            ViewData["Areas"] = _areaRepo.All();
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
                    TempData["ErrorMessage"] = "Ocorreu algum erro!";
                    ViewData["Areas"] = _areaRepo.All();
                    ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                    return View(employeeModel);
                }
                if (employeeModel.ListAreaIds == null || !employeeModel.ListAreaIds.Any())
                {
                    ModelState.AddModelError("ListAreaIds", "Selecione pelo menos uma área de atuação.");
                    ViewData["Areas"] = _areaRepo.All();
                    ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                    return View(employeeModel);
                }
                ModelState.Remove("Id");
                if(ModelState.IsValid)
                {
                    var result = await _employeeRepo.AddAsync(employeeModel);
                    if(!result.Succeeded)
                    {
                        TempData["ErrorMessage"] = $"Algo deu errado!{string.Join(", ", result.Errors.Select(e => e.Description))}";
                        ViewData["Areas"] = _areaRepo.All();
                        ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                        return View(employeeModel);
                    }
                    TempData["SuccessMessage"] = $"Cadastrado com sucesso!";
                    return RedirectToAction("Index", "User");
                }
                ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                ViewData["Areas"] = _areaRepo.All();
                return View(employeeModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado! {ex.Message}";
                return RedirectToAction("index");
            }
        }
        public IActionResult Edit(string id)
        {
            Employee employee = _employeeRepo.FindId(id);
            ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
            ViewData["Areas"] = _areaRepo.All();

            return View(new EmployeeCreateViewModel
            {
                Name = employee.User.Name,
                NIF = employee.User.NIF,
                PhoneNumber = employee.PhoneNumber,
                ListAreaIds = employee.EmployeeAreas.Select(e => e.IdArea).ToList(),
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
                ModelState.Remove("Password");

                if (!ModelState.IsValid)
                {
                    ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                    ViewData["Areas"] = _areaRepo.All();
                    return View(employeeModel);
                }

                if (employeeModel.ListAreaIds == null || !employeeModel.ListAreaIds.Any())
                {
                    ModelState.AddModelError("ListAreaIds", "Selecione pelo menos uma área de atuaçãoW.");
                    ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                    ViewData["Areas"] = _areaRepo.All();
                    return View(employeeModel);
                }
                
                var result = await _employeeRepo.UpdateAsync(employeeModel);
                if (!result.Succeeded)
                {
                    TempData["ErrorMessage"] = $"{string.Join(", ", result.Errors.Select(e => e.Description))}";
                    ViewData["Profiles"] = new SelectList(_profileRepo.All(), "Id", "Name");
                    ViewData["Areas"] = _areaRepo.All();
                    return View(employeeModel);
                }
                TempData["SuccessMessage"] = $"Atualizado com sucesso!";
                return RedirectToAction("index");                
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado!: {ex.Message}";
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
                else TempData["ErrorMessage"] = $"Algo deu errado!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado!: {ex.Message}";
                return RedirectToAction("index");
            }
        }
    }
}
