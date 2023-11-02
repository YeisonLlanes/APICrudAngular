using ApiCrudAngular.Models;

namespace ApiCrudAngular.Services
{
    public interface IDepartamento
    {
        Task<List<Departamento>> GetDepartamentos();

        Task<Departamento> GetDepartamento(int idDepartamento);

        Task<Departamento> CreateDpto(Departamento departamento);

        Task<Departamento> UpdateDpto(int idDepartamento, Departamento departamento);

        Task<bool> DeleteDpto(int idDepartamento);
    }
}
