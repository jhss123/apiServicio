namespace apiServicio.Models
{
    public class TrnIngresoDetalle
    {
        public int IdIngresoDetalle { get; set; }
        public int IdInsumo { get; set; }
        public int IdIngreso { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
        public string Lote { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool HabilitarSalida { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string EstadoRegistro { get; set; }
    }
}
