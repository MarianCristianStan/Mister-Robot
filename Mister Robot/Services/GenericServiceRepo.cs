using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Mister_Robot.Repositories.Interfaces;
using Mister_Robot.Services.Interfaces;

namespace Mister_Robot.Services
{
	public class GenericServiceRepo<T> : IGenericServiceRepo<T> where T : class
	{
		protected readonly IRepositoryBase<T> _repository;
		protected readonly IRepositoryWrapper _repositoryWrapper;

		public GenericServiceRepo(IRepositoryBase<T> repository, IRepositoryWrapper repositoryWrapper)
		{
			_repository = repository;
			_repositoryWrapper = repositoryWrapper;
		}

		public List<T> GetAll()
		{
			return _repository.FindAll().ToList();
		}

		public T GetById(int id)
		{
			// Get the primary key name dynamically for the entity type
			var primaryKeyProperty = typeof(T)
				 .GetProperties()
				 .FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute)));

			if (primaryKeyProperty == null)
			{
				throw new InvalidOperationException($"No primary key defined for entity {typeof(T).Name}");
			}

			var primaryKeyName = primaryKeyProperty.Name;

			// Find the entity by the primary key
			return _repository.FindByCondition(e => EF.Property<int>(e, primaryKeyName) == id).FirstOrDefault();
		}


		public void Add(T entity)
		{
			_repository.Create(entity);
			_repositoryWrapper.Save();
		}

		public void Update(T entity)
		{
			_repository.Update(entity);
			_repositoryWrapper.Save();
		}

		public void Delete(int id)
		{
			
			var primaryKeyProperty = typeof(T)
				 .GetProperties()
				 .FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute)));

			if (primaryKeyProperty == null)
			{
				throw new InvalidOperationException($"No primary key defined for entity {typeof(T).Name}");
			}

			var primaryKeyName = primaryKeyProperty.Name;

			
			var entity = _repository.FindByCondition(e => EF.Property<int>(e, primaryKeyName) == id).FirstOrDefault();

			if (entity != null)
			{
				_repository.Delete(entity);
				_repositoryWrapper.Save();
			}
			else
			{
				throw new ArgumentException($"Entity with ID {id} not found.");
			}
		}

	}
}
