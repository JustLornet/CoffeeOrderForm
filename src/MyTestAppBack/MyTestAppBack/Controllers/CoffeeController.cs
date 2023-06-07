using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using MyTestAppBack.Aggregates.Dto;
using MyTestAppBack.DataAccess;
using MyTestAppBack.Domain.Aggregates;
using IoFile = System.IO.File;

namespace MyTestAppBack.Controllers
{
    public class CoffeeController : Controller
    {
        private Db _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CoffeeController(Db db, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = db;
            _hostingEnvironment = hostingEnvironment;
        }

        // получение изображения
        [HttpGet]
        public IActionResult GetImage(string path)
        {
            var currentDirectory = _hostingEnvironment.ContentRootPath;
            var requestedPath = Path.Combine(currentDirectory, path);

            if (!IoFile.Exists(requestedPath))
            {
                // в случае, если изображение не найдено, выдается заглушка
                requestedPath = Path.Combine(currentDirectory, "Images", "CoffeeTypes", "Заглушка", "Composition.jpg");
            }

            return PhysicalFile(requestedPath, contentType: "image/png");
        }

        // получение стандартных словарей
        [HttpGet]
        public async Task<ActionResult<SelectionsDto>> GetSelections()
        {
            try
            {
                var syrupList = await _dbContext.Syrups.ToListAsync();
                var coffeeTypesList = await _dbContext.CoffeeTypes.ToListAsync();
                //// получение добавочных ингридиентов
                var ingredientList = await _dbContext.Ingredients.Where((ingredient) => ingredient.IsOptional).Include(p => p.IngredientUnit).ToListAsync();

                var dto = new SelectionsDto { CoffeeTypes = coffeeTypesList, Syrups = syrupList, Ingredients = ingredientList };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // получение состава текущего типа кофе
        [HttpGet]
        public async Task<IActionResult> GetComposition(long coffeeId)
        {
            try
            {
                var composition = await _dbContext.StandartCompositions.Where(p => p.CoffeeTypeId == coffeeId).Include(p => p.Ingredient.IngredientUnit).Select((ingredient) =>
                    new { ingredient.Ingredient, ingredient.Value }).ToListAsync();

                return Ok(composition);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // получение всех заказов
        [HttpGet]
        public async Task<IActionResult> GetOrderHistory()
        {
            try
            {
                var orderList = await _dbContext.CoffeeOrders.Include(p => p.CoffeeType).Include(p => p.CustomCompositions).Include(p => p.Syrup).ToListAsync();

                return Ok(orderList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // сохранение заказа в бд
        [HttpPost]
        public async Task<IActionResult> SaveOrder([FromBody] CoffeeOrder newOrder)
        {
            try
            {
                var orderToDb = new CoffeeOrder();

                // проверка, что такого состава ещё нет и добавление его или периспользование, если таковой имеется
                foreach (var compos in newOrder.CustomCompositions)
                {
                    if (compos.Value > 0)
                    {
                        var existedComposition = await _dbContext.CustomCompositions.FirstOrDefaultAsync(item => item.IngredientId == compos.IngredientId &&
                        item.Value == compos.Value);
                        if (existedComposition == null)
                        {
                            _dbContext.CustomCompositions.Add(compos);
                            _dbContext.SaveChanges();
                        }

                        orderToDb.CustomCompositions.Add(existedComposition ?? compos);
                    }
                }

                orderToDb.CoffeeTypeId = newOrder.CoffeeType.Id;
                orderToDb.SyrupId = newOrder.Syrup?.Id;
                orderToDb.Comments = newOrder.Comments;
                orderToDb.CustomerName = newOrder.CustomerName;
                orderToDb.OrderExecutionDateTime = newOrder.OrderExecutionDateTime;
                orderToDb.OrderCreationDateTime = DateTime.Now;

                _dbContext.CoffeeOrders.Add(orderToDb);
                _dbContext.SaveChanges();

                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}