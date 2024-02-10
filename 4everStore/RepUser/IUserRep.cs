using _4everStore.Models;

namespace _4everStore.RepUser
{
    public interface IUserRep
    {
        public List<Catgory> getAllCatgory();
        public List<Iteam> getAlliteam();
        public List<Iteam> getiteamBycat(int id);
        public Iteam getIteamById(int id);
        public List<Iteam> randomIteam();
        public void addrequset(request request);
        public List<request> GetRequestByemail(string email);

        public List<Iteam> getlastiteams();



    }
}
