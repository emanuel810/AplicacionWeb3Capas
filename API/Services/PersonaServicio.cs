using API.Clases;
using API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Services
{
    public class PersonaServicio : IPersonaServicio
    {

        public readonly string conexion;

        public PersonaServicio(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("conexion");
        }

        private Persona Mapeo(SqlDataReader reader)
        {
            return new Persona()
            {
                PersonaId = Convert.ToInt32(reader["PersonaId"]),
                Nombre = reader["Nombre"].ToString(),
                Apellido = reader["Apellido"].ToString(),
                Sexo = Convert.ToChar(reader["Sexo"]),
                FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"])
            };
        }


        public async Task Actualizar(Persona persona)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(CatalogoConstantes.SP_ActualizarPersona, sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PersonaId", persona.PersonaId));
                        cmd.Parameters.Add(new SqlParameter("@Nombre", persona.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@Apellido", persona.Apellido));
                        cmd.Parameters.Add(new SqlParameter("@Sexo", persona.Sexo));
                        cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", persona.FechaNacimiento));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error" + ex);
            }
        }

        public async Task Insertar(Persona persona)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(CatalogoConstantes.SP_AgregarPersona, sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Nombre", persona.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@Apellido", persona.Apellido));
                        cmd.Parameters.Add(new SqlParameter("@Sexo", persona.Sexo));
                        cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", persona.FechaNacimiento));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error" + ex);
            }

        }

        public async Task EliminarPorId(int personaId)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(CatalogoConstantes.SP_EliminarPersona, sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PersonaId", personaId));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error"+ex);
            }
        }

        public async Task<List<Persona>> Obtener()
        {
            var response = new List<Persona>();
            try
            {
                using (SqlConnection sql = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(CatalogoConstantes.SP_ObtenerPersona, sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(Mapeo(reader));
                            }
                        }
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error"+ex);
            }
            return response;
        }

        public async Task<Persona> ObtenerPorId(int personaId)
        {
            Persona response = new Persona();
            try
            {
                using (SqlConnection sql = new SqlConnection(conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(CatalogoConstantes.SP_ObtenerPersonaId, sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PersonaId", personaId));
                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response = Mapeo(reader);
                            }
                        }
                        return response;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error" + ex);
            }
            return response;
        }
    }
}
