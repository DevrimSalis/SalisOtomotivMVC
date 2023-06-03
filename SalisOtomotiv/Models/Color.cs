namespace SalisOtomotiv.Models
{
    public class Color
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public IList<Car> Cars { get; set; }
    }
}
