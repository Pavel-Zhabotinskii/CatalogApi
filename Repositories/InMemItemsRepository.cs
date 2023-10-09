using Catalog.Entities;

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

        public IEnumerable<Item> GetItems() => items;

        public Item GetItem(Guid id) => items.Where(item => item.Id == id).SingleOrDefault();
    }
}
