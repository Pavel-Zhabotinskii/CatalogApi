﻿using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTime.Now },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTime.Now },
            new Item { Id = Guid.NewGuid(), Name = "Bronze", Price = 18, CreatedDate = DateTime.Now }
        };

        public async Task<IEnumerable<Item>> GetItemsAsync() => await Task.FromResult(items);

        public async Task<Item> GetItemAsync(Guid id) => await Task.FromResult(items.Where(item => item.Id == id).SingleOrDefault());

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item; 
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            items.RemoveAt(items.FindIndex(existingItem => existingItem.Id == id));
            await Task.CompletedTask;
        }
    }
}
