using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KooliProjekt.Controllers
{
    [Route("api/Panels")]
    [ApiController]
    public class PanelApiController : ControllerBase
    {
        private readonly IPanelsService _panelsService;

        public PanelApiController(IPanelsService panelsService)
        {
            _panelsService = panelsService;
        }


        // GET: api/<PanelApiController>
        [HttpGet]
        public async Task <IEnumerable<Panel>> Get()
        {
            var result = await _panelsService.List(1, 10000);
            return result.Results;
        }

        // GET api/<PanelApiController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var list = await _panelsService.Get(id);
            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // POST api/<PanelApiController>
        [HttpPost]
        public async Task<object> Post([FromBody] Panel list)
        {
            await _panelsService.Save(list);

            return Ok(list);
        }

        // PUT api/<PanelApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Panel list)
        {
            if (id != list.Id) {
                return BadRequest();
            }

            await _panelsService.Save(list);
            return Ok();

        }

        // DELETE api/<PanelApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var list = await (_panelsService.Get(id));
            if (list == null)
            {
                return NotFound();
            }

            await _panelsService.Delete(id);
            return Ok();
        }
    }
}
