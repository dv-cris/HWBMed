using HWBMed.Models;

namespace HWBMed.repo
{
    public interface IAreaRepo
    {
        List<Area> All();
        Area ListId(int id);
        Area Add(Area area);
        Area Update(Area area);
        bool Delete(int id);
    }
}
