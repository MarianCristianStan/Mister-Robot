using System.Linq.Expressions;
using Mister_Robot.Models;
using Mister_Robot.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mister_Robot.Repositories
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected MisterRobotContext _MisterRobotContext { get; set; }

		public RepositoryBase(MisterRobotContext MisterRobotContext)
		{
			this._MisterRobotContext = MisterRobotContext;
		}

		public IQueryable<T> FindAll()
		{
			return this._MisterRobotContext.Set<T>().AsNoTracking();
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			return this._MisterRobotContext.Set<T>().Where(expression).AsNoTracking();
		}

		public void Create(T entity)
		{
			this._MisterRobotContext.Set<T>().Add(entity);
		}

		public void Update(T entity)
		{
			this._MisterRobotContext.Set<T>().Update(entity);
		}

		public void Delete(T entity)
		{
			this._MisterRobotContext.Set<T>().Remove(entity);
		}
	}
}
