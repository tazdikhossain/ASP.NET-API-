using AppApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppApi.Context;

namespace WebAppApi.Controllers.Api
{
    public class GameController : ApiController
    {
        private AppDBContext _dbCon;
        public GameController()
        {
            this._dbCon = new AppDBContext();
        }

        [Route("api/games")]
        public IEnumerable<Game> GetGames()
        {
            return _dbCon.Game.ToList();
        }

        [Route("api/games/{id}")]
        //public Game GetGame(int id)
        public IHttpActionResult GetGame(int id)
        {
            var game = _dbCon.Game.FirstOrDefault(x => x.Id == id);
            if (game == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();

            return Ok(game);
        }

        [Route("api/addgames")]
        [HttpPost]
        public Game AddGames(Game game)
        {
            if(!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _dbCon.Game.Add(game);
            _dbCon.SaveChanges();

            return game;
        }

        [Route("api/updategames/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateGame(int id, Game game)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var data = _dbCon.Game.FirstOrDefault(x => x.Id == id);
            if (data == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();

            data.Name = game.Name;
            data.Quantity = game.Quantity;
            data.Price = game.Price;

            return Ok();
            //_dbCon.SaveChanges();
        }

        [Route("api/deletegame/{id}")]
        [HttpDelete]
        //public void DeleteGame(int id)
        public IHttpActionResult DeleteGame(int id)
        {
            var data = _dbCon.Game.FirstOrDefault(x => x.Id == id);
            if(data == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();

            _dbCon.Game.Remove(data);
            _dbCon.SaveChanges();

            return Ok();
        }
    }
}
