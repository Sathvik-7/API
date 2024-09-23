using API.Filters;
using API.Models.DTO;
using API.Repository.Interface;
using AutoMapper;
using DemoAPIProject.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetWalkData()
        {
            var walksData = await walkRepository.GetWalkAsync();

            //Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDTO>>(walksData));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walksData = await walkRepository.GetWalkByIdAsync(id);

            if (walksData == null)
                return NotFound("Data not found");

            return Ok(mapper.Map<WalkDTO>(walksData));
        }

        [HttpPost]
        [ValidationStateFilter]
        public async Task<IActionResult> Create([FromBody] AddWalksDTO addWalksDTO)
        {
            if (addWalksDTO == null)
                return BadRequest();

            //Mapping DTO to Domain Model
            var walksData = mapper.Map<Walk>(addWalksDTO);
            //Inserting the data 
            await walkRepository.AddWalkAsync(walksData);

            return Ok(mapper.Map<WalkDTO>(walksData));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidationStateFilter]
        public async Task<IActionResult> PutData([FromRoute] Guid id, [FromBody] UpdateWalksDTO upWalksDTO)
        {
            if (upWalksDTO == null)
                return BadRequest();

            //Mapping DTO to Domain Model
            var walksData = mapper.Map<Walk>(upWalksDTO);

            //Inserting the data 
            var r = await walkRepository.UpdateWalkAsync(id, walksData);

            if (r == null)
                return NotFound();

            return Ok(mapper.Map<WalkDTO>(walksData));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var walksData = await walkRepository.DeleteWalkAsync(id);

            if (walksData == 0)
                return NotFound("Record Delete failed");

            return Ok(mapper.Map<WalkDTO>(walksData));
        }

    }
}
