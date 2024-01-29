using apiServicio.Business.Contracts;
using apiServicio.Models;
using apiServicio.Services.Contracts;

namespace apiServicio.Services.Clases
{
    public class TrnIngresoDetalleService : ITrnIngresoDetalleService
    {
        private readonly ItrnIngresoDetalleRepository _ItrnIngresoDetalleRepository;

        public TrnIngresoDetalleService (ItrnIngresoDetalleRepository temp)
        {
            _ItrnIngresoDetalleRepository = temp;
        }

        public Task<List<TrnIngresoDetalle>> GetList()
        {
            return _ItrnIngresoDetalleRepository.GetList();
        }

        public Task<TrnIngresoDetalle> AgregaActualiza (TrnIngresoDetalle l, string t)
        {
            return _ItrnIngresoDetalleRepository.AgregaActualiza(l, t);
        }





        public async Task<bool> EliminaDetalle(int id)
        {
            return await _ItrnIngresoDetalleRepository.EliminaDetalle(id);
        }

        public async Task<TrnIngresoDetalle> GetById(int id)
        {
            return await _ItrnIngresoDetalleRepository.GetByIdDetalle(id);
        }

        Task<TrnIngresoDetalle> ITrnIngresoDetalleService.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
