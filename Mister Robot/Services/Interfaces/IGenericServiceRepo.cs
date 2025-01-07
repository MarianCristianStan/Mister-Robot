using System.Collections.Generic;

namespace Mister_Robot.Services.Interfaces
{
	public interface IGenericServiceRepo<T>
	{
		List<T> GetAll();
		T GetById(string id);
		void Add(T entity);
		void Update(T entity);
		void Delete(string id);
	}
}
