using ImdbSystem.EntityDataBase.SqlEntityDataBase;
using System;

namespace ImdbSystem
{
	public class EntityBaseManager //: IDisposable
	{
		protected ImdbFavoritesEntities DB = new ImdbFavoritesEntities();

		public void Dispose()
		{
			DB.Dispose();
		}
	}
}
