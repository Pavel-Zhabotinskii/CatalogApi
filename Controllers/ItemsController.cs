﻿using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : Controller
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        //Get /items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
            return items;
        }

        //Get /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id) 
        {
            var item = await repository.GetItemAsync(id);

            if(item == null) return NotFound();

            return item.AsDto();
        }

        //Post /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto) 
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTime.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new {id = item.Id}, item.AsDto());
        }

        //Put /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateDto itemDto) 
        {
            var existingItem = await repository.GetItemAsync(id);

            if(existingItem is null) return NotFound();

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
            }; 

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        //Delete /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id) 
        {
            var existingItem = await repository.GetItemAsync(id);

            if(existingItem is null) return NotFound();

            await repository.DeleteItemAsync(id);

            return NoContent();
        }

    }
}
