using HWBMed.Models.ViewModels;

namespace HWBMed.Services.Interfaces
{
    public interface IClientService
    {
        string CreateClient(ClientViewModel client);
        string UpdateClient(ClientViewModel client);
        string DeleteClient(ClientDeleteModel client);
        List<ClientViewModel> GetClients();
        (string, ClientViewModel?) GetClientID(int id);
    }
}
