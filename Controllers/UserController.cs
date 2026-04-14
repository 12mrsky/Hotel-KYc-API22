using Hotel_KYC_Api.Data;
using Hotel_KYC_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Hotel_KYC_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

   
}
}