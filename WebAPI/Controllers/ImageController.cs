using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ImageController : ApiController
    {

        [HttpPost]
        [Route("api/UploadImage")]
        public HttpResponseMessage UploadImage()
        {
            string imageName = null;
            var httRequest = HttpContext.Current.Request;
            //Upload Image 
            var postedFile = httRequest.Files["Image"];
            //Create custom filename
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
            postedFile.SaveAs(filePath);

            //Save to DB
            using (DatingAppEntities db = new DatingAppEntities())
            {
                image image = new image()
                {
                    ImageCaption = httRequest["ImageCaption"],
                    ImageName = imageName
                };
                db.image.Add(image);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
