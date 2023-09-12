using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment2_Baetiong.Models;
namespace Assignment2_Baetiong.Controllers
{
    public class AnimalsController : ApiController
    {
        ANIMALEntities3 db = new ANIMALEntities3();

        [HttpGet]
        public HttpResponseMessage Get() //List all items
        {
            var animals = db.Animal2;
            var response = Request.CreateResponse<IEnumerable<Animal2>>(HttpStatusCode.OK, animals);
            return response;
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)  // list an item
        {
            var animal = db.Animal2.Where(m => m.id == id).FirstOrDefault();
            if (animal != null)
            {
                var response = Request.CreateResponse<Animal2>(HttpStatusCode.OK ,animal); 
                return response;
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound ,"Id Not Found!");
                return response;
            }



        }

        [HttpPost]
        public HttpResponseMessage Post(AnimalDTO animal)
        {
            try 
            { 
            
            var newanimal = new Animal2
            {
                Size = animal.Size,
                Color = animal.Color,
                Name = animal.Name,
                Type = animal.Type,
            };
            db.Animal2.Add(newanimal);
            db.SaveChanges();
            var response = Request.CreateResponse<Animal2>(HttpStatusCode.Created ,newanimal);
            return response;
            }
            catch (Exception ex)
            {
                var errorresponse = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                return errorresponse;
            }
        }


        [HttpPut]
        public HttpResponseMessage Put(int id, AnimalDTO animal)
        {
            var animaledit = db.Animal2.Where(m => m.id == id).FirstOrDefault();
            if (animaledit != null)
            { 
            animaledit.Size = animal.Size;
            animaledit.Color = animal.Color;
            animaledit.Name = animal.Name;
            animaledit.Type = animal.Type;
            db.SaveChanges();
                var response = Request.CreateResponse<Animal2>(HttpStatusCode.OK ,animaledit);
            return response;
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound, "Id Not Found!");
                return response;
            }

        }

        public HttpResponseMessage Delete(int id)
        {
            var animal = db.Animal2.Where(m => m.id == id).FirstOrDefault();
            if (animal != null)
            {
                db.Animal2.Remove(animal);
                db.SaveChanges();
                var response = Request.CreateResponse(HttpStatusCode.OK, "Deleted Successfully");

                return response;
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound, "Id Not Found!");
                return response;
            }

        }
    }
}
