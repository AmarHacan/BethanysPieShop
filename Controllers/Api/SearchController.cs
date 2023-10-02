using BethanysPieShop.Models;
using BethanysPieShop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository _pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var a = _pieRepository.AllPies;
            return Ok(a);
        }
        [HttpGet("{id}")]
        public IActionResult GetAll(int id)
        {
            if (_pieRepository.GetPieById(id) == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_pieRepository.GetPieById(id));
            }
        }
        [HttpPost]
        public IActionResult SearchPies([FromBody] string searchQuery)
        {
            IEnumerable<Pie> pies = new List<Pie>();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                pies = _pieRepository.SearchPies(searchQuery);
            }
            return new JsonResult(pies);
        }
    }
}
 