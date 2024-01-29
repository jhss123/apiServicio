using apiServicio.Models;

namespace apiServicio.Business.Contracts
{
    public interface ItrnIngresoDetalleRepository
    {
        Task<TrnIngresoDetalle> AgregaActualiza(TrnIngresoDetalle l, string t);
        Task<List<TrnIngresoDetalle>> GetList();
        Task<bool> EliminaDetalle(int id);
        Task<TrnIngresoDetalle> GetByIdDetalle(int id);
    }
}
