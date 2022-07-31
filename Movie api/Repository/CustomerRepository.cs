using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Movie_api.Data;
using Movie_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_api.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _Context;

        public CustomerRepository(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task<int> AddAsync(Customer customer)
        {
            await _Context.Customers.AddAsync(customer);
            await _Context.SaveChangesAsync();
            return customer.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _Context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            EntityEntry entityEntry = _Context.Entry(result);
            entityEntry.State = EntityState.Deleted;
            await _Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() => await _Context.Customers.ToListAsync();

        public async Task<Customer> GetByIdAsync(int id)
        {
            var result = await _Context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task UpdateAsync(int id, Customer customer)
        {
            var result = await _Context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (result != null)
            {
                result.Name = customer.Name;
                result.Address = customer.Address;
                result.Phone = customer.Phone;
                await _Context.SaveChangesAsync();
            }

            //var customers = new Customer()
            //{
            //    Id = id,
            //    Name = customer.Name,
            //    Address = customer.Address,
            //    Phone = customer.Phone
            //};
            //_Context.Customers.Update(customers);
            //await _Context.SaveChangesAsync();
        }
    }
}
