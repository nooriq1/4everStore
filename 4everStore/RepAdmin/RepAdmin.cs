using _4everStore.Data;
using _4everStore.Models;

namespace _4everStore.RepAdmin
{
    public class RepAdmin : IRepAdmin
    {
        private readonly AppDbContext _ctx;

        public RepAdmin(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public void addCatgory(Catgory cat)
        {
            _ctx.catgories.Add(cat);
            _ctx.SaveChanges(); 

        }

        public void addIteam(Iteam it)
        {
            _ctx.Iteams.Add(it);
            _ctx.SaveChanges();
        }

        public void removeCatgory(int id)
        {
            var categoryToRemove = _ctx.catgories.FirstOrDefault(c => c.Id == id);

            if (categoryToRemove != null)
            {
                _ctx.catgories.Remove(categoryToRemove);
                _ctx.SaveChanges();
            }
        }

        public void removeIteam(int id)
        {
            var iteamToRemove = _ctx.Iteams.FirstOrDefault(c => c.Id == id);
            if(iteamToRemove != null)
            {
                _ctx.Iteams.Remove(iteamToRemove);
                _ctx.SaveChanges();
            }
        }

        public List<request> getAllRequset()
        {
            List<request> requests = _ctx.requests.Where(r => !r.ship ).ToList();
            return requests;
        }

        public request getRequset(int id)
        {
            request re = _ctx.requests
                  .FirstOrDefault(p => p.Id == id);

            return re;
        }

        public void updatestatus(int id)
        {
            var requestToUpdate = _ctx.requests.FirstOrDefault(r => r.Id == id);

            if (requestToUpdate != null)
            {
                
                requestToUpdate.ship = true;

                
                _ctx.SaveChanges();
            }

        }

        public List<request> getAllShpping()
        {
            List<request> requests = _ctx.requests.Where(r => r.ship && !r.done).ToList();
                    
            return requests;
        }

        public void updatestatusdone(int id)
        {
            var requestToUpdate = _ctx.requests.FirstOrDefault(r => r.Id == id);

            if (requestToUpdate != null)
            {

                requestToUpdate.done = true;


                _ctx.SaveChanges();
            }
        }

        public List<request> getAllDone()
        {
            List<request> requests = _ctx.requests.Where(r => r.ship && r.done).ToList();

            return requests;
        }
    }
}
