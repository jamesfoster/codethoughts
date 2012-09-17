namespace CodeThoughts.Data.Tests
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using NUnit.Framework;

	public class InMemoryDbSet<T> : IDbSet<T> where T : class
	{
		readonly HashSet<T> _set;
		readonly IQueryable<T> _queryableSet;
		Func<T, int> _idGetter; 
		Action<T, int> _idSetter; 

		public List<string> Includes { get; set; }

		public InMemoryDbSet() : this(Enumerable.Empty<T>()) { }

		public InMemoryDbSet(IEnumerable<T> entities)
		{
			Includes = new List<string>();
			_set = new HashSet<T>();

			foreach (var entity in entities)
			{
				_set.Add(entity);
			}

			_queryableSet = _set.AsQueryable();

			GetIdAccessors(typeof (T));
		}

		void GetIdAccessors(Type type)
		{
			var property = type.GetProperty("Id");

			_idGetter = o => (int)property.GetValue(o, null);
			_idSetter = (o,v) => property.SetValue(o, v, null);
		}

		public T Add(T entity)
		{
			var nextId = _set.Any() ? _set.Max(_idGetter) + 1 : 1;

			_idSetter(entity, nextId);

			_set.Add(entity);
			return entity;
		}

		public T Attach(T entity)
		{
			_set.Add(entity);
			return entity;
		}

		public IQueryable<T> Include(string path)
		{
			Includes.Add(path);

			return _queryableSet;
		}

		public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
		{
			throw new NotImplementedException();
		}

		public T Create()
		{
			throw new NotImplementedException();
		}

		public T Find(params object[] keyValues)
		{
			Assert.That(keyValues, Has.Length.EqualTo(1));
			Assert.That(keyValues[0], Is.TypeOf<int>());

			var id = (int)keyValues[0];
			return _set.FirstOrDefault(o => _idGetter(o) == id);
		}

		public System.Collections.ObjectModel.ObservableCollection<T> Local
		{
			get { throw new NotImplementedException(); }
		}

		public T Remove(T entity)
		{
			_set.Remove(entity);
			return entity;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _set.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public Type ElementType
		{
			get { return _queryableSet.ElementType; }
		}

		public System.Linq.Expressions.Expression Expression
		{
			get { return _queryableSet.Expression; }
		}

		public IQueryProvider Provider
		{
			get { return _queryableSet.Provider; }
		}
	}
}