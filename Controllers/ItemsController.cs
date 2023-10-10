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
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }

        //Get /items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id) 
        {
            var item = repository.GetItem(id);

            if(item == null) return NotFound();

            return item.AsDto();
        }

        //Post /items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto) 
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTime.UtcNow
            };

            repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());
        }

        //Put /items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateDto itemDto) 
        {
            var existingItem = repository.GetItem(id);

            if(existingItem is null) return NotFound();

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
            }; 

            repository.UpdateItem(updatedItem);

            return NoContent();
        }

        //Delete /items/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id) 
        {
            var existingItem = repository.GetItem(id);

            if(existingItem is null) return NotFound();

            repository.DeleteItem(id);

            return NoContent();
        }

    }
}
