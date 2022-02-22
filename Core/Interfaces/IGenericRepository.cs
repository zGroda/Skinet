using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    //só pode ser usado por classes que Herdam de BaseEntity
    public interface IGenericRepositor<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}
