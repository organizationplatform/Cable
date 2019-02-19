﻿using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.MongoDb.Repositories;
using MongoDB.Driver;

namespace WorkFlowTaskSystem.MongoDb
{
   public static class MongoRepositoryExtensions
    {
        public static IQueryable<TEntity> GetPage<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, IMongoQuery query, int skip, int limit, out long count)
            where TEntity : class, IEntity<TPrimaryKey>
        {
          var _repository = repository as WorkFlowTaskRepositoryBase<TEntity, TPrimaryKey>;
          return  _repository.GetPage(query, skip, limit, out count);

        }
        public static IQueryable<TEntity> GetPage<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, int skip, int limit, out long count)
            where TEntity : class, IEntity<TPrimaryKey>
        {
          var _repository = repository as WorkFlowTaskRepositoryBase<TEntity, TPrimaryKey>;
          return _repository.GetPage(skip, limit, out count);
     
        }
        public static TEntity Get<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, IMongoQuery query)
            where TEntity : class, IEntity<TPrimaryKey>
        {
          var _repository = repository as WorkFlowTaskRepositoryBase<TEntity, TPrimaryKey>;
          return _repository.Get(query);
     
        }
     
        public static List<TEntity> GetList<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, IMongoQuery query)
            where TEntity : class, IEntity<TPrimaryKey>
        {
          var _repository = repository as WorkFlowTaskRepositoryBase<TEntity, TPrimaryKey>;
          return _repository.GetList(query);
     
        }
        public static void RealDelete<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, TPrimaryKey idKey)
            where TEntity : class, IEntity<TPrimaryKey>
        {
          var _repository = repository as WorkFlowTaskRepositoryBase<TEntity, TPrimaryKey>;
           _repository.RealDelete(idKey);
     
        }
        public static void RealDeleteAll<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository)
            where TEntity : class, IEntity<TPrimaryKey>
        {
          var _repository = repository as WorkFlowTaskRepositoryBase<TEntity, TPrimaryKey>;
           _repository.RealDeleteAll();
     
        }
        public static void InsertBatch<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, IEnumerable<TEntity> list)
            where TEntity : class, IEntity<TPrimaryKey>
        {
          var _repository = repository as WorkFlowTaskRepositoryBase<TEntity, TPrimaryKey>;
           _repository.InsertBatch(list);
     
        }
    }
}
