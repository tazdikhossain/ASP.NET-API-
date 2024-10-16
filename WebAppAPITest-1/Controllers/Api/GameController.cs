using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppAPITest.Models;
using WebAppAPITest.Context;

namespace WebAppAPITest.Controllers.Api
{
    public class GameController : ApiController
    {
        private AppDBContext _dbContext;

        public GameController()
        {
            this._dbContext = new AppDBContext();
        }

        [Route("api/games")]
        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
            return _dbContext.Games.ToList();
        }

        [Route("api/games/{id}")]
        public Game GetGame(int id)
        {
            var game = _dbContext.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return game;
        }

        [Route("api/addgames")]
        [HttpPost]
        public Game AddGames(Game game)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _dbContext.Games.Add(game);
            _dbContext.SaveChanges();

            return game;
        }
        [Route("api/updategames/{id}")]
        [HttpPut]
        public void UpdateGames(int id, Game game)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var data = _dbContext.Games.FirstOrDefault(g => g.Id == id);

            if (data == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            data.Name = game.Name;
            data.Price = game.Price;
            data.Quantity = game.Quantity;

            _dbContext.SaveChanges();

        }

        [Route("api/deletegames/{id}")]
        [HttpDelete]
        public void DeleteGame(int id)
        {
            var data = _dbContext.Games.FirstOrDefault(g => g.Id == id);

            if (data == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _dbContext.Games.Remove(data);
            _dbContext.SaveChanges();

        }
    }
}
