using _4everStore.Models;
using _4everStore.Models.view;
using _4everStore.RepUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace _4everStore.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IUserRep _UserRep;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(IUserRep userRep, UserManager<IdentityUser> userManager)
        {
            _UserRep=userRep;
            _userManager=userManager;
        }

       
        public IActionResult Index()
        {
            iteamAndCatgory a = new iteamAndCatgory();
            a.catgoriesV = _UserRep.getAllCatgory();
            a.iteamsV = _UserRep.getAlliteam();

            return View(a);
        }

        [Authorize]
        [Route("Iteams/{id}")]
        public IActionResult Iteams(int id)
        {
            RelatedAndITeam a = new RelatedAndITeam();
            a.iteamV= _UserRep.getIteamById(id);
            a.Related= _UserRep.randomIteam();

            return View(a);
        }
        [Authorize]
        public IActionResult catgoryIteam(int id)
        {
           List<Iteam> it = _UserRep.getiteamBycat(id);
            return View(it);
        }
        [Authorize]
        public IActionResult cart()
        {
           List<request> re= _UserRep.GetRequestByemail(User.Identity.Name.ToString());

            return View(re);
        }
        [Authorize]
        public IActionResult buy(int id)
        {
            requestAndIteam a = new requestAndIteam();
            a.Iteam = _UserRep.getIteamById(id);
            
            return View(a);
        }
        [HttpPost]
        [Authorize]
        public IActionResult buy(request request)
        {
            Iteam it = _UserRep.getIteamById(request.iteamId);
            int price=it.price;
            request r = new request()
            {
                User_ID_email = User.Identity.Name.ToString(),
                name = request.name,
                descripon = request.descripon,
                city = request.city,
                phonenumber = request.phonenumber,
                iteamId = request.iteamId,
               Quantity=request.Quantity,
               totalprice= request.Quantity * price
            };

            _UserRep.addrequset(r);
            TempData["Add"] = " „ «·‘—«¡";
            return RedirectToAction("cart");

        }
        


    }
}
