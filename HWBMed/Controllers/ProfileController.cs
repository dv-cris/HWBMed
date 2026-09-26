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
        public IActionResult Index()
        {
            var profiles = _profileRepo.All();
            return View(profiles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Profile profile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _profileRepo.Add(profile);
                    TempData["SuccessMessage"] = $"Cadastrado com sucesso!";
                    return RedirectToAction("index");
                }
                return View(profile);
            }
            catch (Exception ex)
            {
                TempData["ErrorMenssage"] = $"Algo deu errado! {ex.Message}";
                return RedirectToAction("index");
            }
            
        }

        public IActionResult Edit(int id)
        {
            Profile profile = _profileRepo.ListId(id);
            return View(profile);
        }
        [HttpPost]
        public IActionResult Edit(Profile profile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _profileRepo.Update(profile);
                    TempData["SuccessMessage"] = $"Atualizado com sucesso!";
                    return RedirectToAction("index");
                }
                return View(profile);
            }
            catch(Exception ex)
            {
                TempData["ErrorMenssage"] = $"Algo deu errado!: {ex.Message}";
                return RedirectToAction("index");
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Profile profile = _profileRepo.ListId(id);
            return View(profile);
        }
        [HttpPost]
        public IActionResult ExeDelete(int id)
        {
            try {
                bool ConfirmDelete = _profileRepo.Delete(id);
                if(ConfirmDelete) TempData["SuccessMessage"] = $"Excluido com sucesso!";
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
