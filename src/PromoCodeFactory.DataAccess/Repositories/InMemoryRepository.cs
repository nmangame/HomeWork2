using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using PromoCodeFactory.Core.Domain.Administration;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T: BaseEntity
    {
        protected IList<T> _data { get; set; }

        public InMemoryRepository(IList<T> data)
        {
            _data = data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_data.AsEnumerable());
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            await IsExits(id);

            return await Task.Run(() => _data.FirstOrDefault(x => x.Id == id));
        }

        public async Task Add(T entity)
        {
            await Task.Run(() => { _data.Add(entity); });
        }

        public async Task Delete(Guid id)
        {
            await IsExits(id);

            await Task.Run(() =>
            {
                var item = _data.FirstOrDefault(t => t.Id == id);
                _data.Remove(item);
            });
        }

        public async Task Update(T entity)
        {
            await IsExits(entity.Id);

            await Task.Run(() =>
            {
                var index = _data.IndexOf(_data.FirstOrDefault(t => t.Id == entity.Id));
                _data[index] = entity;
            });
        }

        public async Task IsExits(Guid id)
        {
            await Task.Run(() =>
            {
                var item = _data.FirstOrDefault(t => t.Id == id);
                if (item == null)
                {
                    throw new KeyNotFoundException("Запись не найдена");
                }
            });
        }
    }
}