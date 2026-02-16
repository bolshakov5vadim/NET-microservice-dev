using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using NetApp.Entities; // класс, описывающий сущность БД
using NetApp.Services; // класс MyService со всеми методами


namespace NetApp.Controllers // namespace указывает на папку проекта
{
    [Route("api/[controller]")]
    public class MyController : Controller
    {
        // Объект со всеми методами
        private readonly IMyService _myService; 
 

        // Конструктор
        // Интерфейс позволяет переключаться между разными сервисами. 
        // Подключаем другой Service без переписывания кода.
        public MyController(IMyService myService)
        {
            _myService = myService;
        }


        // Аннотация [HttpGet] для обработки GET
        // Аннотация [FromQuery] для парсинга запроса
        // Аннотация [Route("{id}")] задает URL


        [HttpGet]
        public async Task<IActionResult> GetEntities([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            var entities = await _myService.ReadMany();
            return new OkObjectResult(entities);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ReadDetails(int id)
        {
            var entities = await _myService.ReadById(id);
            if (entities == null)
                return StatusCode((int) HttpStatusCode.NotFound, new JsonResult(new
                {
                    Success = false,
                    FullMessages = new string[]
                    {
                        "Not Found"
                    }
                }));
            else
                return Json(entities);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MyEntity entity)
        {
            await _myService.Create(entity);
            var response = new ObjectResult(entity);
            response.StatusCode = (int) HttpStatusCode.Created;
            return response;
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MyEntity entity)
        {
            var response = await _myService.ReadById(id);
            if (response == null)
                return NotFound(Json(new
                {
                    Success = false,
                    FullMessages = new string[]
                    {
                        "Not Found"
                    }
                }));
            else
                return new OkObjectResult(await _myService.Update(response, entity));
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _myService.ReadById(id);
            if (response == null)
                return new NotFoundObjectResult(Json(new
                {
                    Success = false,
                    FullMessages = new string[]
                    {
                        "Not Found"
                    }
                }));
            else
            {
                EntityEntry<MyEntity> result = _myService.Delete(id);
            }

            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await _myService.DeleteAll();
            return new NoContentResult();
        }
    }
}