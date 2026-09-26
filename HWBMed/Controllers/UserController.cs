using HWBMed.Models;
using HWBMed.repo;
using Microsoft.AspNetCore.Mvc;

namespace HWBMed.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;
        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public IActionResult Index()
        {
            var users = _userRepo.All();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                var userDB = _userRepo.FindUtente(user.UtenteNumber);
                if(userDB != null)
                {
                    TempData["ErrorMenssage"] = "Utente já cadastrado";
                    return View(user);
                }
                userDB = _userRepo.FindNIF(user.NIF);
                if (userDB != null)
                {
                    TempData["ErrorMenssage"] = "NIF já cadastrado";
                    return View(user);
                }

                if (ModelState.IsValid)
                {
                    _userRepo.Add(user);
                    TempData["SuccessMessage"] = $"Cadastrado com sucesso!";
                    return RedirectToAction("index");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMenssage"] = $"Algo deu errado! {ex.Message}";
                return RedirectToAction("index");
            }
        }
        public IActionResult Edit(int id)
        {
            User user = _userRepo.ListId(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepo.Update(user);
                    TempData["SuccessMessage"] = $"Atualizado com sucesso!";
                    return RedirectToAction("index");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMenssage"] = $"Algo deu errado!: {ex.Message}";
                return RedirectToAction("index");
            }
        }
        public IActionResult Delete(int id)
        {
            User user = _userRepo.ListId(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult ExeDelete(int id)
        {
            try
            {
                bool ConfirmDelete = _userRepo.Delete(id);
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
