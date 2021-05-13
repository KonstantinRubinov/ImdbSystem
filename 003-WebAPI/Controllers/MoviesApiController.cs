using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImdbSystem
{
	//[EnableCors("*", "*", "*")]
	[RoutePrefix("api")]
	[Authorize]
	public class MoviesApiController : ApiController
    {
		private IMoviesExtendRepository moviesExtendRepository;
		public MoviesApiController(IMoviesExtendRepository _moviesExtendRepository)
		{
			moviesExtendRepository = _moviesExtendRepository;
		}


		[HttpGet]
		[Route("movies")]
		public HttpResponseMessage GetAllMovies()
		{
			try
			{
				string id = base.ControllerContext.RequestContext.Principal.Identity.Name;
				List<MovieExtendModel> allMovies = moviesExtendRepository.GetAllMovies(id);
				return Request.CreateResponse(HttpStatusCode.OK, allMovies);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("movies/favoriteWord/{byWord}/")]
		public HttpResponseMessage GetByWord(string byWord)
		{
			try
			{
				string id = base.ControllerContext.RequestContext.Principal.Identity.Name;
				List<MovieExtendModel> movies = moviesExtendRepository.GetByWord(byWord, id);
				return Request.CreateResponse(HttpStatusCode.OK, movies);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("movies/favoriteId/{byId}/")]
		public HttpResponseMessage GetById(string byId)
		{
			try
			{
				string id = base.ControllerContext.RequestContext.Principal.Identity.Name;
				MovieExtendModel oneMovie = moviesExtendRepository.GetById(byId, id);
				return Request.CreateResponse(HttpStatusCode.OK, oneMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("movies/favoriteTitle/{movieTitle}/")]
		public HttpResponseMessage GetByTitle(string movieTitle)
		{
			try
			{
				string id = base.ControllerContext.RequestContext.Principal.Identity.Name;
				MovieExtendModel oneMovie = moviesExtendRepository.GetByTitle(movieTitle, id);
				return Request.CreateResponse(HttpStatusCode.OK, oneMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpPost]
		[Route("movies")]
		public HttpResponseMessage AddMovie(MovieExtendModel movieModel)
		{
			try
			{
				if (movieModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				//if (!ModelState.IsValid)
				//{
				//	Errors errors = ErrorsHelper.GetErrors(ModelState);
				//	return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				//}
				string id = base.ControllerContext.RequestContext.Principal.Identity.Name;
				movieModel.userID = id;
				MovieExtendModel addedMovie = moviesExtendRepository.AddMovie(movieModel);
				return Request.CreateResponse(HttpStatusCode.Created, addedMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpPut]
		[Route("movies/{id}")]
		public HttpResponseMessage UpdateMovie(string id, MovieExtendModel movieModel)
		{
			try
			{
				if (movieModel == null)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Data is null.");
				}
				//if (!ModelState.IsValid)
				//{
				//	Errors errors = ErrorsHelper.GetErrors(ModelState);
				//	return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
				//}
				string uid = base.ControllerContext.RequestContext.Principal.Identity.Name;
				movieModel.userID = uid;
				movieModel.imdbID = id;
				MovieExtendModel updatedMovie = moviesExtendRepository.UpdateMovie(movieModel);
				return Request.CreateResponse(HttpStatusCode.OK, updatedMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpDelete]
		[Route("movies/{id}")]
		public HttpResponseMessage DeleteMovie(string id)
		{
			try
			{
				string userId = base.ControllerContext.RequestContext.Principal.Identity.Name;
				int i = moviesExtendRepository.DeleteMovie(id, userId);
				return Request.CreateResponse(HttpStatusCode.NoContent);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}
	}
}
