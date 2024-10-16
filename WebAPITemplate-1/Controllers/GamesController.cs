using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPITemplate.Context;
using WebAPITemplate.Models;

namespace WebAPITemplate.Controllers
{
    public class GamesController : Controller
    {
        private AppGameApiDB db = new AppGameApiDB();
        private HttpClient client; 

        public GamesController()
        {
            this.client = new HttpClient();
        }

        // GET: Games
        public ActionResult Index()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"http://localhost:55418/api/games");
                var response = client.GetAsync("games");
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var data = response.Result.Content.ReadAsAsync<IEnumerable<Game>>().Result;
                    return View(data);
                }
                else
                    return HttpNotFound();
            }

                //return View(db.Game.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            client.BaseAddress = new Uri(@"http://localhost:55418/api/games");
            var response = client.GetAsync("games/" + id.ToString());

            if(response.Result.IsSuccessStatusCode)
            {
                var data = response.Result.Content.ReadAsAsync<Game>().Result;
                return View(data);
            }
            return HttpNotFound();

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Game game = db.Game.Find(id);
            //if (game == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Game game)
        {
            client.BaseAddress = new Uri(@"http://localhost:55418/api/addgames");
            var response = client.PostAsJsonAsync("addgames", game);
            response.Wait();

            if (response.Result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return HttpNotFound();
        }

        //public ActionResult Create([Bind(Include = "Id,Name,Quantity,Price")] Game game)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Game.Add(game);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(game);
        //}

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Game game)
        {
            client.BaseAddress = new Uri(@"http://localhost:55418/api/updategames");
            var response = client.PutAsJsonAsync("updategames/" + game.Id.ToString(), game);
            response.Wait();

            if (response.Result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return HttpNotFound();
        }

        //public ActionResult Edit([Bind(Include = "Id,Name,Quantity,Price")] Game game)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(game).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(game);
        //}

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            client.BaseAddress = new Uri(@"http://localhost:55418/api/deletegames");
            var response = client.DeleteAsync("deletegames/" + id.ToString());
            response.Wait();

            if (response.Result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return HttpNotFound();

            //Game game = db.Game.Find(id);
            //db.Game.Remove(game);
            //db.SaveChanges();
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
