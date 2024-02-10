using _4everStore.Models;
using _4everStore.Models.view;
using _4everStore.RepAdmin;
using _4everStore.RepUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace _4everStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IRepAdmin _repAdmin;
        private readonly  IUserRep _userRep;
        private readonly IHostingEnvironment _host;
       

        public AdminController(IRepAdmin IRepAdmin, IUserRep IUserRep,IHostingEnvironment host)
        {
                _repAdmin = IRepAdmin;
                _userRep = IUserRep;
                _host = host;
              
        }

        public IActionResult Index()
        {
            List<request>re = _repAdmin.getAllRequset();
            return View(re);
        }
        [HttpGet]
        public IActionResult AddIteam()
        {
            iteamAdmin a = new iteamAdmin();
            a.catgory=_userRep.getAllCatgory();
           
            return View(a);
        }
        [HttpPost]
        public IActionResult AddIteam(Iteam Iteam)
        {
            string filename = "";
            string myupload = Path.Combine(_host.WebRootPath, "images");
            filename = Guid.NewGuid().ToString() + "_"+ Iteam.clientFile.FileName;
            string fullpath=Path.Combine(myupload, filename);
            Iteam.clientFile.CopyTo(new FileStream(fullpath,FileMode.Create));
            Iteam.img1 = filename;



            string filename2 = "";
            string myupload2 = Path.Combine(_host.WebRootPath, "images");
            filename2 = Guid.NewGuid().ToString() + "_" + Iteam.clientFile2.FileName;
            string fullpath2 = Path.Combine(myupload2, filename2);
            Iteam.clientFile.CopyTo(new FileStream(fullpath2, FileMode.Create));
            Iteam.img2 = filename2;





            _repAdmin.addIteam(Iteam);
            TempData["Add"] = "  تمت اضافة " + Iteam.Name;
            return RedirectToAction("index");

        }

        public IActionResult AddCatgory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCatgory(Catgory cat)
        {
            _repAdmin.addCatgory(cat);
            TempData["Add"] =  " تمت اضافة" + cat.Name;
            return RedirectToAction("index");
        }
        public IActionResult detalisReq(int idR,int idI) 
        {
            requsetandIteam n = new requsetandIteam();
            n.request = _repAdmin.getRequset(idR);
            n.iteam= _userRep.getIteamById(idI);
            return View(n);
        }
        public IActionResult shipping (int id)
        {
            _repAdmin.updatestatus(id);
            TempData["Add"] = "تم الشحن";
            return RedirectToAction("index");
        }
        public IActionResult inshpping () 
        {
            List<request> re = _repAdmin.getAllShpping();
            return View(re);
        }
        public IActionResult Done(int id)
        {
            _repAdmin.updatestatusdone(id);
            TempData["Add"] = "اكتمل الطلب";
            return RedirectToAction("index");
        }
        public IActionResult doneReq()
        {
            List<request> r = _repAdmin.getAllDone();
            return View(r);
        }

        


    }
}
