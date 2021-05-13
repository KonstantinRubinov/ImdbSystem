using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImdbSystem
{
	public interface IImdbRepository
	{
		Task<MovieExtendModel> GetImdbById(string userPass, string movieId, string userId, string type = "", int year = 0, string r = "json", string plot = "short");
		Task<MovieExtendModel> GetImdbByTitle(string userPass, string movieTitle, string userId, string type = "", int year = 0, string r = "json", string plot = "short");
		Task<List<MovieModel>> GetImdbByWord(string userPass, string movieWord, string userId, string type = "", int year = 0, string r = "json", int page = 0);
	}
}
