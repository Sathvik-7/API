﻿using DemoAPIProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoAPIProject.DataDbContext
{
    public class WalksDbContext : DbContext
    {

        public WalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Region> Regions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            #region Preparing and seeding the data for difficulty table
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    ID = Guid.Parse("df2bc1f2-4acf-438d-aa55-83b6d8e99592"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    ID = Guid.Parse("506b5853-2d3b-477b-95b4-67294cf27a3d"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    ID = Guid.Parse("723f02fb-89d1-48fd-94bf-cc191f989374"),
                    Name = "Hard"
                }
            };
            //Seeding the data
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
            #endregion

            #region Preparing and seeding the data for Regions Table
            var regions = new List<Region>()
            {
              new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                }
            };
            //seeding the data
            modelBuilder.Entity<Region>().HasData(regions);
            #endregion
        }
    }
}
