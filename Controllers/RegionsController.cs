using API.Models.DTO;
using DemoAPIProject.DataDbContext;
using DemoAPIProject.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalksDbContext _context;

        public RegionsController(WalksDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Region> regions = await _context.Regions.ToListAsync();

            #region Mapping Domain Models to DTO
            var regionDTO = new List<RegionDTO>();

            foreach (var region in regions)
            {
                regionDTO.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }
            #endregion
            return Ok(regionDTO);
            //return Ok(regions);
        }

        [HttpGet]
        [Route("{id:guid}")]//Route Constraints
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _context.Regions.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (region == null)
                return NotFound();

            #region Mapping Domain Model to RegionDTO
            var regionDTO = new RegionDTO()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };
            #endregion

            return Ok(regionDTO);
            //return Ok(region);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRequestDTO addRequestDTO)
        {
            #region Mapping data from DTO to Model
            var regions = new Region()
            {
                Name = addRequestDTO.Name,
                Code = addRequestDTO.Code,
                RegionImageUrl = addRequestDTO.RegionImageUrl
            };
            #endregion

            //Add to Domain Model
            await _context.Regions.AddAsync(regions);
            await _context.SaveChangesAsync();

            #region Storing the deleted data into DTO and sending it to end user
            var regionDTO = new RegionDTO()
            {
                Id = regions.Id,
                Name = regions.Name,
                Code = regions.Code,
                RegionImageUrl = regions.RegionImageUrl,
            };
            #endregion
            //return Ok();
            return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> PutData([FromRoute] Guid id, [FromBody] UpdateReqDTO updateReqDTO)
        {
            var regionData = await _context.Regions.Where(r => r.Id == id).FirstOrDefaultAsync();

            if (regionData == null)
                return NotFound();

            #region updating the data based on id from route
            regionData.Name = updateReqDTO.Name;
            regionData.Code = updateReqDTO.Code;
            regionData.RegionImageUrl = updateReqDTO.RegionImageUrl;
            #endregion

            await _context.SaveChangesAsync();

            #region Storing the deleted data into DTO and sending it to end user
            var regionDTO = new RegionDTO()
            {
                Id = regionData.Id,
                Name = regionData.Name,
                Code = regionData.Code,
                RegionImageUrl = regionData.RegionImageUrl,
            };
            #endregion
            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteByID([FromRoute] Guid id)
        {
            var regions = await _context.Regions.Where(r => r.Id == id).FirstOrDefaultAsync();

            if (regions == null)
                return NotFound();

            _context.Regions.Remove(regions);
            _context.SaveChanges();

            #region Storing the deleted data into DTO and sending it to end user
            var regionsDTO = new RegionDTO()
            {
                Id = regions.Id,
                Name = regions.Name,
                Code = regions.Code,
                RegionImageUrl = regions.RegionImageUrl
            };
            #endregion

            return Ok(regionsDTO);
        }
    }
}
