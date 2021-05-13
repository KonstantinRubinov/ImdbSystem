using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ImdbSystem
{
	public class MySqlMoviesManager : MySqlDataBase, IMoviesRepository
	{
		public List<MovieModel> GetAllMovies(string userID)
		{
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();
			List<MovieModel> arrMovie = new List<MovieModel>();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsMySql.GetAllMovies(userID));
			}

			foreach (DataRow ms in dt.Rows)
			{
				arrMovie.Add(MovieModel.ToObject(ms));
			}

			return arrMovie;
		}

		public List<MovieModel> GetByWord(string word, string userID)
		{
			if (word.Equals(string.Empty) || word.Equals(""))
				throw new ArgumentOutOfRangeException();

			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();


			DataTable dt = new DataTable();
			List<MovieModel> arrMovie = new List<MovieModel>();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsMySql.GetByWord(word, userID));
			}

			foreach (DataRow ms in dt.Rows)
			{
				arrMovie.Add(MovieModel.ToObject(ms));
			}

			return arrMovie;
		}

		public MovieModel GetById(string imdbID, string userID)
		{
			if (imdbID.Equals(string.Empty) || imdbID.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();
			MovieModel movieModel = new MovieModel();


			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsMySql.GetById(imdbID, userID));
			}

			foreach (DataRow ms in dt.Rows)
			{
				movieModel = MovieModel.ToObject(ms);
			}

			return movieModel;
		}

		public MovieModel GetByTitle(string title, string userID)
		{
			if (title.Equals(string.Empty) || title.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();

			MovieModel movieModel = new MovieModel();

			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsMySql.GetByTitle(title, userID));
			}

			foreach (DataRow ms in dt.Rows)
			{
				movieModel = MovieModel.ToObject(ms);
			}
			return movieModel;
		}


		public MovieModel AddMovie(MovieModel movieModel)
		{
			DataTable dt = new DataTable();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsMySql.AddMovie(movieModel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				movieModel = MovieModel.ToObject(ms);
			}

			return movieModel;
		}


		public MovieModel UpdateMovie(MovieModel movieModel)
		{
			DataTable dt = new DataTable();
			using (MySqlCommand command = new MySqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsMySql.UpdateMovie(movieModel));
			}
			foreach (DataRow ms in dt.Rows)
			{
				movieModel = MovieModel.ToObject(ms);
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
				i = ExecuteNonQuery(MovieStringsMySql.DeleteMovie(imdbID, userID));
			}
			return i;
		}
	}
}
