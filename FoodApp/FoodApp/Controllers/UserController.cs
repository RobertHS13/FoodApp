using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using FoodApp.Infraestructure;
using FoodApp.Models;
using FoodApp.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyContext _context;

        public UserController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return UserItem(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Registration([FromBody] User model)
        {
            model.Password = model.Password.HashPassword();

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUser),
                new { id = model.Id },
                UserItem(model));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<User>> Login([FromBody] User model)
        {
            var user = await _context.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            if (VerifyHashedPassword(user.Password, model.Password))
            {
                user.Password = null;
                return CreatedAtAction(
                nameof(GetUser),
                new { id = user.Id },
                UserItem(user));
            }

            return BadRequest();
        }

        private static User UserItem(User user) =>
            new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Type = user.Type,
                Phone = user.Phone,
                Email = user.Email,
                Password = null
            };

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
                return false;

            if (password == null)
                throw new ArgumentNullException("password");

            byte[] src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != 0x31) || (src[0] != 0))
                return false;

            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private static bool ByteArraysEqual(byte[] b0, byte[] b1)
        {
            if (b0 == b1)
            {
                return true;
            }

            if (b0 == null || b1 == null)
            {
                return false;
            }

            if (b0.Length != b1.Length)
            {
                return false;
            }

            for (int i = 0; i < b0.Length; i++)
            {
                if (b0[i] != b1[i])
                {
                    return false;
                }
            }

            return true;
        }


    }
}
