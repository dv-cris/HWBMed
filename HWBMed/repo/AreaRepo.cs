using HWBMed.Data;
using HWBMed.Models;

namespace HWBMed.repo
{
    public class AreaRepo : IAreaRepo
    {
        private readonly ApplicationDbContext _context;
        public AreaRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public Area Add(Area area)
        {
            _context.Areas.Add(area);
            _context.SaveChanges();
            return area;
        }

        public Area Update(Area area)
        {
            Area areaDB = ListId(area.Id);
            if (areaDB == null) throw new Exception("Houve um erro na atualização");
            areaDB.Name = area.Name;
            _context.Areas.Update(areaDB);
            _context.SaveChanges();
            return areaDB;
        }
        public bool Delete(int id)
        {
            Area areaDB = ListId(id);
            if (areaDB == null) throw new Exception("Houve um erro na atualização");
            _context.Areas.Remove(areaDB);
            _context.SaveChanges();
            return true;
        }
        public List<Area> All()
        {
            return _context.Areas.ToList();
        }
        public Area ListId(int id)
        {
            return _context.Areas.FirstOrDefault(x => x.Id == id);
        }
    }
}
