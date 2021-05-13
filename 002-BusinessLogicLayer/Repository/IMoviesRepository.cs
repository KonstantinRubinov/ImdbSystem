using System.Collections.Generic;

namespace ImdbSystem
{
	public interface IMoviesRepository
	{
		List<MovieModel> GetAllMovies(string userID);
		List<MovieModel> GetByWord(string word, string userID);
		MovieModel GetById(string imdbID, string userID);
		MovieModel GetByTitle(string title, string userID);
		MovieModel AddMovie(MovieModel movieModel);
		MovieModel UpdateMovie(MovieModel movieModel);
		int DeleteMovie(string imdbID, string userID);
	}
}
