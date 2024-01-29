using apiServicio.Models;
using apiServicio.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace apiServicio.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class trnIngresoDetalleController
    {
        private readonly ITrnIngresoDetalleService _ITrnIngresoDetalleService;
        
        public trnIngresoDetalleController(ITrnIngresoDetalleService temp)
        {
            this._ITrnIngresoDetalleService = temp;
        }
        [HttpGet]
        public async Task<List<TrnIngresoDetalle>> GetList()
        {
            return await _ITrnIngresoDetalleService.GetList();
;       }



        [HttpPost("AgregaActualizaDetalle")]
        public async Task<TrnIngresoDetalle> AgregaActualizaDetalle(
        int IdIngresoDetalle, int IdInsumo, int IdIngreso, int Cantidad,
        decimal PrecioUnitario, decimal SubTotal, string Lote, DateTime FechaVencimiento,
        bool HabilitarSalida, DateTime FechaRegistro, string EstadoRegistro, string t)
        {
            TrnIngresoDetalle l = new TrnIngresoDetalle();
            l.IdIngresoDetalle = IdIngresoDetalle;
            l.IdInsumo = IdInsumo;
            l.IdIngreso = IdIngreso;
            l.Cantidad = Cantidad;
            l.PrecioUnitario = PrecioUnitario;
            l.SubTotal = SubTotal;
            l.Lote = Lote;
            l.FechaVencimiento = FechaVencimiento;
            l.HabilitarSalida = HabilitarSalida;
            l.FechaRegistro = FechaRegistro;
            l.EstadoRegistro = EstadoRegistro;

            return await _ITrnIngresoDetalleService.AgregaActualiza(l, t);
        }


        /*
         [HttpDelete("Elimina")]
        public async Task<bool> Elimina([FromQuery] int id)
        {
            return await _clAlmacenService.Elimina(id);
        }

        [HttpGet("{id}")]
        public async Task<ClAlmacen> GetById(int id)
        {
            return await _clAlmacenService.GetById(id);
        }*/

        [HttpDelete("Elimina")]
        public async Task<bool> Elimina ([FromQuery] int id)
        {
            return await _ITrnIngresoDetalleService.EliminaDetalle(id);

        }

        [HttpGet("{id}")]
        public async Task<TrnIngresoDetalle> GetById(int id)
        {
            return await _ITrnIngresoDetalleService.GetById(id);
        }


    }
}
