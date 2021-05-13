using System.Data.SqlClient;

namespace ImdbSystem
{
	static public class MovieStringsSql
	{
		static private string queryMoviesString = "SELECT * from Movies where userID=@userID";
		static private string queryMoviesByIdString = "SELECT * from Movies where movieImdbID=@movieImdbID and userID=@userID";
		static private string queryMoviesByWordString = "SELECT * from Movies where movieTitle LIKE '%' + @word + '%' and userID=@userID;";
		static private string queryMoviesByTitleString = "SELECT * from Movies where movieTitle=@movieTitle and userID=@userID;";
		static private string queryMoviesPost = "INSERT INTO Movies (movieImdbID, movieTitle, moviePoster, movieYear, userID) VALUES (@movieImdbID, @movieTitle, @moviePoster, @movieYear, @userID); SELECT * FROM Movies WHERE movieImdbID = @movieImdbID and userID=@userID;";
		static private string queryMoviesUpdate = "UPDATE Movies SET movieImdbID = @movieImdbID, movieTitle = @movieTitle, moviePoster = @moviePoster, movieYear = @movieYear, userID = @userID WHERE movieImdbID = @movieImdbID and userID = @userID; SELECT * FROM Movies WHERE movieImdbID = @movieImdbID and userID=@userID;";
		static private string queryMoviesDelete = "DELETE FROM Movies WHERE movieImdbID=@movieImdbID and userID=@userID";

		static private string procedureMoviesString = "EXEC GetAllMovies @userID;";
		static private string procedureMoviesByIdString = "EXEC GetById @movieImdbID, @userID;";
		static private string procedureMoviesByWordString = "EXEC GetByWord @word, @userID;";
		static private string procedureMoviesByTitleString = "EXEC GetByTitle @movieTitle, @userID";
		static private string procedureMoviesPost = "EXEC AddMovie @movieImdbID, @movieTitle, @moviePoster, @movieYear, @userID;";
		static private string procedureMoviesUpdate = "EXEC UpdateMovie @movieImdbID, @movieTitle, @moviePoster, @movieYear, @userID;";
		static private string procedureMoviesDelete = "EXEC DeleteMovie @movieImdbID, @userID";

		static public SqlCommand GetAllMovies(string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userID, queryMoviesString);
			else
				return CreateSqlCommand(userID, procedureMoviesString);
		}

		static public SqlCommand GetById(string imdbID, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(imdbID, userID, queryMoviesByIdString);
			else
				return CreateSqlCommandId(imdbID, userID, procedureMoviesByIdString);
		}

		static public SqlCommand GetByWord(string word, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandWord(word, userID, queryMoviesByWordString);
			else
				return CreateSqlCommandWord(word, userID, procedureMoviesByWordString);
		}

		static public SqlCommand GetByTitle(string title, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandTitle(title, userID, queryMoviesByTitleString);
			else
				return CreateSqlCommandTitle(title, userID, procedureMoviesByTitleString);
		}

		static public SqlCommand AddMovie(MovieModel movieModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(movieModel, queryMoviesPost);
			else
				return CreateSqlCommand(movieModel, procedureMoviesPost);
		}

		static public SqlCommand UpdateMovie(MovieModel movieModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(movieModel, queryMoviesUpdate);
			else
				return CreateSqlCommand(movieModel, procedureMoviesUpdate);
		}

		static public SqlCommand DeleteMovie(string imdbID, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(imdbID, userID, queryMoviesDelete);
			else
				return CreateSqlCommandId(imdbID, userID, procedureMoviesDelete);
		}

		static private SqlCommand CreateSqlCommand(MovieModel movie, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@movieImdbID", movie.imdbID);
			command.Parameters.AddWithValue("@movieTitle", movie.title);
			command.Parameters.AddWithValue("@moviePoster", movie.poster);
			command.Parameters.AddWithValue("@movieYear", movie.year);
			command.Parameters.AddWithValue("@userID", movie.userID);

			return command;
		}



		static private SqlCommand CreateSqlCommandId(string imdbID, string userID, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@movieImdbID", imdbID);
			command.Parameters.AddWithValue("@userID", userID);

			return command;
		}

		static private SqlCommand CreateSqlCommandTitle(string title, string userID, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@movieTitle", title);
			command.Parameters.AddWithValue("@userID", userID);

			return command;
		}

		static private SqlCommand CreateSqlCommandWord(string word, string userID, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);

			command.Parameters.AddWithValue("@word", word);
			command.Parameters.AddWithValue("@userID", userID);

			return command;
		}

		static private SqlCommand CreateSqlCommand(string userID, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);
			command.Parameters.AddWithValue("@userID", userID);
			return command;
		}
	}
}
