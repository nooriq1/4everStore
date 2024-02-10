using System.ComponentModel.DataAnnotations.Schema;

namespace _4everStore.Models
{
    public class Iteam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        [NotMapped]
        public IFormFile  clientFile { get; set; }
        [NotMapped]
        public IFormFile  clientFile2 { get; set; }
        
        public int CatgoryId { get; set; }
        public Catgory catgory { get; set; }
    }
}
