using HWBMed.Models;
namespace HWBMed.repo
{
    public interface IProfileRepo
    {
        Task<List<Profile>> AllAsync();
        Task<Profile?> FindIdAsync(string id);
        Task<Profile?> FindNameAsync(string name);
        Task<Profile> AddAsync(Profile profile);
        Task<Profile> UpdateAsync(Profile profile);
        Task<bool> DeleteAsync(string id);
    }
}
