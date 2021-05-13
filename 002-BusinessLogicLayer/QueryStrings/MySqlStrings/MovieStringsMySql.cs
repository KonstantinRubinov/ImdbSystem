using MySql.Data.MySqlClient;

namespace ImdbSystem
{
	static public class MovieStringsMySql
	{
		static private string queryMoviesString = "SELECT * from Movies where userID=@userID";
		static private string queryMoviesByIdString = "SELECT * from Movies where movieImdbID=@movieImdbID and userID=@userID";
		static private string queryMoviesByWordString = "SELECT * from Movies where movieTitle LIKE '%' + @word + '%' and userID=@userID;";
		static private string queryMoviesByTitleString = "SELECT * from Movies where movieTitle=@movieTitle and userID=@userID;";
		static private string queryMoviesPost = "INSERT INTO Movies (movieImdbID, movieTitle, moviePoster, movieYear, userID) VALUES (@movieImdbID, @movieTitle, @moviePoster, @movieYear, @userID); SELECT * FROM Movies WHERE movieImdbID = @movieImdbID and userID=@userID;";
		static private string queryMoviesUpdate = "UPDATE Movies SET movieImdbID = @movieImdbID, movieTitle = @movieTitle, moviePoster = @moviePoster, movieYear = @movieYear, userID = @userID WHERE movieImdbID = @movieImdbID and userID = @userID; SELECT * FROM Movies WHERE movieImdbID = @movieImdbID and userID=@userID;";
		static private string queryMoviesDelete = "DELETE FROM Movies WHERE movieImdbID=@movieImdbID and userID=@userID";

		static private string procedureMoviesString = "CALL `imdbfavorites`.`GetAllMovies`(@userID);";
		static private string procedureMoviesByIdString = "CALL `imdbfavorites`.`GetById`(@movieImdbID, @userID);";
		static private string procedureMoviesByWordString = "CALL `imdbfavorites`.`GetByWord`(@word, @userID);";
		static private string procedureMoviesByTitleString = "CALL `imdbfavorites`.`GetByTitle`(@movieTitle, @userID);";
		static private string procedureMoviesPost = "CALL `imdbfavorites`.`AddMovie`(@movieImdbID, @movieTitle, @moviePoster, @movieYear, @userID);";
		static private string procedureMoviesUpdate = "CALL `imdbfavorites`.`UpdateMovie`(@movieImdbID, @movieTitle, @moviePoster, @movieYear, @userID);";
		static private string procedureMoviesDelete = "CALL `imdbfavorites`.`DeleteMovie`(@movieImdbID, @userID);";


		static public MySqlCommand GetAllMovies(string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userID, queryMoviesString);
			else
				return CreateSqlCommand(userID, procedureMoviesString);
		}

		static public MySqlCommand GetById(string imdbID, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(imdbID, userID, queryMoviesByIdString);
			else
				return CreateSqlCommandId(imdbID, userID, procedureMoviesByIdString);
		}

		static public MySqlCommand GetByWord(string word, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandWord(word, userID, queryMoviesByWordString);
			else
				return CreateSqlCommandWord(word, userID, procedureMoviesByWordString);
		}

		static public MySqlCommand GetByTitle(string title, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandTitle(title, userID, queryMoviesByTitleString);
			else
				return CreateSqlCommandTitle(title, userID, procedureMoviesByTitleString);
		}

		static public MySqlCommand AddMovie(MovieModel movieModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(movieModel, queryMoviesPost);
			else
				return CreateSqlCommand(movieModel, procedureMoviesPost);
		}

		static public MySqlCommand UpdateMovie(MovieModel movieModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(movieModel, queryMoviesUpdate);
			else
				return CreateSqlCommand(movieModel, procedureMoviesUpdate);
		}

		static public MySqlCommand DeleteMovie(string imdbID, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(imdbID, userID, queryMoviesDelete);
			else
				return CreateSqlCommandId(imdbID, userID, procedureMoviesDelete);
		}

		static private MySqlCommand CreateSqlCommand(MovieModel movie, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@movieImdbID", movie.imdbID);
			command.Parameters.AddWithValue("@movieTitle", movie.title);
			command.Parameters.AddWithValue("@moviePoster", movie.poster);
			command.Parameters.AddWithValue("@movieYear", movie.year);
			command.Parameters.AddWithValue("@userID", movie.userID);

			return command;
		}



		static private MySqlCommand CreateSqlCommandId(string imdbID, string userID, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@movieImdbID", imdbID);
			command.Parameters.AddWithValue("@userID", userID);

			return command;
		}

		static private MySqlCommand CreateSqlCommandTitle(string title, string userID, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@movieTitle", title);
			command.Parameters.AddWithValue("@userID", userID);

			return command;
		}

		static private MySqlCommand CreateSqlCommandWord(string word, string userID, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@word", word);
			command.Parameters.AddWithValue("@userID", userID);

			return command;
		}

		static private MySqlCommand CreateSqlCommand(string userID, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userID", userID);
			return command;
		}
	}
}
