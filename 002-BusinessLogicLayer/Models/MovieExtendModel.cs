using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace ImdbSystem
{
	[DataContract]
	public class MovieExtendModel : MovieModel
	{
		private string _plot;
		private string _website;
		private string _rated;
		private float _imdbRating;
		private bool _seen;

		[DataMember]
		public string plot
		{
			get { return _plot; }
			set { _plot = value; }
		}

		[DataMember]
		public string website
		{
			get { return _website; }
			set { _website = value; }
		}

		[DataMember]
		public string rated
		{
			get { return _rated; }
			set { _rated = value; }
		}

		[DataMember]
		public float imdbRating
		{
			get { return _imdbRating; }
			set { _imdbRating = value; }
		}

		[DataMember]
		public bool seen
		{
			get { return _seen; }
			set { _seen = value; }
		}

		public override string ToString()
		{
			return
				imdbID + " " +
				title + " " +
				poster + " " +
				year + " " +
				userID + " " +
				plot + " " +
				website + " " +
				rated + " " +
				imdbRating + " " +
				seen;
		}


		public static MovieExtendModel ToObject(DataRow reader)
		{
			MovieExtendModel movieExtendModel = new MovieExtendModel();
			movieExtendModel.imdbID = reader[0].ToString();
			movieExtendModel.title = reader[1].ToString();
			movieExtendModel.poster = reader[2].ToString();
			movieExtendModel.userID = reader[3].ToString();
			movieExtendModel.year = int.Parse(reader[4].ToString());
			movieExtendModel.plot = reader[5].ToString();
			movieExtendModel.website = reader[6].ToString();
			movieExtendModel.rated = reader[7].ToString();
			movieExtendModel.imdbRating = float.Parse(reader[8].ToString());

			try
			{
				movieExtendModel.seen = int.Parse(reader[9].ToString()) > 0;
			} catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mysql seen: " + ex.Message);
			}

			try
			{
				movieExtendModel.seen = bool.Parse(reader[9].ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("trying to parse mssql seen: " + ex.Message);
			}

			Debug.WriteLine("MovieExtendModel:" + movieExtendModel.ToString());
			return movieExtendModel;
		}
	}
}
