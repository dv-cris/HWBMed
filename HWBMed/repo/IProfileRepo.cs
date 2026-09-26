using HWBMed.Models;
namespace HWBMed.repo
{
    public interface IProfileRepo
    {
        List<Profile> All();
        Profile ListId(int id);
        Profile Add(Profile profile);
        Profile Update(Profile profile);
        bool Delete(int id);
    }
}
