using apiServicio.Models;
using apiServicio.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace apiServicio.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RolController
    {
        private readonly IRolService _IRolService;
        public RolController(IRolService temp)
        {
            this._IRolService = temp;
        }
        [HttpGet]
        public async Task<List<Rol>> GetList()
        {
            return await _IRolService.GetList();
        }
        [HttpPost("AgregaActualiza")]
        public async Task<Rol> AgregaActualiza(
        int id,string NombreRol, string t)
        {
            Rol l = new Rol();
            l.id = id;
            l.NombreRol = NombreRol;
            return await _IRolService.AgregaActualiza(l, t);

        }

    }
}
