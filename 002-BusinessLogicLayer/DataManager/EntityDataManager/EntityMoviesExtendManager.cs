using ImdbSystem.EntityDataBase.SqlEntityDataBase;
using System.Collections.Generic;
using System.Linq;

namespace ImdbSystem
{
	public class EntityMoviesExtendManager : EntityBaseManager, IMoviesExtendRepository
	{
		public List<MovieExtendModel> GetAllMovies(string userID)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.MOVIEEXTENDS.Where(m => m.MOVy.userID.Equals(userID)).Select(m => new MovieExtendModel
				{
					userID = m.MOVy.userID,
					title = m.MOVy.movieTitle,
					poster = m.MOVy.moviePoster,
					year = m.MOVy.movieYear,

					imdbID = m.movieImdbID,
					plot = m.moviePlot,
					website = m.movieUrl,
					rated = m.movieRated,
					imdbRating = m.movieImdbRating,
					seen = m.movieSeen
				}).ToList();
			}
			else
			{
				return DB.GetAllExtendedMovies(userID).Select(m => new MovieExtendModel
				{
					userID = m.userID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear,

					imdbID = m.movieImdbID,
					plot = m.moviePlot,
					website = m.movieUrl,
					rated = m.movieRated,
					imdbRating = m.movieImdbRating.Value,
					seen = m.movieSeen.Value
				}).ToList();
			}
		}

		public List<MovieExtendModel> GetByWord(string word, string userID)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.MOVIEEXTENDS.Where(m => m.MOVy.movieTitle.Contains(word) && m.MOVy.userID.Equals(userID)).Select(m => new MovieExtendModel
				{
					userID = m.MOVy.userID,
					title = m.MOVy.movieTitle,
					poster = m.MOVy.moviePoster,
					year = m.MOVy.movieYear,

					imdbID = m.movieImdbID,
					plot = m.moviePlot,
					website = m.movieUrl,
					rated = m.movieRated,
					imdbRating = m.movieImdbRating,
					seen = m.movieSeen
				}).ToList();
			}
			else
			{
				return DB.GetByWordExtendedMovies(word, userID).Select(m => new MovieExtendModel
				{
					userID = m.userID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear,

					imdbID = m.movieImdbID,
					plot = m.moviePlot,
					website = m.movieUrl,
					rated = m.movieRated,
					imdbRating = m.movieImdbRating.Value,
					seen = m.movieSeen.Value
				}).ToList();
			}
		}

		public MovieExtendModel GetById(string imdbID, string userID)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.MOVIEEXTENDS.Where(m => m.movieImdbID.Equals(imdbID) && m.MOVy.userID.Equals(userID)).Select(m => new MovieExtendModel
				{
					userID = m.MOVy.userID,
					title = m.MOVy.movieTitle,
					poster = m.MOVy.moviePoster,
					year = m.MOVy.movieYear,

					imdbID = m.movieImdbID,
					plot = m.moviePlot,
					website = m.movieUrl,
					rated = m.movieRated,
					imdbRating = m.movieImdbRating,
					seen = m.movieSeen
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetByIdExtendedMovies(imdbID, userID).Select(m => new MovieExtendModel
				{
					userID = m.userID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear,

					imdbID = m.movieImdbID,
					plot = m.moviePlot,
					website = m.movieUrl,
					rated = m.movieRated,
					imdbRating = m.movieImdbRating.Value,
					seen = m.movieSeen.Value
				}).SingleOrDefault();
			}
		}

		public MovieExtendModel GetByTitle(string title, string userID)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.MOVIEEXTENDS.Where(m => m.MOVy.movieTitle.Equals(title) && m.MOVy.userID.Equals(userID)).Select(m => new MovieExtendModel
				{
					userID = m.MOVy.userID,
					title = m.MOVy.movieTitle,
					poster = m.MOVy.moviePoster,
					year = m.MOVy.movieYear,

					imdbID = m.movieImdbID,
					plot = m.moviePlot,
					website = m.movieUrl,
					rated = m.movieRated,
					imdbRating = m.movieImdbRating,
					seen = m.movieSeen
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetByTitleExtendedMovies(title, userID).Select(m => new MovieExtendModel
				{
					userID = m.userID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear,

					imdbID = m.movieImdbID,
					plot = m.moviePlot,
					website = m.movieUrl,
					rated = m.movieRated,
					imdbRating = m.movieImdbRating.Value,
					seen = m.movieSeen.Value
				}).SingleOrDefault();
			}
		}

		public MovieExtendModel AddMovie(MovieExtendModel movieModel)
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

				MOVIEEXTEND movieExtend = new MOVIEEXTEND
				{
					movieImdbID = movieModel.imdbID,
					moviePlot = movieModel.plot,
					movieUrl = movieModel.website,
					movieRated = movieModel.rated,
					movieImdbRating = movieModel.imdbRating,
					movieSeen = movieModel.seen,
					userID = movieModel.userID
				};
				//movieExtend.MOVy = movie;

				DB.MOVIES.Add(movie);
				DB.MOVIEEXTENDS.Add(movieExtend);
				DB.SaveChanges();
				return GetById(movie.movieImdbID, movie.userID);
			}
			else
			{
				return DB.AddExtendedMovie(movieModel.imdbID, movieModel.title, movieModel.poster, movieModel.year, movieModel.userID, movieModel.plot, movieModel.website, movieModel.rated, movieModel.imdbRating, movieModel.seen).Select(m => new MovieExtendModel
				{
					userID = m.userID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear,

					imdbID = m.movieImdbID,
					plot = m.moviePlot,
					website = m.movieUrl,
					rated = m.movieRated,
					imdbRating = m.movieImdbRating.Value,
					seen = m.movieSeen.Value
				}).SingleOrDefault();
			}
		}

		public MovieExtendModel UpdateMovie(MovieExtendModel movieModel)
		{
			if (GlobalVariable.queryType == 0)
			{
				MOVy movie = DB.MOVIES.Where(m => m.movieImdbID.Equals(movieModel.imdbID)).SingleOrDefault();
				MOVIEEXTEND movieExtend = DB.MOVIEEXTENDS.Where(m => m.movieImdbID.Equals(movieModel.imdbID)).SingleOrDefault();
				if (movie == null)
					return null;
				movie.userID = movieModel.userID;
				movie.movieImdbID = movieModel.imdbID;
				movie.movieTitle = movieModel.title;
				movie.moviePoster = movieModel.poster;
				movie.movieYear = movieModel.year;

				movieExtend.movieImdbID = movieModel.imdbID;
				movieExtend.moviePlot = movieModel.plot;
				movieExtend.movieUrl = movieModel.website;
				movieExtend.movieRated = movieModel.rated;
				movieExtend.movieImdbRating = movieModel.imdbRating;
				movieExtend.movieSeen = movieModel.seen;
				movieExtend.userID = movieModel.userID;
				DB.SaveChanges();
				return GetById(movie.movieImdbID, movie.userID);
			}
			else
			{
				return DB.UpdateExtendedMovie(movieModel.imdbID, movieModel.title, movieModel.poster, movieModel.year, movieModel.userID, movieModel.plot, movieModel.website, movieModel.rated, movieModel.imdbRating, movieModel.seen).Select(m => new MovieExtendModel
				{
					userID = m.userID,
					title = m.movieTitle,
					poster = m.moviePoster,
					year = m.movieYear,

					imdbID = m.movieImdbID,
					plot = m.moviePlot,
					website = m.movieUrl,
					rated = m.movieRated,
					imdbRating = m.movieImdbRating.Value,
					seen = m.movieSeen.Value
				}).SingleOrDefault();
			}
		}

		public int DeleteMovie(string imdbID, string userID)
		{
			if (GlobalVariable.queryType == 0)
			{
				MOVy movie = DB.MOVIES.Where(m => m.movieImdbID.Equals(imdbID) && m.userID.Equals(userID)).SingleOrDefault();
				MOVIEEXTEND movieExtend = DB.MOVIEEXTENDS.Where(m => m.movieImdbID.Equals(imdbID) && m.MOVy.userID.Equals(userID)).SingleOrDefault();
				DB.MOVIES.Attach(movie);
				DB.MOVIEEXTENDS.Attach(movieExtend);
				if (movie == null)
					return 0;
				DB.MOVIEEXTENDS.Remove(movieExtend);
				DB.MOVIES.Remove(movie);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteExtendedMovie(imdbID, userID);
			}
		}
	}
}