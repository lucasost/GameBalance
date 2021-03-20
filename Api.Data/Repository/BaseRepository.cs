using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly GameContext _context;
        private readonly DbSet<T> _dataset;
        public BaseRepository(GameContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            if (result == null)
            {
                return false;
            }

            _dataset.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> InsertAsync(T item)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }

            _dataset.Add(item);

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<T> SelectAsync(Guid id)
        {
            return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            return await _dataset.ToListAsync();
        }

        public async Task<T> UpdateAsync(T item)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
            if (result == null)
            {
                return null;
            }

            _context.Entry(result).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
