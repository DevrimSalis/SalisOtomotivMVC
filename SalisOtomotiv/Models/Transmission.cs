namespace SalisOtomotiv.Models
{
    public class Transmission
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public IList<Car> Cars { get; set; }
    }
}
