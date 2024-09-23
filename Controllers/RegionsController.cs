using API.Models.DTO;
using API.Repository.Interface;
using AutoMapper;
using DemoAPIProject.DataDbContext;
using DemoAPIProject.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        
        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Reposiory Layer - Dependency Injection
            List<Region> regions = await regionRepository.GetAllData();//await _context.Regions.ToListAsync();

            #region Traditional Approach - Mapping Domain Models to DTO
            //var regionDTO = new List<RegionDTO>();
            //foreach (var region in regions)
            //{
            //    regionDTO.Add(new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });
            //}
            #endregion

            // AutoMapper source = regions ; destination = RegionDTO
            var regionDTO = mapper.Map<List<RegionDTO>>(regions);
            
            return Ok(regionDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]//Route Constraints
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Reposiory Layer - Dependency Injection
            var region = await regionRepository.GetById(id);//_context.Regions.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (region == null)
                return NotFound();

            //AutoMapper source = region ; destination = RegionDTO
            var regionDTO = mapper.Map<RegionDTO>(region);
            
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRequestDTO addRequestDTO)
        {
            if (ModelState.IsValid)
            {
                #region Mapping data from DTO to Model - Outdated
                //var regions = new Region()
                //{
                //    Name = addRequestDTO.Name,
                //    Code = addRequestDTO.Code,
                //    RegionImageUrl = addRequestDTO.RegionImageUrl
                //};
                #endregion

                //AutoMapper Source = addRequestDTO ; Destination = Region
                var regions = mapper.Map<Region>(addRequestDTO);
                
                //Reposiory Layer - Dependency Injection
                int r = await regionRepository.CreateRegions(regions);
                if (r == 0)
                    return BadRequest("Record insert failed");

                #region Storing the data into DTO and sending it to end user
                //var regionDTO = new RegionDTO()
                //{
                //    Id = regions.Id,
                //    Name = regions.Name,
                //    Code = regions.Code,
                //    RegionImageUrl = regions.RegionImageUrl,
                //};
                #endregion

                //AutoMapper Source = regions ; Destination = RegionDTO
                var regionDTO = mapper.Map<RegionDTO>(regions);

                return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> PutData([FromRoute] Guid id, [FromBody] UpdateReqDTO updateReqDTO)
        {
            if (ModelState.IsValid)
            {
                //Reposiory Layer - Dependency Injection
                var regionData = await regionRepository.GetById(id);//_context.Regions.Where(r => r.Id == id).FirstOrDefaultAsync();

                if (regionData == null)
                    return NotFound();

                #region updating the data based on id from route
                regionData.Name = updateReqDTO.Name;
                regionData.Code = updateReqDTO.Code;
                regionData.RegionImageUrl = updateReqDTO.RegionImageUrl;
                #endregion

                //Reposiory Layer - Dependency Injection
                int r = await regionRepository.UpdateRegions(id, regionData);
                if (r == 0)
                    return BadRequest("Update Failed..");

                // AutoMapper source = regionData, destination = RegionDTO
                var regionDTO = mapper.Map<RegionDTO>(regionData);

                return Ok(regionDTO);
            }
            else
            { 
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteByID([FromRoute] Guid id)
        {
            //Reposiory Layer - Dependency Injection
            var regions = await regionRepository.GetById(id);// _context.Regions.Where(r => r.Id == id).FirstOrDefaultAsync();

            if (regions == null)
                return NotFound();

            //Reposiory Layer - Dependency Injection
            int r = await regionRepository.DeleteRegions(id);
            if (r == 0) 
                return NotFound("Record delete failed");
           
            // AutoMapper source = regions ; destination = RegionDTO
            var regionsDTO = mapper.Map<RegionDTO>(regions);

            return Ok(regionsDTO);
        }
    }
}
