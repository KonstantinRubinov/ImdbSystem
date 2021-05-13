using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ImdbSystem
{
	public class MySqlMoviesExtendManager : MySqlDataBase, IMoviesExtendRepository
	{
		public List<MovieExtendModel> GetAllMovies(string userID)
		{
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();
			List<MovieExtendModel> arrMovie = new List<MovieExtendModel>();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsMySql.GetAllMovies(userID));
			}

			foreach (DataRow ms in dt.Rows)
			{
				arrMovie.Add(MovieExtendModel.ToObject(ms));
			}

			return arrMovie;
		}

		public List<MovieExtendModel> GetByWord(string word, string userID)
		{
			if (word.Equals(string.Empty) || word.Equals(""))
				throw new ArgumentOutOfRangeException();

			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();
			List<MovieExtendModel> arrMovie = new List<MovieExtendModel>();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsMySql.GetByWord(word, userID));
			}

			foreach (DataRow ms in dt.Rows)
			{
				arrMovie.Add(MovieExtendModel.ToObject(ms));
			}

			return arrMovie;
		}




		public MovieExtendModel GetById(string imdbID, string userID)
		{
			if (imdbID.Equals(string.Empty) || imdbID.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();

			MovieExtendModel movieModel = new MovieExtendModel();


			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsMySql.GetById(imdbID, userID));
			}

			foreach (DataRow ms in dt.Rows)
			{
				movieModel = MovieExtendModel.ToObject(ms);
			}

			return movieModel;
		}






		public MovieExtendModel GetByTitle(string title, string userID)
		{
			if (title.Equals(string.Empty) || title.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();

			MovieExtendModel movieModel = new MovieExtendModel();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsMySql.GetByTitle(title, userID));
			}

			foreach (DataRow ms in dt.Rows)
			{
				movieModel = MovieExtendModel.ToObject(ms);
			}
			return movieModel;
		}


		public MovieExtendModel AddMovie(MovieExtendModel movieModel)
		{
			DataTable dt = new DataTable();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsMySql.AddMovie(movieModel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				movieModel = MovieExtendModel.ToObject(ms);
			}

			return movieModel;
		}


		public MovieExtendModel UpdateMovie(MovieExtendModel movieModel)
		{
			DataTable dt = new DataTable();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsMySql.UpdateMovie(movieModel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				movieModel = MovieExtendModel.ToObject(ms);
			}

			return movieModel;
		}


		public int DeleteMovie(string imdbID, string userID)
		{
			if (imdbID.Equals(string.Empty) || imdbID.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			int i = 0;
			using (MySqlCommand command = new MySqlCommand())
			{
				i = ExecuteNonQuery(MovieExtendStringsMySql.DeleteMovie(imdbID, userID));
			}
			return i;
		}
	}
}
