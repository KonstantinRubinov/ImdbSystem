using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ImdbSystem
{
	public class SqlDataBase
	{
		static private SqlConnectionStringBuilder builder;
		static private SqlConnection connection = null;
		static private SqlCommand command = null;
		static private SqlDataReader reader = null;

		static private void setConnectionString(string dataSource = ".", string initialCatalog = "ImdbFavorites", bool integratedSecurity = true)
		{
			builder = new SqlConnectionStringBuilder();
			builder.DataSource = dataSource;
			builder.InitialCatalog = initialCatalog;
			builder.IntegratedSecurity = integratedSecurity;
		}

		static private string getConnectionString()
		{
			if (builder == null)
			{
				builder = new SqlConnectionStringBuilder();
				builder.DataSource = ".";
				builder.InitialCatalog = "ImdbFavorites";
				builder.IntegratedSecurity = true;
			}
			return builder.ConnectionString;
		}

		static private SqlConnection getConnection()
		{
			if (connection == null)
			{
				Debug.WriteLine("Create connection");
				connection = new SqlConnection(getConnectionString());
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


		static public DataTable GetMultipleQuery(SqlCommand sqlCommand)
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

		static public int ExecuteNonQuery(SqlCommand sqlCommand)
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