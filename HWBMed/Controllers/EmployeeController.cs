using HWBMed.Models;
using HWBMed.Models.ViewModel;
using HWBMed.repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> CreateAsync(string NIF)
        {
            if (NIF is null) NotFound();
            var userDB = _userRepo.FindNIF(NIF);
            if (userDB.Employee != null)
            {
                TempData["ErrorMessage"] = "Login já cadastrado!";
                return RedirectToAction("Index", "User");
            }
            var profiles = await _profileRepo.AllAsync();
            ViewData["Profiles"] = new SelectList(profiles, "Id", "Name");
            ViewData["Areas"] = _areaRepo.All();
            return View(
                new EmployeeCreateViewModel()
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
                User user = _userRepo.FindNIF(employeeModel.NIF);
                if (user.Employee != null || !employeeModel.ListAreaIds.Any())
                {
                    TempData["ErrorMessage"] = $"Usuário já cadastrado!";
                    return RedirectToAction("Index", "User");
                }
                if (employeeModel is null)
                {
                    TempData["ErrorMessage"] = "Ocorreu algum erro!";
                    ViewData["Areas"] = _areaRepo.All();
                    var profiles = await _profileRepo.AllAsync();
                    ViewData["Profiles"] = new SelectList(profiles, "Id", "Name");
                    return View(employeeModel);
                }
                if (employeeModel.ListAreaIds == null || !employeeModel.ListAreaIds.Any())
                {
                    ModelState.AddModelError("ListAreaIds", "Selecione pelo menos uma área de atuação.");
                    ViewData["Areas"] = _areaRepo.All();
                    var profiles = await _profileRepo.AllAsync();
                    ViewData["Profiles"] = new SelectList(profiles, "Id", "Name");
                    return View(employeeModel);
                }
                
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                {
                    var profiles = await _profileRepo.AllAsync();
                    ViewData["Profiles"] = new SelectList(profiles, "Id", "Name");
                    ViewData["Areas"] = _areaRepo.All();
                    return View(employeeModel);
                }

                var result = await _employeeRepo.AddAsync(employeeModel);
                if (!result.Succeeded)
                {
                    TempData["ErrorMessage"] = $"Algo deu errado!{string.Join(", ", result.Errors.Select(e => e.Description))}";
                    ViewData["Areas"] = _areaRepo.All();
                    var profiles = await _profileRepo.AllAsync();
                    ViewData["Profiles"] = new SelectList(profiles, "Id", "Name");
                    return View(employeeModel);
                }
                TempData["SuccessMessage"] = $"Cadastrado com sucesso!";
                return RedirectToAction("Index", "User");
                
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado! {ex.Message}";
                return RedirectToAction("index");
            }
        }
        public async Task<IActionResult> EditAsync(string id)
        {
            Employee employee = _employeeRepo.FindId(id);
            var profiles = await _profileRepo.AllAsync();
            ViewData["Profiles"] = new SelectList(profiles, "Id", "Name");
            ViewData["Areas"] = _areaRepo.All();
            return View(new EmployeeCreateViewModel()
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
                    var profiles = await _profileRepo.AllAsync();
                    ViewData["Profiles"] = new SelectList(profiles, "Id", "Name");
                    ViewData["Areas"] = _areaRepo.All();
                    return View(employeeModel);
                }

                if (employeeModel.ListAreaIds == null || !employeeModel.ListAreaIds.Any())
                {
                    ModelState.AddModelError("ListAreaIds", "Selecione pelo menos uma área de atuaçãoW.");
                    var profiles = await _profileRepo.AllAsync();
                    ViewData["Profiles"] = new SelectList(profiles, "Id", "Name");
                    ViewData["Areas"] = _areaRepo.All();
                    return View(employeeModel);
                }
                var result = await _employeeRepo.UpdateAsync(employeeModel);
                if (!result.Succeeded)
                {
                    TempData["ErrorMessage"] = $"{string.Join(", ", result.Errors.Select(e => e.Description))}";
                    var profiles = await _profileRepo.AllAsync();
                    ViewData["Profiles"] = new SelectList(profiles, "Id", "Name");
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
                if (!ConfirmDelete) TempData["ErrorMessage"] = $"Algo deu errado!";
                else TempData["SuccessMessage"] = $"Excluido com sucesso!"; 
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
