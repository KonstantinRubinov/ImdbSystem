using System.Collections.Generic;

namespace ImdbSystem
{
	public interface IMoviesExtendRepository
	{
		List<MovieExtendModel> GetAllMovies(string userID);
		List<MovieExtendModel> GetByWord(string word, string userID);
		MovieExtendModel GetById(string imdbID, string userID);
		MovieExtendModel GetByTitle(string title, string userID);
		MovieExtendModel AddMovie(MovieExtendModel movieModel);
		MovieExtendModel UpdateMovie(MovieExtendModel movieModel);
		int DeleteMovie(string imdbID, string userID);
	}
}
