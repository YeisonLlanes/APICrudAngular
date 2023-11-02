using ApiCrudAngular.Models;
using Azure.Core;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiCrudAngular.Services
{
    public class DepartamentoServices : IDepartamento
    {
        private readonly string _conexion = "";//Creao Variable de conexion BD

        public DepartamentoServices(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("ConexionBD");//1 - Obtengo Conexion BD.
        }

        public async Task<List<Departamento>> GetDepartamentos()
        {
            List<Departamento> _lista = new List<Departamento>();
            string _query = "SELECT * FROM Departamento";//2 - Creo el query, consulta que voy a ejecutar.
            
            using (SqlConnection conexion = new SqlConnection(_conexion))//3 - Creo mi conexion a mi BD
            {
                SqlCommand cmd = new SqlCommand(_query, conexion); //4 - Creo la instruccion SQL que voy a ejecutar en la BD
                try
                {
                    if(conexion.State == System.Data.ConnectionState.Closed )//5 - Valido el estado de la conexion a mi BD
                    {
                        conexion.Open();//6 - Abro mi conexion a la BD
                    }

                    using (var dataReader = await cmd.ExecuteReaderAsync())//7 - Ejecuto el comando sobre mi BD
                    {
                        while (await dataReader.ReadAsync()) 
                        {
                            _lista.Add(new Departamento
                            {
                                idDepartamento = Convert.ToInt32(dataReader["idDepartamento"]),
                                descripcion = dataReader["descripcion"].ToString()
                            });//8 - Manipulo los datos segun la necesidad
                        }
                        conexion.Close();//9 - Cierro mi conexion a la BD
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return _lista;//10 - Retorno el resultado.

        }

        public async Task<Departamento> GetDepartamento(int idDepartamento)
        {
            Departamento _departamento = new Departamento();
            string _query = "SELECT * FROM Departamento WHERE idDepartamento = @idDpto";

            using (SqlConnection conexion = new SqlConnection(_conexion))
            {
                SqlCommand cmd = new SqlCommand( _query, conexion);
                cmd.Parameters.AddWithValue("@idDpto", idDepartamento);

                try
                {
                    if(conexion.State == System.Data.ConnectionState.Closed )
                        conexion.Open() ;

                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (await dataReader.ReadAsync())
                        {
                            _departamento.idDepartamento = Convert.ToInt32(dataReader["idDepartamento"]);
                            _departamento.descripcion = dataReader["descripcion"].ToString();
                        }
                        conexion.Close() ;
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return _departamento;

        }

        public async Task<Departamento> CreateDpto(Departamento departamento)
        {
            Departamento _departamento = new Departamento();
            string _query = "INSERT INTO Departamento (descripcion) VALUES (@descripcion)";

            using (SqlConnection conexion = new SqlConnection(_conexion))
            {
                SqlCommand cmd = new SqlCommand(_query, conexion);
                cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 50).Value = departamento.descripcion;

                try
                {
                    if(conexion.State == System.Data.ConnectionState.Closed )
                        conexion.Open() ;

                    await cmd.ExecuteNonQueryAsync();

                    conexion.Close();

                }
                catch (Exception ex)
                {

                }
            }
            return _departamento;
        }

        public async Task<Departamento> UpdateDpto(int idDepartamento, Departamento departamento)
        {
            Departamento _dpto = new Departamento();
            string _query = "UPDATE Departamento SET descripcion = @descripcion WHERE idDepartamento = @idDepartamento ";

            //Valido que exista el registro a actualizar
            var dpto = await GetDepartamento(idDepartamento);

            if (dpto.idDepartamento == 0 || dpto.descripcion == null)
            {
                return dpto;
            }

            using (SqlConnection conexion = new SqlConnection(_conexion))
            {
                SqlCommand cmd = new SqlCommand( _query, conexion);
                cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 50).Value = departamento.descripcion;
                cmd.Parameters.AddWithValue("@idDepartamento", idDepartamento);

                try
                {
                    if (conexion.State == System.Data.ConnectionState.Closed)
                        conexion.Open();

                    await cmd.ExecuteNonQueryAsync();

                    conexion.Close();
                }
                catch (Exception ex)
                {

                }

            }
            return _dpto;
        }

        public async Task<bool> DeleteDpto(int idDepartamento)
        {
            string _query = "DELETE FROM Departamento WHERE idDepartamento = @idDepartamento";

            //Valido que exista el registro a actualizar
            var dpto = await GetDepartamento(idDepartamento);

            if (dpto.idDepartamento == 0 || dpto.descripcion == null)
            {
                return false;
            }

            using (SqlConnection conexion = new SqlConnection(_conexion))
            {
                SqlCommand cmd = new SqlCommand(_query, conexion);
                cmd.Parameters.AddWithValue("@idDepartamento", idDepartamento);

                try
                {
                    if(conexion.State == System.Data.ConnectionState.Closed)
                        conexion.Open();

                    await cmd.ExecuteNonQueryAsync();

                    conexion.Close();
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
        }

    }
}
