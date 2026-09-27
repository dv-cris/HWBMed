using HWBMed.Data;
using HWBMed.Models;

namespace HWBMed.repo
{
    public class ProfileRepo : IProfileRepo
    {
        private readonly ApplicationDbContext _context;
        public ProfileRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public Profile Add(Profile profile)
        {
            _context.Profiles.Add(profile);
            _context.SaveChanges();
            return profile;
        }
        public Profile Update(Profile profile)
        {
            Profile profileDB = FindId(profile.Id);
            if (profileDB == null) throw new Exception("Houve um erro na atualização");
            profileDB.Name = profile.Name;
            profileDB.Discount = profile.Discount;
            _context.Profiles.Update(profileDB);
            _context.SaveChanges();
            return profileDB;
        }
        public bool Delete(int id)
        {
            Profile profileDB = FindId(id);
            if (profileDB == null) throw new Exception("Houve um erro na atualização");
            _context.Profiles.Remove(profileDB);
            _context.SaveChanges();
            return true;
        }
        public List<Profile> All()
        {
            return _context.Profiles.ToList();
        }
        
        public Profile FindId(int id)
        {
            return _context.Profiles.FirstOrDefault(x => x.Id == id);
        }

        public Profile FindName(string name)
        {
            return _context.Profiles.FirstOrDefault(x => x.Name == name);
        }
    }
}
