using _4everStore.Data;
using _4everStore.Models;
using Microsoft.Extensions.Hosting;

namespace _4everStore.RepUser
{

    public class UserRep : IUserRep
    {
        private readonly AppDbContext _ctx;
        public UserRep(AppDbContext ctx)
        {
            _ctx = ctx;

        }

        public void addrequset(request request)
        {
            _ctx.requests.Add(request);
            _ctx.SaveChanges();
        }

        public List<Catgory> getAllCatgory()
        {
            List<Catgory> cat = _ctx.catgories.ToList();
            return cat;

        }

        public List<Iteam> getAlliteam()
        {
            List<Iteam> it = _ctx.Iteams.ToList();
            return it;
        }

        public List<Iteam> getiteamBycat(int id)
        {
            List<Iteam> posts = _ctx.Iteams
                         .Where(p => p.CatgoryId == id)
                        .ToList();

            return posts;

        

        }

        public Iteam getIteamById(int id)
        {
            Iteam it = _ctx.Iteams
                  .FirstOrDefault(p => p.Id == id);

            return it;
        }

        public List<Iteam> getlastiteams()
        {
            throw new NotImplementedException();
        }

        public List<request> GetRequestByemail(string email)
        {
            //List<request> requests = _ctx.requests
            //                             .Where(p => p.User_ID_email == email && !p.done )
            //                             .ToList();
            //return requests;





            List<request> requests = _ctx.requests
        .Where(p => p.User_ID_email == email && !p.done)
        .Join(
            _ctx.Iteams,
            request => request.iteamId,
            iteam => iteam.Id,
            (request, iteam) => new request
            {
                Id = request.Id,
                User_ID_email = request.User_ID_email,
                name = request.name,
                city = request.city,
                descripon = request.descripon,
                phonenumber = request.phonenumber,
                Quantity = request.Quantity,
                totalprice = request.totalprice,
                ship = request.ship,
                done = request.done,
                iteamId = request.iteamId,
                iteam = iteam
            }
        )
        .ToList();

            return requests;

        }



        public List<Iteam> randomIteam()
        {
            List<Iteam> randomit = _ctx.Iteams
       .OrderBy(p => Guid.NewGuid())
       .Take(10)
       .ToList();

            return randomit;
        }


    }
}
