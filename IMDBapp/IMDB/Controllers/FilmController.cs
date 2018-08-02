﻿using System.Linq;
using System.Net;
using System.Web.Mvc;
using IMDB.Models;

namespace IMDB.Controllers
{
    [ValidateInput(false)]
    public class FilmController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            using (var db = new IMDBDbContext())
            {
                var films = db.Films.ToList();
                return View(films);
            }
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Film film)
        {
            using (var db = new IMDBDbContext())
            {
                db.Films.Add(film);
                db.SaveChanges();
                return Redirect("/");
            }
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            using (var db = new IMDBDbContext())
            {
                var film = db.Films.Find(id);
                if (film != null)
                {
                    return View(film);
                }
            }
            return Redirect("/");
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(int? id, Film filmModel)
        {

           

            using (var db = new IMDBDbContext())
            {
                var filmFromDb = db.Films.Find(filmModel.Id);
                if (filmFromDb != null)
                {
                    filmFromDb.Name = filmModel.Name;
                    filmFromDb.Director = filmModel.Director;
                    filmFromDb.Genre = filmModel.Genre;
                    filmFromDb.Year = filmModel.Year;
                    db.SaveChanges();
                }
            }
            return Redirect("/");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            using (var db = new IMDBDbContext())
            {
                var film = db.Films.Find(id);
                if (film != null)
                {
                    return View(film);
                }
            }
            return Redirect("/");
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id, Film filmModel)
        {
            using (var db = new IMDBDbContext())
            {
                var film = db.Films.Find(id);
                if (film != null)
                {
                    db.Films.Remove(film);
                    db.SaveChanges();
                }
            }
            return Redirect("/");
        }
    }
}