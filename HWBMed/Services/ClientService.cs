using HWBMed.Models.Entities;
using HWBMed.Models.ViewModels;
using HWBMed.Repositories.Interfaces;
using HWBMed.Services.Interfaces;

namespace HWBMed.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public string CreateClient(ClientViewModel client)
        {
            var clientDB = _clientRepository.FindNIF(client.NIF);
            if (clientDB != null)
                return "ErrorMessageClientNIF";

            if (client.UtenteNumber != null)
            {
                clientDB = _clientRepository.FindUN(client.UtenteNumber);
                if (clientDB != null)
                    return "ErrorMessageClientUtenteNumber";
            }

            var saveClient = _clientRepository.Add(new Client()
            {
                Name = client.Name.ToUpper(),
                Birthday = client.Birthday,
                Address = client.Address.ToLower(),
                City = client.City.ToUpper(),
                PostalCode = client.PostalCode.ToUpper(),
                NIF = client.NIF,
                Email = client.Email.ToLower(),
                UtenteNumber = client.UtenteNumber.ToUpper(),
                ContactEmail = client.ContactEmail,
                PhoneNumber = client.PhoneNumber.ToLower(),
                ContactPhone = client.ContactPhone
            });
            if (!saveClient)
                return "ErrorMessageClientRegister";

            return "SuccessMessageClientRegister";
        }
        public string UpdateClient(ClientViewModel client)
        {
            var clientDB = _clientRepository.FindID(client.Id);
            if (clientDB == null)
                return "ErrorMessageClient";

            if (clientDB.NIF != client.NIF)
                return "ErrorMessageClient";

            if (clientDB.UtenteNumber != client.UtenteNumber)
            {
                if (!string.IsNullOrWhiteSpace(client.UtenteNumber))
                {
                    var clientUtente = _clientRepository.FindUN(client.UtenteNumber);
                    if (clientUtente != null && clientUtente.Id != client.Id)
                        return "ErrorMessageClientUtenteNumber";
                }
                clientDB.UtenteNumber = client.UtenteNumber;
            }
            clientDB.Name = client.Name;
            clientDB.Birthday = client.Birthday;
            clientDB.Address = client.Address;
            clientDB.City = client.City;
            clientDB.PostalCode = client.PostalCode;
            clientDB.PhoneNumber = client.PhoneNumber;
            clientDB.Email = client.Email;
            clientDB.ContactPhone = client.ContactPhone;
            clientDB.ContactEmail = client.ContactEmail;
            var saveClient = _clientRepository.Update(clientDB);
            if (!saveClient)
                return "ErrorMessageClientUpdate";
            return "SuccessMessageClientUpdate";
        }
        public string DeleteClient(ClientDeleteModel client)
        {
            var clientDB = _clientRepository.FindID(client.Id);
            if (clientDB == null)
                return "ErrorMessageClient";

            if (clientDB.NIF != client.NIF)
                return "ErrorMessageClient";

            if (clientDB.UtenteNumber != client.UtenteNumber)
                return "ErrorMessageClient";

            var saveClient = _clientRepository.Delete(clientDB);
            if (!saveClient)
                return "ErrorMessageClientDelete";

            return "SuccessMessageClientDelete";
        }
        public (string, ClientViewModel?) GetClientID(int id)
        {
            Client c = _clientRepository.FindID(id);
            if (c is null)
                return ("ErrorMessageClient", null);

            return ("SuccessMessageClientID", new ClientViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Birthday = c.Birthday,
                Address = c.Address,
                City = c.City,
                PostalCode = c.PostalCode,
                NIF = c.NIF,
                Email = c.Email,
                UtenteNumber = c.UtenteNumber,
                ContactEmail = c.ContactEmail,
                PhoneNumber = c.PhoneNumber,
                ContactPhone = c.ContactPhone
            });
        }

        public List<ClientViewModel> GetClients()
        {
            var clients = _clientRepository.All();
            return clients.Select(c => new ClientViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Birthday = c.Birthday,
                Address = c.Address,
                City = c.City,
                PostalCode = c.PostalCode,
                NIF = c.NIF,
                Email = c.Email,
                UtenteNumber = c.UtenteNumber,
                ContactEmail = c.ContactEmail,
                PhoneNumber = c.PhoneNumber,
                ContactPhone = c.ContactPhone
            }).ToList();
        }
    }
}
