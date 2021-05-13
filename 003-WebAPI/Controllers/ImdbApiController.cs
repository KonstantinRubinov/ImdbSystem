using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ImdbSystem
{
	[RoutePrefix("api")]
	[Authorize]
	public class ImdbApiController : ApiController
    {
		private IImdbRepository imdbRepository;
		private IUsersRepository usersRepository;

		public ImdbApiController(IImdbRepository _imdbRepository, IUsersRepository _usersRepository)
		{
			imdbRepository = _imdbRepository;
			usersRepository = _usersRepository;
		}


		[HttpGet]
		[Route("movies/imdbId/{movieId}/")]
		public async Task<HttpResponseMessage> GetImdbById(string movieId)
		{
			//http://localhost:49270/api/imdbMovies/byId/tt3896198/

			string id = base.ControllerContext.RequestContext.Principal.Identity.Name;

			try
			{
				UserModel userModel = usersRepository.GetOneUserById(id);
				MovieExtendModel oneMovie = await imdbRepository.GetImdbById(userModel.userImdbPass, movieId, id);
				return Request.CreateResponse(HttpStatusCode.OK, oneMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("movies/imdbWord/{movieWord}/")]
		public async Task<HttpResponseMessage> GetImdbByWord(string movieWord)
		{
			//http://localhost:49270/api/imdbMovies/byWord/matrix/

			string id = base.ControllerContext.RequestContext.Principal.Identity.Name;

			try
			{
				UserModel userModel = usersRepository.GetOneUserById(id);
				List<MovieModel> movies = await imdbRepository.GetImdbByWord(userModel.userImdbPass, movieWord, id);
				return Request.CreateResponse(HttpStatusCode.OK, movies);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}

		[HttpGet]
		[Route("movies/imdbTitle/{movieTitle}/")]
		public async Task<HttpResponseMessage> GetImdbByTitle(string movieTitle)
		{
			//http://localhost:49270/api/imdbMovies/byTitle/matrix/


			string id = base.ControllerContext.RequestContext.Principal.Identity.Name;

			try
			{
				UserModel userModel = usersRepository.GetOneUserById(id);
				MovieExtendModel oneMovie = await imdbRepository.GetImdbByTitle(userModel.userImdbPass, movieTitle, id);
				return Request.CreateResponse(HttpStatusCode.OK, oneMovie);
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
			}
		}
	}
}
