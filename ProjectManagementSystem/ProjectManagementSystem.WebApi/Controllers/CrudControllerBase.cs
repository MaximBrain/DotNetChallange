using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.WebApi.Models;
using ProjectManagementSystem.WebApi.Services;

namespace ProjectManagementSystem.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class CrudControllerBase<TItemModel> : ControllerBase where TItemModel: ItemModelBase
    {
        private readonly IItemService<TItemModel> _itemService;

        protected CrudControllerBase(IItemService<TItemModel> itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public ActionResult<List<TItemModel>> GetAll()
        {
            return  _itemService.GetAllItems().ToList();
        }


        [HttpGet("{id}")]
        public ActionResult<TItemModel> GetItem(int id)
        {
            var item = _itemService.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, TItemModel item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _itemService.UpdateItem(item);

            return Ok();
        }

        [HttpPost]
        public ActionResult<TItemModel> Post(TItemModel item)
        {
            _itemService.CreateItem(item);

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public ActionResult<TItemModel> Delete(int id)
        {
            var item = _itemService.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }
            _itemService.DeleteItem(item);
            return item;
        }
    }
}