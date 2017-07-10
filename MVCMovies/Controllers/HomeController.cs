using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCMovies.Models;

namespace MVCMovies.Controllers
{
    public class HomeController : Controller
    {

        //DataBase Connection
        private MVCMovies150617Entities1 db = new MVCMovies150617Entities1();

        // GET: Home
        public ActionResult Index(string movieGenre, string searchString)
        {
            //genre serach
            var GenreList = new List<string>();
            var GenreQuery = from g in db.Movies
                             orderby g.Genre
                             select g.Genre;
            GenreList.AddRange(GenreQuery.Distinct());
            ViewBag.movieGenre = new SelectList(GenreList);


            //LINQ query to get all records from Movies table
            var movies = from m in db.Movies
                         select m;
            //title search
            if(!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            //last bit of the genre search
            if(!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return View(movies);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movy movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }





        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movy movie = db.Movies.Find(id);
            if(movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movy movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }



        [HttpPost]
        public ActionResult Edit(Movy movie)
        {
            if(ModelState.IsValid)
            {
                db.Entry(movie).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }
           



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movy movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }



        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed (int id)
        {
            Movy movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();     
            return RedirectToAction("Index");
        }



    }
}