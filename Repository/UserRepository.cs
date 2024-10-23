using Azure;
using InventoryAPI.DataContext;
using InventoryAPI.Helper;
using InventoryAPI.Interface;
using InventoryAPI.Model.Domain;
using InventoryAPI.Model.DTOs;
using InventoryAPI.Service;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryAPI.Repository
{
    public class UserRepository : IUserService       
    {
        private readonly ApplicationContext context;
        private readonly IEmailService emailService;
        private readonly BaseSetup setup;
        public UserRepository(IEmailService emailService,IOptions<BaseSetup> setup ,ApplicationContext context)
        {
            this.context = context;
            this.emailService = emailService;
            this.setup = setup.Value;
        }
        
        public async Task<ResponseModel<string>> Login(Login login)
        {
            var response = new ResponseModel<string>();
            try
            {
                var user = await context.Users.Include(x => x.Roles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Email == login.Email);
                var password = Utilities.DecryptPassword(login.PasswordHash, user.PasswordHash);
                if (user == null || !password ) 
                {
                    response = response.FailedResult("your email or password is not correct");
                }
                else if (!user.IsVerified)
                {
                    response = response.FailedResult("Account not verified");
                }
                else
                {
                    var userRole = user.Roles.Select(x => x.Role.Name).ToList();
                    //claims
                    var claims = new List<Claim>
                    {
                        new("Email", user.Email),
                        new("VerificationStatus", user.IsVerified.ToString())
                    };
                    foreach(var role in userRole)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setup.SecretKey));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        setup.Issuer,
                        signingCredentials: credentials,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(120)
                        );
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                    response = response.Successful(jwtToken);
                    
                    await context.SaveChangesAsync();
                
                }
                return response;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseModel<string>> RegisterAdmin(Register register)
        {
            var response = new ResponseModel<string>();
            try
            {
                var confirmIfEmailExist = await context.Users.AnyAsync(x => x.Email == register.Email);
                if (confirmIfEmailExist)
                {
                    response = response.FailedResult("email already exist, try and login");
                }
                else if (register.Password != register.ConfirmPassword) 
                {
                    response = response.FailedResult("your password deos not match ");
                }
                else              
                {
                    var newRegister = new User
                    {
                        Email = register.Email,
                        Name = register.Name,
                        Password = register.Password,
                        ConfirmPassword = register.ConfirmPassword
                    };
                    await context.Users.AddAsync(newRegister);
                    await context.SaveChangesAsync();
                    
                    //update admin role to the registration
                    var role = await context.Roles.FirstOrDefaultAsync( x => x.Id == 1);
                    var userRole = new UserRole
                    {
                        RoleId = role.Id,
                        Role = role,
                        UserId = newRegister.Id,
                        User = newRegister                     
                    };
                     newRegister.Roles.Add(userRole);
                    await context.SaveChangesAsync();
                    
                }
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        public async Task<ResponseModel<string>> RegisterCustomer(Register register)
        {
            var response = new ResponseModel<string>(); 
            try
            {
                var newCustomer = await context.Users.AnyAsync(X => X.Email == register.Email);
                if(newCustomer)
                {
                    response = response.FailedResult("Email already exist, try login");
                }
                else if(register.Password != register.ConfirmPassword )
                {
                    response = response.FailedResult("your password is incorrect");
                }
                else
                {
                    var customerRegistration = new User
                    {
                        Email = register.Email,
                        Password = register.Password,
                        ConfirmPassword = register.ConfirmPassword
                    };
                    await context.AddAsync(customerRegistration);
                    context.SaveChanges();                   
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        public async Task<ResponseModel<string>> RegisterStaff(Register register)
        {
            var response = new ResponseModel<string>();
            try
            {
                var staff = await context.Users.AnyAsync(x => x.Email == register.Email);
                if (staff)
                {
                    response = response.FailedResult("this email alreayd exist, try password");
                }
                else if (register.Password != register.ConfirmPassword)
                {
                    response = response.FailedResult("invalid password, try and register");
                }
                else
                {
                    var newUser = new User
                    {
                        Name = register.Email,
                        Email = register.Email,
                        Password = register.Password,
                        ConfirmPassword = register.ConfirmPassword
                    };
                    await context.AddAsync(newUser);
                    context.SaveChanges();


                    var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == 2);
                    var userRole = new UserRole
                    {
                        RoleId = role.Id,
                        Role = role,
                        UserId = newUser.Id,
                        User = newUser
                    };
                    newUser.Roles.Add(userRole);
                    await context.SaveChangesAsync();
                    
                }
            }
            catch( Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
