using HWBMed.Models;
using HWBMed.repo;
using Microsoft.AspNetCore.Mvc;

namespace HWBMed.Controllers
{
    public class AreaController : Controller
    {
        private readonly IAreaRepo _areaRepo;
        public AreaController(IAreaRepo areaRepo)
        {
            _areaRepo = areaRepo;
        }
        public IActionResult Index()
        {
            var areas = _areaRepo.All();
            return View(areas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Area area)
        {
            try
            {
                var areaBD = _areaRepo.FindName(area.Name.ToUpper());
                if (areaBD != null) {
                    TempData["ErrorMessage"] = "Area já cadastrada";
                    return View(area);
                }                
                if (!ModelState.IsValid) return View(area);
                
                area.Name = area.Name.ToUpper();
                _areaRepo.Add(area);
                TempData["SuccessMessage"] = $"Cadastrado com sucesso!";
                return RedirectToAction("index");
                
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado! {ex.Message}";
                return RedirectToAction("index");
            }

        }
        public IActionResult Edit(int id)
        {
            Area area = _areaRepo.FindId(id);
            return View(area);
        }
        [HttpPost]
        public IActionResult Edit(Area area)
        {
            try
            {
                if (!ModelState.IsValid) return View(area);

                _areaRepo.Update(area);
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
        public IActionResult Delete(int id)
        {
            Area area = _areaRepo.FindId(id);
            return View(area);
        }
        [HttpPost]
        public IActionResult ExeDelete(int id)
        {
            try
            {
                bool ConfirmDelete = _areaRepo.Delete(id);
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
