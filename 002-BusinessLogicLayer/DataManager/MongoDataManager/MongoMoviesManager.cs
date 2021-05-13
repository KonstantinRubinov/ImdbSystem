using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ImdbSystem
{
	public class MongoMoviesManager : IMoviesRepository
	{
		private readonly IMongoCollection<MovieModel> _movies;

		public MongoMoviesManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_movies = database.GetCollection<MovieModel>(ConnectionStrings.MoviesCollectionName);
		}


		public List<MovieModel> GetAllMovies(string userID)
		{
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _movies.Find<MovieModel>(movie => movie.userID.Equals(userID)).Project(m => new MovieModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year
			}).ToList();
		}

		public List<MovieModel> GetByWord(string word, string userID)
		{
			if (word.Equals(string.Empty) || word.Equals(""))
				throw new ArgumentOutOfRangeException();

			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _movies.Find<MovieModel>(movie => movie.title.Contains(word) && movie.userID.Equals(userID)).Project(m => new MovieModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year
			}).ToList();
		}

		public MovieModel GetById(string imdbID, string userID)
		{
			if (imdbID.Equals(string.Empty) || imdbID.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _movies.Find<MovieModel>(movie => movie.imdbID.Equals(imdbID) && movie.userID.Equals(userID)).Project(m => new MovieModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year
			}).FirstOrDefault();
		}

		public MovieModel GetByTitle(string title, string userID)
		{
			if (title.Equals(string.Empty) || title.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _movies.Find<MovieModel>(movie => movie.title.Equals(title) && movie.userID.Equals(userID)).Project(m => new MovieModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year
			}).FirstOrDefault();
		}


		public MovieModel AddMovie(MovieModel movieModel)
		{
			if (!_movies.Find<MovieModel>(movie => movie.imdbID.Equals(movieModel.imdbID) && movie.userID.Equals(movieModel.userID)).Any())
			{
				_movies.InsertOne(movieModel);
			}

			return _movies.Find<MovieModel>(movie => movie.imdbID.Equals(movieModel.imdbID) && movie.userID.Equals(movieModel.userID)).Project(m => new MovieModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year
			}).FirstOrDefault();
		}


		public MovieModel UpdateMovie(MovieModel movieModel)
		{
			_movies.ReplaceOne(movie => movie.imdbID.Equals(movieModel.imdbID) && movie.userID.Equals(movieModel.userID), movieModel);
			return _movies.Find<MovieModel>(movie => movie.imdbID.Equals(movieModel.imdbID) && movie.userID.Equals(movieModel.userID)).Project(m => new MovieModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year
			}).FirstOrDefault();
		}


		public int DeleteMovie(string imdbID, string userID)
		{
			_movies.DeleteOne(movie => movie.imdbID.Equals(imdbID) && movie.userID.Equals(userID));
			return 1;
		}
	}
}
