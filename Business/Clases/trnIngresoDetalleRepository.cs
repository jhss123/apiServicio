using apiServicio.Business.Contracts;
using apiServicio.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace apiServicio.Business.Clases
{
    public class trnIngresoDetalleRepository : ItrnIngresoDetalleRepository
    {
        private readonly string conect;

        public trnIngresoDetalleRepository(IConfiguration _con)
        {
            conect = _con.GetConnectionString("conBase");
        }
        public async Task<List<TrnIngresoDetalle>> GetList()
        {
            List<TrnIngresoDetalle> list = new List<TrnIngresoDetalle>();
            TrnIngresoDetalle l;
            
            using (SqlConnection con = new SqlConnection(conect))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("select * from TrnIngresoDetalle;", con);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        l = new TrnIngresoDetalle();
                        l.IdIngresoDetalle = Convert.ToInt32(reader["IdIngresoDetalle"]);
                        l.IdInsumo = Convert.ToInt32(reader["IdInsumo"]);
                        l.IdIngreso = Convert.ToInt32(reader["IdIngreso"]);
                        l.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                        l.PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]);
                        l.SubTotal = Convert.ToDecimal(reader["SubTotal"]);
                        l.Lote = Convert.ToString(reader["Lote"]);
                        l.FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]);
                        l.HabilitarSalida = Convert.ToBoolean(reader["HabilitarSalida"]);
                        l.FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]);
                        l.EstadoRegistro = Convert.ToString(reader["EstadoRegistro"]);

                        list.Add(l);
                    }
                }
            }

            return list;
        }

        public async Task<TrnIngresoDetalle> AgregaActualiza(TrnIngresoDetalle l, string t)
        {
            using (SqlConnection connection = new SqlConnection(conect))
            {
                string query = "";

                if (t == "c")
                    query = "SET @I = (SELECT ISNULL(MAX(IdIngresoDetalle), 0) + 1 FROM trnIngresoDetalle);" +
                            "INSERT INTO trnIngresoDetalle(IdInsumo, IdIngreso, Cantidad, PrecioUnitario, SubTotal, Lote, FechaVencimiento, HabilitarSalida, FechaRegistro, EstadoRegistro)" +
                            "VALUES (@IdInsumo, @IdIngreso, @Cantidad, @PrecioUnitario, @SubTotal, @Lote, @FechaVencimiento, @HabilitarSalida, @FechaRegistro, @EstadoRegistro);";

                if (t == "u")
                    query = "UPDATE trnIngresoDetalle SET IdInsumo = @IdInsumo, IdIngreso = @IdIngreso, Cantidad = @Cantidad, PrecioUnitario = @PrecioUnitario, SubTotal = @SubTotal, Lote = @Lote, FechaVencimiento = @FechaVencimiento, HabilitarSalida = @HabilitarSalida, FechaRegistro = @FechaRegistro, EstadoRegistro = @EstadoRegistro" +
                            " WHERE IdIngresoDetalle = @IdIngresoDetalle;";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlParameter result = new SqlParameter("@I", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(result);

                    cmd.Parameters.AddWithValue("@IdIngresoDetalle", l.IdIngresoDetalle);
                    cmd.Parameters.AddWithValue("@IdInsumo", l.IdInsumo);
                    cmd.Parameters.AddWithValue("@IdIngreso", l.IdIngreso);
                    cmd.Parameters.AddWithValue("@Cantidad", l.Cantidad);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", l.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@SubTotal", l.SubTotal);
                    cmd.Parameters.AddWithValue("@Lote", l.Lote);
                    cmd.Parameters.AddWithValue("@FechaVencimiento", l.FechaVencimiento);
                    cmd.Parameters.AddWithValue("@HabilitarSalida", l.HabilitarSalida);
                    cmd.Parameters.AddWithValue("@FechaRegistro", l.FechaRegistro);
                    cmd.Parameters.AddWithValue("@EstadoRegistro", l.EstadoRegistro);

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    if (t == "c")
                        l.IdIngresoDetalle = Convert.ToInt32(result.Value);
                }
            }

            return l;
        }




        public async Task<bool> EliminaDetalle(int id)
        {
            using (SqlConnection con = new SqlConnection(conect))
            {
                string query = "DELETE FROM trnIngresoDetalle WHERE IdIngresoDetalle = @IdIngresoDetalle;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdIngresoDetalle", id);

                    await con.OpenAsync();
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }

        public async Task<TrnIngresoDetalle> GetByIdDetalle(int id)
        {
            TrnIngresoDetalle detalle = null;
            using (SqlConnection con = new SqlConnection(conect))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM trnIngresoDetalle WHERE IdIngresoDetalle = @IdIngresoDetalle;", con);
                cmd.Parameters.AddWithValue("@IdIngresoDetalle", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        detalle = new TrnIngresoDetalle
                        {
                            IdIngresoDetalle = Convert.ToInt32(reader["IdIngresoDetalle"]),
                            IdInsumo = Convert.ToInt32(reader["IdInsumo"]),
                            IdIngreso = Convert.ToInt32(reader["IdIngreso"]),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                            SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                            Lote = Convert.ToString(reader["Lote"]),
                            FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]),
                            HabilitarSalida = Convert.ToBoolean(reader["HabilitarSalida"]),
                            FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                            EstadoRegistro = Convert.ToString(reader["EstadoRegistro"])
                        };
                    }
                }
            }

            return detalle;
        }





    }
}
