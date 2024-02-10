using _4everStore.Models;

namespace _4everStore.RepAdmin
{
    public interface IRepAdmin
    {
        public void addIteam(Iteam it);
        public void removeIteam(int id );
        public void addCatgory(Catgory cat);
        public void removeCatgory(int id);
        public List<request> getAllRequset();
        public request getRequset(int id);

        public void updatestatus(int id);
        public void updatestatusdone(int id);
        public List<request> getAllShpping();
        public List<request> getAllDone();

    }
}
