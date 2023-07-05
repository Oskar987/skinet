using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity<int>
    {
        Task<T> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> T);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> T);
    }
}