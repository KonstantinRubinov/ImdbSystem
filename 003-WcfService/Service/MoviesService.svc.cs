using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Text;

namespace ImdbSystem
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MoviesService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select MoviesService.svc or MoviesService.svc.cs at the Solution Explorer and start debugging.
	public class MoviesService : IMoviesService
	{
		private IMoviesExtendRepository moviesExtendRepository;
		private IUsersRepository usersRepository;
		public MoviesService()
		{
			if (GlobalVariable.logicType == 0)
			{
				moviesExtendRepository = new EntityMoviesExtendManager();
				usersRepository = new EntityUsersManager();
			}
			else if (GlobalVariable.logicType == 1)
			{
				moviesExtendRepository = new SqlMoviesExtendManager();
				usersRepository = new SqlUsersManager ();
			}
			else if (GlobalVariable.logicType == 2)
			{
				moviesExtendRepository = new MySqlMoviesExtendManager();
				usersRepository = new MySqlUsersManager();
			}
			else
			{
				moviesExtendRepository = new MongoMoviesExtendManager();
				usersRepository = new MongoUsersManager();
			}

		}
		
		public HttpResponseMessage GetAllMovies()
		{
			try {
				IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
				string userPass = woc.Headers["userPass"];
				userPass = Encoding.UTF8.GetString(Convert.FromBase64String(userPass));

				string userID = usersRepository.ReturnUserIdByImdbPass(userPass);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(moviesExtendRepository.GetAllMovies(userID)))
				};

				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hrm;
			}
		}

		public HttpResponseMessage GetById(string getByID)
		{
			try
			{
				IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
				string userPass = woc.Headers["userPass"];
				userPass = Encoding.UTF8.GetString(Convert.FromBase64String(userPass));

				string userID = usersRepository.ReturnUserIdByImdbPass(userPass);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(moviesExtendRepository.GetById(getByID, userID)))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hrm;
			}
		}

		public HttpResponseMessage GetByWord(string getByWord)
		{
			try
			{
				IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
				string userPass = woc.Headers["userPass"];
				userPass = Encoding.UTF8.GetString(Convert.FromBase64String(userPass));

				string userID = usersRepository.ReturnUserIdByImdbPass(userPass);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(moviesExtendRepository.GetByWord(getByWord, userID)))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hrm;
			}
		}

		public HttpResponseMessage GetByTitle(string getByTitle)
		{
			try
			{
				IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
				string userPass = woc.Headers["userPass"];
				userPass = Encoding.UTF8.GetString(Convert.FromBase64String(userPass));

				string userID = usersRepository.ReturnUserIdByImdbPass(userPass);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(moviesExtendRepository.GetByTitle(getByTitle, userID)))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hrm;
			}
		}

		public HttpResponseMessage AddMovie(MovieExtendModel movieModel)
		{
			try
			{
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.Created)
				{
					Content = new StringContent(JsonConvert.SerializeObject(moviesExtendRepository.AddMovie(movieModel)))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hrm;
			}
		}

		public HttpResponseMessage UpdateMovie(string updateById, MovieExtendModel movieModel)
		{
			try
			{
				movieModel.imdbID = updateById;
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(moviesExtendRepository.UpdateMovie(movieModel)))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hrm;
			}
		}

		public HttpResponseMessage DeleteMovie(string deletById)
		{
			try
			{
				IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
				string userPass = woc.Headers["userPass"];
				userPass = Encoding.UTF8.GetString(Convert.FromBase64String(userPass));

				string userID = usersRepository.ReturnUserIdByImdbPass(userPass);
				int i = moviesExtendRepository.DeleteMovie(deletById, userID);

				if (i > 0)
				{
					HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.NoContent)
					{
					};
					return hrm;
				}
				HttpResponseMessage hr = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
				};
				return hr;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hr = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hr;
			}

		}


	}
}
