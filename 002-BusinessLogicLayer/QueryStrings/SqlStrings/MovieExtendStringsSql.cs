using System.Data.SqlClient;

namespace ImdbSystem
{
	static public class MovieExtendStringsSql
	{
		static private string queryMoviesExtendString = "SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID and Movies.userID=MOVIEEXTENDS.userID where Movies.userID=@userID;";
		static private string queryMoviesExtendByIdString = "SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen, MOVIEEXTENDS.userID From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID where Movies.movieImdbID=@movieImdbID and Movies.userID=@userID;";
		static private string queryMoviesExtendByWordString = "SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID and Movies.userID=MOVIEEXTENDS.userID where Movies.movieTitle LIKE '%' + @word + '%' and Movies.userID=@userID;";

		static private string queryMoviesExtendByTitleString = "SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen, MOVIEEXTENDS.userID From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID where Movies.movieTitle=@title and Movies.userID=@userID;";

		static private string queryMoviesExtendPost = "INSERT INTO Movies (movieImdbID, movieTitle, moviePoster, movieYear, userID) VALUES (@movieImdbID, @movieTitle, @moviePoster, @movieYear, @userID); " +
													  "INSERT INTO MOVIEEXTENDS (movieImdbID, moviePlot, movieUrl, movieRated, movieImdbRating, movieSeen, userID) VALUES (@movieImdbID, @moviePlot, @movieUrl, @movieRated, @movieImdbRating, @movieSeen, @userID);" +
													  "SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen, MOVIEEXTENDS.userID From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID where Movies.movieImdbID=@movieImdbID and Movies.userID=@userID;";

		static private string queryMoviesExtendUpdate = "UPDATE Movies SET movieImdbID = @movieImdbID, movieTitle = @movieTitle, moviePoster = @moviePoster, movieYear = @movieYear, userID = @userID WHERE movieImdbID = @movieImdbID and userID = @userID; " +
														"UPDATE MOVIEEXTENDS SET MOVIEEXTENDS.movieImdbID = @movieImdbID, MOVIEEXTENDS.moviePlot = @moviePlot, MOVIEEXTENDS.movieUrl = @movieUrl, MOVIEEXTENDS.movieRated = @movieRated, MOVIEEXTENDS.movieImdbRating = @movieImdbRating, MOVIEEXTENDS.movieSeen = @movieSeen, userID = @userID WHERE movieImdbID = @movieImdbID and userID = @userID; " +
														"SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen, MOVIEEXTENDS.userID From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID where Movies.movieImdbID=@movieImdbID and Movies.userID=@userID;";

		static private string queryMoviesExtendDelete = "DELETE FROM MOVIEEXTENDS WHERE movieImdbID = @movieImdbID and userID=@userID; " +
														"DELETE FROM Movies WHERE movieImdbID = @movieImdbID and userID=@userID;";


		static private string procedureMoviesExtendString = "EXEC GetAllExtendedMovies @userID";
		static private string procedureMoviesExtendByIdString = "EXEC GetByIdExtendedMovies @movieImdbID, @userID";
		static private string procedureMoviesExtendByWordString = "EXEC GetByWordExtendedMovies @word, @userID;";
		static private string procedureMoviesExtendByTitleString = "EXEC GetByTitleExtendedMovies @movieTitle, @userID";
		static private string procedureMoviesExtendPost = "EXEC AddExtendedMovie @movieImdbID, @movieTitle, @moviePoster, @movieYear, @userID, @moviePlot, @movieUrl, @movieRated, @movieImdbRating, @movieSeen;";
		static private string procedureMoviesExtendUpdate = "EXEC UpdateExtendedMovie @movieImdbID, @movieTitle, @moviePoster, @movieYear, @userID, @moviePlot, @movieUrl, @movieRated, @movieImdbRating, @movieSeen;";
		static private string procedureMoviesExtendDelete = "EXEC DeleteExtendedMovie @movieImdbID, @userID";


		static public SqlCommand GetAllMovies(string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userID, queryMoviesExtendString);
			else
				return CreateSqlCommand(userID, procedureMoviesExtendString);
		}

		static public SqlCommand GetById(string imdbID, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(imdbID, userID, queryMoviesExtendByIdString);
			else
				return CreateSqlCommandId(imdbID, userID, procedureMoviesExtendByIdString);
		}

		static public SqlCommand GetByWord(string word, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandWord(word, userID, queryMoviesExtendByWordString);
			else
				return CreateSqlCommandWord(word, userID, procedureMoviesExtendByWordString);
		}

		static public SqlCommand GetByTitle(string title, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandTitle(title, userID, queryMoviesExtendByTitleString);
			else
				return CreateSqlCommandTitle(title, userID, procedureMoviesExtendByTitleString);
		}

		static public SqlCommand AddMovie(MovieExtendModel movieModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(movieModel, queryMoviesExtendPost);
			else
				return CreateSqlCommand(movieModel, procedureMoviesExtendPost);
		}

		static public SqlCommand UpdateMovie(MovieExtendModel movieModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(movieModel, queryMoviesExtendUpdate);
			else
				return CreateSqlCommand(movieModel, procedureMoviesExtendUpdate);
		}

		static public SqlCommand DeleteMovie(string imdbID, string userID)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(imdbID, userID, queryMoviesExtendDelete);
			else
				return CreateSqlCommandId(imdbID, userID, procedureMoviesExtendDelete);
		}

		static private SqlCommand CreateSqlCommand(MovieExtendModel movie, string commandText)
		{
			SqlCommand command = new SqlCommand(commandText);
			command.Parameters.AddWithValue("@movieImdbID", movie.imdbID);
			command.Parameters.AddWithValue("@movieTitle", movie.title);
			command.Parameters.AddWithValue("@moviePoster", movie.poster);
			command.Parameters.AddWithValue("@movieYear", movie.year);
			command.Parameters.AddWithValue("@userId", movie.userID);
			command.Parameters.AddWithValue("@moviePlot", movie.plot);
			command.Parameters.AddWithValue("@movieUrl", movie.website);
			command.Parameters.AddWithValue("@movieRated", movie.rated);
			command.Parameters.AddWithValue("@movieImdbRating", movie.imdbRating);
			command.Parameters.AddWithValue("@movieSeen", movie.seen);
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
