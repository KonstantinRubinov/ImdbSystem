using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ImdbSystem
{
	public class MongoMoviesExtendManager : IMoviesExtendRepository
	{
		private readonly IMongoCollection<MovieExtendModel> _movies;

		public MongoMoviesExtendManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_movies = database.GetCollection<MovieExtendModel>(ConnectionStrings.MoviesCollectionName);
		}

		public List<MovieExtendModel> GetAllMovies(string userID)
		{
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _movies.Find<MovieExtendModel>(movie => movie.userID.Equals(userID)).Project(m => new MovieExtendModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year,

				plot = m.plot,
				website = m.website,
				rated = m.rated,
				imdbRating = m.imdbRating,
				seen = m.seen
			}).ToList();
		}

		public List<MovieExtendModel> GetByWord(string word, string userID)
		{
			if (word.Equals(string.Empty) || word.Equals(""))
				throw new ArgumentOutOfRangeException();

			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _movies.Find<MovieExtendModel>(movie => movie.title.Contains(word) && movie.userID.Equals(userID)).Project(m => new MovieExtendModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year,

				plot = m.plot,
				website = m.website,
				rated = m.rated,
				imdbRating = m.imdbRating,
				seen = m.seen
			}).ToList();
		}

		public MovieExtendModel GetById(string imdbID, string userID)
		{
			if (imdbID.Equals(string.Empty) || imdbID.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _movies.Find<MovieExtendModel>(movie => movie.imdbID.Equals(imdbID) && movie.userID.Equals(userID)).Project(m => new MovieExtendModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year,

				plot = m.plot,
				website = m.website,
				rated = m.rated,
				imdbRating = m.imdbRating,
				seen = m.seen
			}).FirstOrDefault();
		}

		public MovieExtendModel GetByTitle(string title, string userID)
		{
			if (title.Equals(string.Empty) || title.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (userID.Equals(string.Empty) || userID.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _movies.Find<MovieExtendModel>(movie => movie.title.Equals(title) && movie.userID.Equals(userID)).Project(m => new MovieExtendModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year,

				plot = m.plot,
				website = m.website,
				rated = m.rated,
				imdbRating = m.imdbRating,
				seen = m.seen
			}).FirstOrDefault();
		}


		public MovieExtendModel AddMovie(MovieExtendModel movieModel)
		{
			if (!_movies.Find<MovieExtendModel>(movie => movie.imdbID.Equals(movieModel.imdbID) && movie.userID.Equals(movieModel.userID)).Any())
			{
				_movies.InsertOne(movieModel);
			}
			_movies.InsertOne(movieModel);
			return _movies.Find<MovieExtendModel>(movie => movie.imdbID.Equals(movieModel.imdbID) && movie.userID.Equals(movieModel.userID)).Project(m => new MovieExtendModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year,

				plot = m.plot,
				website = m.website,
				rated = m.rated,
				imdbRating = m.imdbRating,
				seen = m.seen
			}).FirstOrDefault();
		}


		public MovieExtendModel UpdateMovie(MovieExtendModel movieModel)
		{
			_movies.ReplaceOne(movie => movie.imdbID.Equals(movieModel.imdbID) && movie.userID.Equals(movieModel.userID), movieModel);
			return _movies.Find<MovieExtendModel>(movie => movie.imdbID.Equals(movieModel.imdbID) && movie.userID.Equals(movieModel.userID)).Project(m => new MovieExtendModel
			{
				userID = m.userID,
				imdbID = m.imdbID,
				title = m.title,
				poster = m.poster,
				year = m.year,

				plot = m.plot,
				website = m.website,
				rated = m.rated,
				imdbRating = m.imdbRating,
				seen = m.seen
			}).FirstOrDefault();
		}


		public int DeleteMovie(string imdbID, string userID)
		{
			_movies.DeleteOne(movie => movie.imdbID.Equals(imdbID) && movie.userID.Equals(userID));
			return 1;
		}
	}
}
