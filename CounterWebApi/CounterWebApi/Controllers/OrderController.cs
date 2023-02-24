using CounterWebApi.Data;
using CounterWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading;

namespace CounterWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private readonly CounterDbContext db;

        public OrderController(CounterDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.db.Orders.ToArray());
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateWithLock(CreateOrderModel model)
        //{
        //    try
        //    {
        //        await _semaphore.WaitAsync();

        //        int maxCode = 0;
        //        if (this.db.Orders.Any(o => o.Customer == model.Customer))
        //        {
        //            maxCode = this.db.Orders.Where(o => o.Customer == model.Customer).Max(o => o.Code);
        //        }

        //        maxCode++;

        //        var order = new Order() { Code = maxCode, Customer = model.Customer, Name = model.Name, CreatedOn = DateTime.Now };
        //        db.Orders.Add(order);
        //        await db.SaveChangesAsync();
        //        return Ok(order);
        //    }
        //    catch(Exception ex)
        //    {
        //        string error = ex.Message;
        //    }
        //    finally
        //    {
        //        _semaphore.Release();
        //    }

        //    return NotFound(model.Customer);

        //}

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderModel model)
        {
            try
            {
                var order = new Order() { Customer = model.Customer, Name = model.Name, CreatedOn = DateTime.Now };
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return Ok(order);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return NotFound(model.Customer);

        }
    }
}
