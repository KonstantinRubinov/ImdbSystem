using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ImdbSystem
{
	public class SqlMoviesManager : SqlDataBase, IMoviesRepository
	{
		public List<MovieModel> GetAllMovies(string userID)
		{
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();
			List<MovieModel> arrMovie = new List<MovieModel>();

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsSql.GetAllMovies(userID));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsSql.GetByWord(word, userID));
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


			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsSql.GetById(imdbID, userID));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsSql.GetByTitle(title, userID));
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
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsSql.AddMovie(movieModel));
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
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieStringsSql.UpdateMovie(movieModel));
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
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(MovieStringsSql.DeleteMovie(imdbID, userID));
			}
			return i;
		}
	}
}
