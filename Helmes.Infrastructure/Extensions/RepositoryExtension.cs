using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Helmes.Domain.Interfaces;

namespace Helmes.Infrastructure.Extensions
{
	public static class RepositoryExtension
	{
		public static IQueryable<T> Where<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate)
			where T : class
		{
			return repository.Entities.Where(predicate);
		}

		public static async Task<List<T>> ToListAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate)
			where T : class
		{
			return await repository.Where(predicate).ToListAsync();
		}

		public static IOrderedQueryable<T> OrderBy<T, TKey>(this IRepository<T> repository, Expression<Func<T, TKey>> keySelector)
			where T : class
		{
			return repository.Entities.OrderBy(keySelector);
		}

		public static async Task<T> FirstOrDefaultAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate)
			where T : class
		{
			return await repository.Entities.FirstOrDefaultAsync(predicate);
		}

		public static async Task<T> SingleOrDefaultAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate)
			where T : class
		{
			return await repository.Entities.SingleOrDefaultAsync(predicate);
		}

		public static async Task<T> LastOrDefaultAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate)
			where T : class
		{
			return await repository.Entities.LastOrDefaultAsync(predicate);
		}

		public static async Task<T> SingleAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate)
			where T : class
		{
			return await repository.Entities.SingleAsync(predicate);
		}

		public static async Task<T> FirstAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate)
			where T : class
		{
			return await repository.Entities.FirstAsync(predicate);
		}

		public static async Task<bool> AnyAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate)
			where T : class
		{
			return await repository.Entities.AnyAsync(predicate);
		}

		public static async Task<int> CountAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate)
			where T : class
		{
			return await repository.Entities.CountAsync(predicate);
		}


		public static IIncludableQueryable<T, TProperty> Include<T, TProperty>(this IRepository<T> repository, Expression<Func<T, TProperty>> path)
			where T : class
		{
			return repository.Entities.Include(path);
		}

		public static IQueryable<TResult> Select<T, TResult>(this IRepository<T> repository, Expression<Func<T, TResult>> selector)
			where T : class
		{
			return repository.Entities.Select(selector);
		}

        public static DbCommand LoadCommand<T>(this IRepository<T> repository, string storedProcName)
            where T : class
		{
            var conn = repository.DbContext.Database.GetDbConnection();
            var cmd = conn.CreateCommand();

            if (repository.DbContext.Database.CurrentTransaction != null)
            {
                cmd.Transaction = repository.DbContext.Database.CurrentTransaction.GetDbTransaction();
            }

            cmd.CommandText = storedProcName;
            cmd.CommandType = System.Data.CommandType.Text;
            return cmd;
        }

		public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
			where T : class
		{
			return await query.Where(predicate).ToListAsync();
		}

		public static async Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
		{
			return await query.Where(predicate).FirstOrDefaultAsync();
		}

		public static void Load<T, TProperty>(this IRepository<T> repository, T entity, Expression<Func<T, TProperty>> propertyExpression)
			where T : class where TProperty : class
		{
			repository.DbContext.Entry(entity).Reference(propertyExpression).Load();
		}

		private static void LoadEntity<T, T1, TProperty>(this IRepository<T> repository, T1 entity, Expression<Func<T1, TProperty>> propertyExpression)
			where T : class where TProperty : class where T1 : class
		{
			repository.DbContext.Entry(entity).Reference(propertyExpression).Load();
		}

		public static void LoadEntities<T, T1, TProperty>(this IRepository<T> repository, IEnumerable<T1> entities, Expression<Func<T1, TProperty>> propertyExpression)
			where T : class where TProperty : class where T1 : class
		{
			foreach (var entity in entities)
			{
				repository.LoadEntity(entity, propertyExpression);
			}
		}
	}
}
