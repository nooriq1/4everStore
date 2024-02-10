using Microsoft.Extensions.Hosting;

namespace _4everStore.Models
{
    public class Catgory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Iteam> Iteam { get; set; }

    }
}
