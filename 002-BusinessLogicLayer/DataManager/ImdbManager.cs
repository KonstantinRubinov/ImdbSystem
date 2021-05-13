using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImdbSystem
{
	public class ImdbManager: IImdbRepository
	{
		public async Task<MovieExtendModel> GetImdbById(string userPass, string movieId, string userId, string type = "", int year = 0, string r = "json", string plot = "short")
		{
			//type = movie, series, episode
			//y = 1981
			//r=json, xml
			//plot	=	short, full

			string movie = "";
			JObject jmovie = new JObject();
			string url = "http://www.omdbapi.com/?" + "apikey=" + userPass + "&i=" + movieId + "&plot=full";
			using (HttpClient client = new HttpClient())
			{

				movie = await client.GetStringAsync(url);
				jmovie = JObject.Parse(movie);
			}

			return createMovieExtendModel(jmovie, userPass, userId);
		}


		public async Task<MovieExtendModel> GetImdbByTitle(string userPass, string movieTitle, string userId, string type = "", int year = 0, string r = "json", string plot = "short")
		{
			//type = movie, series, episode
			//y = 1981
			//r=json, xml
			//plot	=	short, full


			MovieExtendModel movieModel = new MovieExtendModel();
			string movie = "";
			JObject jmovie = new JObject();
			string url = "http://www.omdbapi.com/?" + "apikey=" + userPass + "&t=" + movieTitle;
			using (HttpClient client = new HttpClient())
			{

				movie = await client.GetStringAsync(url);
				jmovie = JObject.Parse(movie);
			}

			return createMovieExtendModel(jmovie, userPass, userId);
		}

		public async Task<List<MovieModel>> GetImdbByWord(string userPass, string movieWord, string userId, string type = "", int year = 0, string r = "json", int page = 0)
		{
			//type = movie, series, episode
			//y = 1981
			//r=json, xml
			//page  = 1-100
			List<MovieModel> moviesToReturn = new List<MovieModel>();
			string movies = "";
			JArray jmovies = new JArray();
			string url = "http://www.omdbapi.com/?" + "apikey=" + userPass + "&s=" + movieWord;
			using (HttpClient client = new HttpClient())
			{
				movies = await client.GetStringAsync(url);
				jmovies = (JArray)JObject.Parse(movies).GetValue("Search");
			}

			foreach (JObject jmovie in jmovies.Children<JObject>())
			{
				moviesToReturn.Add(createMovieModel(jmovie, userPass, userId));
			}
			return moviesToReturn;
		}


















		private MovieModel createMovieModel(JObject jmovie, string userPass, string userID)
		{
			MovieModel movieModel = new MovieModel();
			try
			{
				movieModel.imdbID = jmovie.GetValue("imdbID").ToString();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("imdbID Exception: " + ex.Message);
				movieModel.imdbID = "";
			}

			try
			{
				movieModel.title = jmovie.GetValue("Title").ToString();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("title Exception: " + ex.Message);
				movieModel.title = "";
			}

			try
			{
				movieModel.poster = jmovie.GetValue("Poster").ToString();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("poster Exception: " + ex.Message);
				movieModel.poster = "";
			}

			try
			{
				movieModel.userID = userID;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("userId Exception: " + ex.Message);
				movieModel.userID = "";
			}


			try
			{
				movieModel.year = int.Parse(jmovie.GetValue("Year").ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("year Exception: " + ex.Message);
				movieModel.year = 0;
			}



			return movieModel;
		}




		private MovieExtendModel createMovieExtendModel(JObject jmovie, string userPass, string userID)
		{
			MovieExtendModel movieModel = new MovieExtendModel();
			try
			{
				movieModel.imdbID = jmovie.GetValue("imdbID").ToString();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("imdbID:" + ex.Message);
				movieModel.imdbID = "";
			}

			try
			{
				movieModel.title = jmovie.GetValue("Title").ToString();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("title:" + ex.Message);
				movieModel.title = "";
			}

			try
			{
				movieModel.plot = jmovie.GetValue("Plot").ToString();
			}

			catch (Exception ex)
			{
				Debug.WriteLine("plot:" + ex.Message);
				movieModel.plot = "";
			}

			try
			{
				movieModel.website = jmovie.GetValue("Website").ToString();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("website:" + ex.Message);
				movieModel.website = "";
			}

			try
			{
				movieModel.rated = jmovie.GetValue("Rated").ToString();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("rated:" + ex.Message);
				movieModel.rated = "";
			}

			movieModel.seen = false;



			try
			{
				movieModel.poster = jmovie.GetValue("Poster").ToString();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("poster:" + ex.Message);
				movieModel.poster = "";
			}

			try
			{
				movieModel.userID = userID;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("userId:" + ex.Message);
				movieModel.userID = "";
			}


			try
			{
				movieModel.year = int.Parse(jmovie.GetValue("Year").ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("year:" + ex.Message);
				movieModel.year = 0;
			}


			try
			{
				movieModel.imdbRating = float.Parse(jmovie.GetValue("imdbRating").ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("imdbRating:" + ex.Message);
				movieModel.imdbRating = 0;
			}
			return movieModel;
		}
	}
}
