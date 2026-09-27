using HWBMed.Models;
namespace HWBMed.repo
{
    public interface IProfileRepo
    {
        List<Profile> All();
        Profile FindId(int id);
        Profile FindName(string name);
        Profile Add(Profile profile);
        Profile Update(Profile profile);
        bool Delete(int id);
    }
}
