using HWBMed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HWBMed.Data.Seed
{
    public class SeedDefault
    {
        public static void SeedAllAsync(ApplicationDbContext context, UserManager<Employee> userManager, RoleManager<Profile> roleManager)
        {
            SeedArea(context).Wait();
            SeedProfile(roleManager).Wait();
            SeedUser(context, userManager).Wait();
        }
        private static async Task SeedArea(ApplicationDbContext context)
        {
            IEnumerable<Area> areas = [new Area(){Name= "Generalista"},
                new Area(){Name = "Ortodontista"},
                new Area(){Name = "Cirurgia Oral"}];
            foreach (var area in areas)
            {
                var areaDB = await context.Areas.FirstOrDefaultAsync(a => a.Name == area.Name);
                if (areaDB == null) await context.Areas.AddAsync(area);
            }
            await context.SaveChangesAsync();
        }
        private static async Task SeedProfile(RoleManager<Profile> roleManager)
        {
            IEnumerable<Profile> ProfileList = [new Profile(){ Name = "administrador", Discount = 100},
                                                new Profile(){Name = "medico", Discount = 20},
                                                new Profile(){ Name = "assistente", Discount = 15},
                                                new Profile(){ Name = "recepcionista", Discount = 10}];
            foreach (var profile in ProfileList)
            {
                var profileDB = await roleManager.RoleExistsAsync(profile.Name);
                if (profileDB == null) await roleManager.CreateAsync(profile);
            }            
        }
        private static async Task SeedRole(RoleManager<IdentityRole> roleManager)
        {

        }
        private static async Task SeedUser(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            List<Profile> profiles = await context.Profiles.ToListAsync();
            List<Area> areas = await context.Areas.ToListAsync();
            IEnumerable<User> usersList =
                [new User(){
                    Name = "Admin", //Nome da empresa
                    BirthDate = DateOnly.Parse("01/01/2000"), //Data de criação da empresa
                    Address = "rua do admin", //Endereço da empresa
                    PostalCode = "1000-000", // Codigo postal da empresa
                    Local = "Portugal",
                    NIF = "111111111", //NIF da empresa
                    PhoneNumber = "999999999", //Numero da empresa
                    Email = "Admin@admin.com", //Email da empresa
                    ContactPhone = false,
                    ContactEmail =false,
                },
                new User(){
                    Name ="Teste Medico 1",
                    BirthDate = DateOnly.Parse("01/02/2000"),
                    Address = "Rua do medico",
                    PostalCode = "2000-201",
                    Local = "Portugal",
                    NIF = "111222333",
                    PhoneNumber = "999888777",
                    Email = "medico1@medico.com",
                    ContactPhone= false,
                    ContactEmail=false
                },
                new User(){
                    Name ="Teste Medico 2",
                    BirthDate = DateOnly.Parse("01/02/2000"),
                    Address = "Rua do medico",
                    PostalCode = "2000-202",
                    Local = "Portugal",
                    NIF = "444555666",
                    PhoneNumber = "999777666",
                    Email = "medico2@medico.com",
                    ContactPhone= false,
                    ContactEmail=false
                },
                new User(){
                    Name ="Teste Medico 3",
                    BirthDate = DateOnly.Parse("01/02/2000"),
                    Address = "Rua do medico",
                    PostalCode = "2000-203",
                    Local = "Portugal",
                    NIF = "777888999",
                    PhoneNumber = "999666555",
                    Email = "medico3@medico.com",
                    ContactPhone= false,
                    ContactEmail=false
                },
                new User(){
                    Name ="Teste Assistente",
                    BirthDate = DateOnly.Parse("01/03/2000"),
                    Address = "Rua do assistente",
                    PostalCode = "2222-222",
                    Local = "Portugal",
                    NIF = "123456789",
                    PhoneNumber = "987654321",
                    Email = "assistente@assistente.com",
                    ContactPhone= false,
                    ContactEmail=false
                },
                new User(){
                    Name ="Teste Recepcionista",
                    BirthDate = DateOnly.Parse("01/04/2000"),
                    Address = "Rua do recepcionista",
                    PostalCode = "2333-333",
                    Local = "Portugal",
                    NIF = "999876543",
                    PhoneNumber = "999876543",
                    Email = "recepcionista@recepcionista.com",
                    ContactPhone= false,
                    ContactEmail=false
                },
                new User(){
                    Name ="Teste Usuario 1",
                    BirthDate = DateOnly.Parse("01/05/2000"),
                    Address = "Rua do usuario",
                    PostalCode = "2433-333",
                    Local = "Portugal",
                    NIF = "998876543",
                    PhoneNumber = "998876543",
                    Email = "usuario1@usuario.com",
                    ContactPhone= false,
                    ContactEmail=false
                },
                new User(){
                    Name ="Teste Usuario 2",
                    BirthDate = DateOnly.Parse("01/06/2000"),
                    Address = "Rua do usuario",
                    PostalCode = "2444-333",
                    Local = "Portugal",
                    NIF = "998877543",
                    PhoneNumber = "998877543",
                    Email = "usuario2@usuario.com",
                    ContactPhone= false,
                    ContactEmail=false
                }
            ];

            foreach (var user in usersList)
            {
                var UserDB = await context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);
                if (UserDB == null)
                {
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                    
                    if (user.Name == "Admin")
                    {
                        var p = profiles.FirstOrDefault(d => d.Name == "administrador");
                        Employee employee = new Employee()
                        {
                            IdProfile = p.Id,
                            IdUser = user.Id,
                            UserName = user.Email,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            EmailConfirmed = true
                        };
                        await userManager.CreateAsync(employee, "Admin@123");
                        await userManager.AddToRoleAsync(employee, p.Name);
                    }
                    else if (user.Name.Contains("Medico"))
                    {
                        var p = profiles.FirstOrDefault(d => d.Name == "medico");
                        Employee employee = new Employee
                        {
                            IdProfile = p.Id,
                            IdUser = user.Id,
                            UserName = user.Email,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            EmailConfirmed = true
                        };

                        await userManager.CreateAsync(employee, "Medico@123");
                        await userManager.AddToRoleAsync(employee, p.Name);

                        if (user.Name.EndsWith("1"))
                        {
                            var area = areas.FirstOrDefault(d => d.Name == "Generalista");
                            await context.employeeAreas.AddAsync(new EmployeeArea
                            {
                                IdArea = area.Id,
                                IdEmployee = employee.Id
                            });
                            await context.SaveChangesAsync();
                        }
                        else if (user.Name.EndsWith("2"))
                        {
                            var area = areas.FirstOrDefault(d => d.Name == "Generalista");
                            await context.employeeAreas.AddAsync(new EmployeeArea
                            {
                                IdArea = area.Id,
                                IdEmployee = employee.Id
                            });
                            await context.SaveChangesAsync();
                        }
                        else if (user.Name.EndsWith("3"))
                        {
                            var area = areas.FirstOrDefault(d => d.Name == "Generalista");
                            await context.employeeAreas.AddAsync(new EmployeeArea
                            {
                                IdArea = area.Id,
                                IdEmployee = employee.Id
                            });
                            await context.SaveChangesAsync();
                        }
                    }
                    else if (user.Name.Contains("Assistente"))
                    {
                        var p = profiles.FirstOrDefault(d => d.Name == "assistente");
                        Employee employee = new Employee()
                        {
                            IdProfile = p.Id,
                            IdUser = user.Id,
                            UserName = user.Email,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            EmailConfirmed = true
                        };
                        await userManager.CreateAsync(employee, "Assistente@123");
                        await userManager.AddToRoleAsync(employee, p.Name);
                    }
                    else if (user.Name.Contains("Recepcionista"))
                    {
                        var p = profiles.FirstOrDefault(d => d.Name == "recepcionista");
                        Employee employee = new Employee()
                        {
                            IdProfile = p.Id,
                            IdUser = user.Id,
                            UserName = user.Email,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            EmailConfirmed = true
                        };
                        await userManager.CreateAsync(employee, "Recepcionista@123");
                        await userManager.AddToRoleAsync(employee, p.Name);                        
                    }
                }
            }
        }
    }
}