using apiServicio.Business.Contracts;
using apiServicio.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace apiServicio.Business.Clases
{
    public class RolRepository : IRolRepository
    {
        private readonly string conect;
        public RolRepository(IConfiguration _con)
        {
            conect = _con.GetConnectionString("conBase");
        }

        public async Task<List<Rol>> GetList()
        {
            List<Rol> list = new List<Rol>();
            Rol l;
            using (SqlConnection con = new SqlConnection(conect))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("select * from Rol;", con);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        l = new Rol();
                        l.id = Convert.ToInt32(reader["id"]);
                        l.NombreRol = Convert.ToString(reader["NombreRol"]);
                        list.Add(l);
                    }
                }
            }

            return list;
        }

        public async Task<Rol> AgregaActualiza(Rol l, string t)
        {
            using (SqlConnection con = new SqlConnection(conect))
            {
                string cadena = "";
                if (t == "c")
                    cadena = "set @I=(select isnull(max(id),0)+1 from Rol" +
                    "insert into Rol(NombreRol) values (@NombreRol)";

                if (t == "u")
                    cadena = "update Rol set NombreRol=@NombreRol where id=@Id; ";

                using (SqlCommand cmd = new SqlCommand(cadena, con))
                {
                    SqlParameter Result = new SqlParameter("@I", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(Result);
                    cmd.Parameters.AddWithValue("@Id", l.id);
                    cmd.Parameters.AddWithValue("@NombreRol", l.NombreRol);
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    if (t == "c")
                        l.id = int.Parse(Result.Value.ToString());
                }
            }
            return l;
        }






    }
}
