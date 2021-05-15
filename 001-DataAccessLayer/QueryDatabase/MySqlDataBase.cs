using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;

namespace ImdbSystem
{
	public class MySqlDataBase
	{
		static private MySqlConnectionStringBuilder builder;
		static private MySqlConnection connection = null;
		static private MySqlCommand command = null;
		static private MySqlDataReader reader = null;

		static private void setConnectionString(string server = "localhost", string userId = "root", uint port = 3306, bool persistSecurityInfo = true, string password = "Rk14101981", string database = "imdbfavorites", bool useAffectedRows = true)
		{
			builder = new MySqlConnectionStringBuilder();
			builder.Server = server;
			builder.UserID = userId;
			builder.Port = port;
			builder.PersistSecurityInfo = persistSecurityInfo;
			builder.Password = password;
			builder.Database = database;
			builder.UseAffectedRows = useAffectedRows;
		}

		static private string getConnectionString()
		{
			if (builder == null)
			{
				builder = new MySqlConnectionStringBuilder();
				builder.Server = "localhost";
				builder.UserID = "root";
				builder.Port = 3306;
				builder.PersistSecurityInfo = true;
				builder.Password = "Rk14101981";
				builder.Database = "imdbfavorites";
				builder.UseAffectedRows = true;
			}
			return builder.ConnectionString;
		}

		static private MySqlConnection getConnection()
		{
			if (connection == null)
			{
				Debug.WriteLine("Create connection");
				connection = new MySqlConnection(getConnectionString());
			}
			Debug.WriteLine("Return connection");
			return connection;
		}

		static public void Connect()
		{
			if (connection.State != ConnectionState.Open)
			{
				Debug.WriteLine("Open connection");
				connection.Open();
			}
		}

		static public void Disconnect()
		{
			Debug.WriteLine("Close connection");
			connection.Close();
		}


		static public DataTable GetMultipleQuery(MySqlCommand sqlCommand)
		{
			DataTable datatable = new DataTable();
			getConnection();
			lock (connection)
			{
				command = sqlCommand;
				Connect();
				command.Connection = connection;
				try
				{
					reader = command.ExecuteReader();
					datatable.Load(reader);
					Debug.WriteLine("Create data reader");
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Create data reader Exception: " + ex.Message);
					if (reader != null)
					{
						reader.Close();
					}
				}
				finally
				{
					Disconnect();
				}
			}
			return datatable;
		}

		static public int ExecuteNonQuery(MySqlCommand sqlCommand)
		{
			int i = 0;
			getConnection();
			lock (connection)
			{
				command = sqlCommand;
				Connect();
				command.Connection = connection;
				try
				{
					i = command.ExecuteNonQuery();
					Debug.WriteLine("Create NonQuery: " + i);
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Create NonQuery :" + ex.Message);
				}
				finally
				{
					Disconnect();
				}
			}
			return i;
		}
	}
}