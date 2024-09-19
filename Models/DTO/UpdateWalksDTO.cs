namespace API.Models.DTO
{
    public class UpdateWalksDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }
    }
}
