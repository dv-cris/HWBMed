using HWBMed.Models.ViewModels;
using HWBMed.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NToastNotify;

namespace HWBMed.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IToastNotification _toastNotification;
        private readonly IStringLocalizer<SharedResource> _Localize;
        public ClientController(IClientService clientService, IToastNotification toastNotification, IStringLocalizer<SharedResource> Localizer)
        {
            _clientService = clientService;
            _toastNotification = toastNotification;
            _Localize = Localizer;
        }

        public IActionResult Index()
        {
            var clients = _clientService.GetClients().ToList();
            return View(clients);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ClientViewModel client)
        {
            if (!ModelState.IsValid)
                return View(client);
            var result = _clientService.CreateClient(client);
            if (result.Contains("Error"))
            {
                _toastNotification.AddErrorToastMessage(_Localize[result]);
                return View(client);
            }

            _toastNotification.AddSuccessToastMessage(_Localize[result]);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var (message, client) = _clientService.GetClientID(id);
            if (client is null)
            {
                _toastNotification.AddErrorToastMessage(_Localize[message]);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }
        [HttpPost]
        public IActionResult Edit(ClientViewModel client)
        {
            if (!ModelState.IsValid)
                return View(client);
            var result = _clientService.UpdateClient(client);
            if (result.Contains("Error"))
            {
                _toastNotification.AddErrorToastMessage(_Localize[result]);
                return View(client);
            }
            _toastNotification.AddSuccessToastMessage(_Localize[result]);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var (message, client) = _clientService.GetClientID(id);
            if (client is null)
            {
                _toastNotification.AddErrorToastMessage(_Localize[message]);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }
        [HttpPost]
        public IActionResult Delete(ClientDeleteModel client)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage(_Localize["ErrorMessageClientDelete"]);
                var (message, clientDB) = _clientService.GetClientID(client.Id);
                return View(clientDB);
            }
            var result = _clientService.DeleteClient(client);
            if (result.Contains("Error"))
            {
                _toastNotification.AddErrorToastMessage(_Localize[result]);
                var (message, clientDB) = _clientService.GetClientID(client.Id);
                return View(clientDB);
            }
            _toastNotification.AddSuccessToastMessage(_Localize[result]);
            return RedirectToAction(nameof(Index));
        }
    }
}
