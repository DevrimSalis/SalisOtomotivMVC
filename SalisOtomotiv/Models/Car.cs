using System.Drawing.Drawing2D;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace SalisOtomotiv.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public string Image { get; set; }
        public int ModelYear { get; set; }
        public int KM { get; set; }
        public decimal Price { get; set; }
        public int EnginePower { get; set; }
        public string Description { get; set; }
        public int EngineSize { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int TransmissionId { get; set; }
        public Transmission Transmission { get; set; }
        public int DoorId { get; set; }
        public Door Door { get; set; }
        public int FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }
        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; }
        public int ConditionId { get; set; }
        public Condition Condition { get; set; }
    }
}
