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
                if (ModelState.IsValid)
                {
                    _areaRepo.Add(area);
                    TempData["SuccessMessage"] = $"Cadastrado com sucesso!";
                    return RedirectToAction("index");
                }
                return View(area);
            }
            catch (Exception ex)
            {
                TempData["ErrorMenssage"] = $"Algo deu errado! {ex.Message}";
                return RedirectToAction("index");
            }

        }
        public IActionResult Edit(int id)
        {
            Area area = _areaRepo.ListId(id);
            return View(area);
        }
        [HttpPost]
        public IActionResult Edit(Area area)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _areaRepo.Update(area);
                    TempData["SuccessMessage"] = $"Atualizado com sucesso!";
                    return RedirectToAction("index");
                }
                return View(area);
            }
            catch (Exception ex)
            {
                TempData["ErrorMenssage"] = $"Algo deu errado!: {ex.Message}";
                return RedirectToAction("index");
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Area area = _areaRepo.ListId(id);
            return View(area);
        }
        [HttpPost]
        public IActionResult ExeDelete(int id)
        {
            try
            {
                bool ConfirmDelete = _areaRepo.Delete(id);
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
