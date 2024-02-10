namespace _4everStore.Models
{
    public class request
    {
        public int Id { get; set; }
        public string User_ID_email { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string descripon { get; set; }
        public string phonenumber { get; set; }
        public int Quantity { get; set; }
        public int totalprice { get; set; } = 0;
        public bool ship { get; set; } = false;
        public bool done { get; set; } = false;

        public int iteamId { get; set; }
        public Iteam iteam { get; set; }
    }
}
