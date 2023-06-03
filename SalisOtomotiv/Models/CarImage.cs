namespace SalisOtomotiv.Models
{
    public class CarImage
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string FilePath { get; set; }
    }
}
