using ImdbSystem.EntityDataBase.SqlEntityDataBase;
using System.Collections.Generic;
using System.Linq;

namespace ImdbSystem
{
	public class EntityMoviesManager : EntityBaseManager, IMoviesRepository
	{
		public List<MovieModel> GetAllMovies(string userID)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.MOVIES.Where(m => m.userID.Equals(userID)).Select(m => new MovieModel
				{
					userID = m.userID,
					imdbID = m.movieImdbID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear
				}).ToList();
			}
			else
			{
				return DB.GetAllMovies(userID).Select(m => new MovieModel
				{
					userID = m.userID,
					imdbID = m.movieImdbID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear
				}).ToList();
			}
		}

		public List<MovieModel> GetByWord(string word, string userID)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.MOVIES.Where(m => m.movieTitle.Contains(word) && m.userID.Equals(userID)).Select(m => new MovieModel
				{
					userID = m.userID,
					imdbID = m.movieImdbID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear
				}).ToList();
			}
			else
			{
				return DB.GetByWord(word, userID).Select(m => new MovieModel
				{
					userID = m.userID,
					imdbID = m.movieImdbID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear
				}).ToList();
			}
		}

		public MovieModel GetById(string imdbID, string userID)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.MOVIES.Where(m => m.movieImdbID.Equals(imdbID) && m.userID.Equals(userID)).Select(m => new MovieModel
				{
					userID = m.userID,
					imdbID = m.movieImdbID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetById(imdbID, userID).Select(m => new MovieModel
				{
					userID = m.userID,
					imdbID = m.movieImdbID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear
				}).SingleOrDefault();
			}
		}

		public MovieModel GetByTitle(string title, string userID)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.MOVIES.Where(m => m.movieTitle.Equals(title) && m.userID.Equals(userID)).Select(m => new MovieModel
				{
					userID = m.userID,
					imdbID = m.movieImdbID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetByTitle(title, userID).Select(m => new MovieModel
				{
					userID = m.userID,
					imdbID = m.movieImdbID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear
				}).SingleOrDefault();
			}
		}

		public MovieModel AddMovie(MovieModel movieModel)
		{
			if (GlobalVariable.queryType == 0)
			{
				MOVy movie = new MOVy
				{
					userID = movieModel.userID,
					movieImdbID = movieModel.imdbID,
					movieTitle = movieModel.title,
					moviePoster = movieModel.poster,
					movieYear = movieModel.year
				};
				DB.MOVIES.Add(movie);
				DB.SaveChanges();
				return GetById(movie.movieImdbID, movie.userID);
			}
			else
			{
				return DB.AddMovie(movieModel.imdbID, movieModel.title, movieModel.poster, movieModel.year, movieModel.userID).Select(m => new MovieModel
				{
					userID = m.userID,
					imdbID = m.movieImdbID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear
				}).SingleOrDefault();
			}
		}

		public MovieModel UpdateMovie(MovieModel movieModel)
		{
			if (GlobalVariable.queryType == 0)
			{
				MOVy movie = DB.MOVIES.Where(m => m.movieImdbID.Equals(movieModel.imdbID)).SingleOrDefault();
				if (movie == null)
					return null;
				movie.userID = movieModel.userID;
				movie.movieImdbID = movieModel.imdbID;
				movie.movieTitle = movieModel.title;
				movie.moviePoster = movieModel.poster;
				movie.movieYear = movieModel.year;
				DB.SaveChanges();
				return GetById(movie.movieImdbID, movie.userID);
			}
			else
			{
				return DB.AddMovie(movieModel.imdbID, movieModel.title, movieModel.poster, movieModel.year, movieModel.userID).Select(m => new MovieModel
				{
					userID = m.userID,
					imdbID = m.movieImdbID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear
				}).SingleOrDefault();
			}
		}

		public int DeleteMovie(string imdbID, string userID)
		{
			if (GlobalVariable.queryType == 0)
			{
				MOVy movie = DB.MOVIES.Where(m => m.movieImdbID.Equals(imdbID) && m.userID.Equals(userID)).SingleOrDefault();
				DB.MOVIES.Attach(movie);
				if (movie == null)
					return 0;
				DB.MOVIES.Remove(movie);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteMovie(imdbID, userID);
			}
		}
	}
}