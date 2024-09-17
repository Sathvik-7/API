using API.Models.DTO;
using API.Repository.Interface;
using DemoAPIProject.DataDbContext;
using DemoAPIProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Implementation
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly WalksDbContext _dbContext;

        public SQLRegionRepository(WalksDbContext _dbContext)
        {
            this._dbContext = _dbContext;   
        }

        public async Task<List<Region>> GetAllData()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetById(Guid id)
        {
            return await _dbContext.Regions.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> CreateRegions(Region region)
        {
            await _dbContext.Regions.AddAsync(region);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateRegions(Guid id, Region region)
        {
            var regionData = await _dbContext.Regions.Where(r => r.Id == id).FirstOrDefaultAsync();

            if (regionData == null)
                return 0;

            regionData.Code = region.Code;
            regionData.Name = region.Name;
            regionData.RegionImageUrl = region.RegionImageUrl;
            
            //_dbContext.Regions.Update(region);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRegions(Guid id)
        {
            var regionData = await _dbContext.Regions.Where(r => r.Id == id).FirstOrDefaultAsync();

            if (regionData == null)
                return 0;

            _dbContext.Regions.Remove(regionData);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
