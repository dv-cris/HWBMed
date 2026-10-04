using HWBMed.Data;
using HWBMed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HWBMed.repo
{
    public class ProfileRepo : IProfileRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<Profile> _roleManager;
        public ProfileRepo(ApplicationDbContext context, RoleManager<Profile> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<Profile> AddAsync(Profile profile)
        {
            await _roleManager.CreateAsync(profile);            
            return profile;
        }
        public async Task<Profile> UpdateAsync(Profile profile)
        {
            Profile profileDB = await FindIdAsync(profile.Id);
            if (profileDB == null) throw new Exception("Houve um erro na atualização");
            profileDB.Name = profile.Name;
            profileDB.Discount = profile.Discount;
            await _roleManager.UpdateAsync(profileDB);            
            return profileDB;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            Profile profileDB = await FindIdAsync(id);
            if (profileDB == null) throw new Exception("Houve um erro na atualização");
            await _roleManager.DeleteAsync(profileDB);            
            return true;
        }
        public async Task<List<Profile>> AllAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profile> FindIdAsync(string id)
        {
            return await _context.Profiles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Profile> FindNameAsync(string name)
        {
            return await _context.Profiles.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
