using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.Data.Entity;

namespace WebApplication3.Controllers
{
   
   
    public class UserController : Controller
    {
        HRMSUser obj = new HRMSUser();

        // GET: User

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddProfile(frmUserData userDetail)
        {
           
                 
            var profile = Request.Files;

            string imgname = string.Empty;
            string ImageName = string.Empty;

            //Following code will check that image is there 
            //If it will Upload or else it will use Default Image

            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var logo = System.Web.HttpContext.Current.Request.Files["file"];
                if (logo.ContentLength > 0)
                {
                    var profileName = Path.GetFileName(logo.FileName);
                    var ext = Path.GetExtension(logo.FileName);

                    ImageName = profileName;
                    var comPath = Server.MapPath("/Images/") + ImageName;

                    logo.SaveAs(comPath);
                    userDetail.Profile = ImageName;


                    //obj.userprofiles.Add(userDetail);

                    userprofile userObj = new userprofile();
                    userObj.name = userDetail.name;
                    userObj.email = userDetail.email;
                    userObj.password = userDetail.password;
                    userObj.Profile = userDetail.Profile;
                    userObj.gender = userDetail.gender;

                    try
                    {
                        obj.userprofiles.Add(userObj);
                        obj.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }


                }

            }
            else
                userDetail.Profile = Server.MapPath("/Images/") + "profile.jpg";

            int response = 1;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getData()
        {

            return View(obj.userprofiles.ToList());
        }
    }
}