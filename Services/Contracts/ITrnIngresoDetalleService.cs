using apiServicio.Models;

namespace apiServicio.Services.Contracts
{
    public interface ITrnIngresoDetalleService
    {
        Task<List<TrnIngresoDetalle>> GetList();
        Task<TrnIngresoDetalle> AgregaActualiza(TrnIngresoDetalle l, string t);

        Task<bool> EliminaDetalle(int id);
        Task<TrnIngresoDetalle> GetById(int id);


    }
}
