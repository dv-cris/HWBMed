using HWBMed.Models;
using HWBMed.Models.ViewModel;
using HWBMed.repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HWBMed.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceRepo _serviceRepo;
        private readonly IAreaRepo _areaRepo;
        public ServiceController(IServiceRepo serviceRepo, IAreaRepo areaRepo)
        {
            _serviceRepo = serviceRepo;
            _areaRepo = areaRepo;
        }
        public IActionResult Index()
        {
            var service = _serviceRepo.All();
            return View(service);
        }
        public IActionResult Create()
        {
            ViewData["Areas"] = new SelectList(_areaRepo.All(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(ServiceCreateViewModel serviceModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _serviceRepo.Add(serviceModel);
                    TempData["SuccessMessage"] = $"Cadastrado com sucesso!";
                    return RedirectToAction("index");
                }
                ViewData["Areas"] = new SelectList(_areaRepo.All(), "Id", "Name");
                return View(serviceModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado! {ex.Message}";
                return RedirectToAction("index");
            }
        }
        public IActionResult Edit(int id)
        {
            var service = _serviceRepo.FindId(id);
            return View(new ServiceCreateViewModel
            {
                Name = service.Name,
                Price = service.Price,
                IVA = service.IVA,
                IdArea = service.IdArea,
                Area = service.Area.Name
            });
        }
        [HttpPost]
        public IActionResult Edit(ServiceCreateViewModel service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _serviceRepo.Update(service);
                    TempData["SuccessMessage"] = $"Atualizado com sucesso!";
                    return RedirectToAction("index");
                }
                return View(service);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Algo deu errado!: {ex.Message}";
                return RedirectToAction("index");
            }
        }
        public IActionResult Delete(int id)
        {
            Service service = _serviceRepo.FindId(id);
            return View(service);
        }
        [HttpPost]
        public IActionResult ExeDelete(int id)
        {
            try
            {
                bool ConfirmDelete = _serviceRepo.Delete(id);
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
