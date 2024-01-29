using apiServicio.Business.Contracts;
using apiServicio.Models;
using System.Data.SqlClient;

namespace apiServicio.Business.Clases
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string conect;

        public UsuarioRepository(IConfiguration _IConfiguration)
        {
            conect = _IConfiguration.GetConnectionString("conBase");
        }

        public async Task<Usuario> GetNombreUsuario(string nombreusuario)
        {
            List<string> list = new List<string>();
            Usuario oUsuario = new Usuario();
            using (SqlConnection conn = new SqlConnection(conect))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("select * from trnUsuario where NombreUsuario='"
                    + nombreusuario + "';", conn);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        oUsuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString());
                        oUsuario.NombreUsuario = reader["NombreUsuario"].ToString();
                        oUsuario.Clave = reader["Clave"].ToString();
                        oUsuario.Salt = reader["Salt"].ToString();
                    }
                }
            }
            return oUsuario;
        }
    }
}
