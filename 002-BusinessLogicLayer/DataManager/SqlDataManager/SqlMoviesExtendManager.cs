using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ImdbSystem
{
	public class SqlMoviesExtendManager : SqlDataBase, IMoviesExtendRepository
	{
		public List<MovieExtendModel> GetAllMovies(string userID)
		{
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			DataTable dt = new DataTable();
			List<MovieExtendModel> arrMovie = new List<MovieExtendModel>();

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsSql.GetAllMovies(userID));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsSql.GetByWord(word, userID));
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


			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsSql.GetById(imdbID, userID));
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

			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsSql.GetByTitle(title, userID));
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
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsSql.AddMovie(movieModel));
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
			using (SqlCommand command = new SqlCommand())
			{
				dt = GetMultipleQuery(MovieExtendStringsSql.UpdateMovie(movieModel));
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
			using (SqlCommand command = new SqlCommand())
			{
				i = ExecuteNonQuery(MovieExtendStringsSql.DeleteMovie(imdbID, userID));
			}
			return i;
		}
	}
}
