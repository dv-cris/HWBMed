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
                Area area = _areaRepo.FindId(serviceModel.IdArea);
                
                if(area.Services.Any(s => s.Name == serviceModel.Name))
                {
                    TempData["ErrorMessage"] = $"O serviço {serviceModel.Name} já é cadastrado!";
                    return View(serviceModel);
                }
                
                if (!ModelState.IsValid)
                {
                    ViewData["Areas"] = new SelectList(_areaRepo.All(), "Id", "Name");
                    return View(serviceModel);
                }
                _serviceRepo.Add(serviceModel);
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
            var service = _serviceRepo.FindId(id);
            return View(new ServiceCreateViewModel()
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
                if (!ModelState.IsValid) return View(service);
                
                _serviceRepo.Update(service);
                TempData["SuccessMessage"] = $"Atualizado com sucesso!";
                return RedirectToAction("index");

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
