using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPITemplate.Context;
using WebAPITemplate.Models;

namespace WebAPITemplate.Controllers.api
{
    public class GameController : ApiController
    {
        private AppGameApiDB _dbContext;

        public GameController()
        {
            this._dbContext = new AppGameApiDB();
        }

        [Route("api/games")]
        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
            return _dbContext.Game.ToList();
        }

        [Route("api/games/{id}")]
        public IHttpActionResult GetGame(int id)
        {
            var game = _dbContext.Game.FirstOrDefault(g => g.Id == id);

            if (game == null)
                return NotFound();//throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(game);
        }

        [Route("api/addgames")]
        [HttpPost]
        public IHttpActionResult AddGames(Game game)
        {
            if (!ModelState.IsValid)
                return BadRequest(); //throw new HttpResponseException(HttpStatusCode.BadRequest);

            _dbContext.Game.Add(game);
            _dbContext.SaveChanges();

            return Ok(game);
        }
        [Route("api/updategames/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateGames(int id, Game game)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var data = _dbContext.Game.FirstOrDefault(g => g.Id == id);

            if (data == null)
                return NotFound(); //throw new HttpResponseException(HttpStatusCode.NotFound);

            data.Name = game.Name;
            data.Price = game.Price;
            data.Quantity = game.Quantity;

            _dbContext.SaveChanges();

            return Ok();
        }

        [Route("api/deletegames/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteGame(int id)
        {
            var data = _dbContext.Game.FirstOrDefault(g => g.Id == id);

            if (data == null)
                return NotFound(); //throw new HttpResponseException(HttpStatusCode.NotFound);

            _dbContext.Game.Remove(data);
            _dbContext.SaveChanges();

            return Ok();
        }

    }
}
