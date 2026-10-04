using HWBMed.Models;
using HWBMed.repo;
using Microsoft.AspNetCore.Mvc;

namespace HWBMed.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepo _profileRepo;
        public ProfileController(IProfileRepo profileRepo)
        {
            _profileRepo = profileRepo;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var profiles = await _profileRepo.AllAsync();
            return View(profiles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Profile profile)
        {
            try
            {
                var profileDB = await _profileRepo.FindNameAsync(profile.Name);
                if (profileDB != null)
                {
                    TempData["ErrorMessage"] = "Perfil já cadastrado!";
                    return View(profile);
                }
                if (!ModelState.IsValid) return View(profile);

                await _profileRepo.AddAsync(profile);
                TempData["SuccessMessage"] = $"Cadastrado com sucesso!";
                return RedirectToAction("index");
                
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado! {ex.Message}";
                return RedirectToAction("index");
            }            
        }

        public async Task<IActionResult> EditAsync(string id)
        {
            Profile profile = await _profileRepo.FindIdAsync(id);
            return View(profile);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(Profile profile)
        {
            try
            {
                if (!ModelState.IsValid) return View(profile);                 
                
                await _profileRepo.UpdateAsync(profile);
                TempData["SuccessMessage"] = $"Atualizado com sucesso!";
                return RedirectToAction("index");

            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado!: {ex.Message}";
                return RedirectToAction("index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            Profile profile = await _profileRepo.FindIdAsync(id);
            return View(profile);
        }
        [HttpPost]
        public async Task<IActionResult> ExeDeleteAsync(string id)
        {
            try {
                bool ConfirmDelete = await _profileRepo.DeleteAsync(id);
                if(!ConfirmDelete) TempData["ErrorMessage"] = $"Algo deu errado!";
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
