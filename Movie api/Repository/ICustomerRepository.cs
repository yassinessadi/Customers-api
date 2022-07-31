using Movie_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_api.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<int> AddAsync(Customer customer);
        Task UpdateAsync(int id,Customer customer);
        Task DeleteAsync(int id);
    }
}
