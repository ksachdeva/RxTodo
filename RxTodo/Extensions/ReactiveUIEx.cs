using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Collections.Specialized;

using ReactiveUI;

namespace RxTodo
{
	public static class ReactiveUIExtensions
	{
		public static IEnumerable ToReactiveObservableList(this IReactiveCollection<TodoItem> source) => new ReactiveObservableList<TodoItem>(source);
	}

	public class ReactiveObservableList<TodoItem> : INotifyCollectionChanged, IEnumerable<TodoItem>, IDisposable
	{
		public ReactiveObservableList(IReactiveCollection<TodoItem> sourceCollection)
		{
			_collection = sourceCollection ?? new ReactiveList<TodoItem>(); // In some binding scenarios the sourceCollection passed might be null, set an empty list instead

			_collection.Changed.Subscribe(args => CollectionChanged?.Invoke(this, args)).DisposeWith(_disposables);
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public IEnumerator<TodoItem> GetEnumerator() => _collection.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();

		public void Dispose() => _disposables.Dispose();

		private IReactiveCollection<TodoItem> _collection;
		private CompositeDisposable _disposables = new CompositeDisposable();
	}
}