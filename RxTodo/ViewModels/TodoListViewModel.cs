using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ReactiveUI;

namespace RxTodo
{
	public class TodoListViewModel : ReactiveObject
	{
		private readonly ITodoService _todoService;

		public ReactiveList<TodoItem> TodoItemCollection { get; } =
			new ReactiveList<TodoItem>();

		public ReactiveCommand AddNewItemCommand { get; }
		public ReactiveCommand LoadAllItemsCommand { get; }

		public TodoListViewModel(ITodoService todoService)
		{
			this._todoService = todoService;

			// LoadAllItemsCommand = ReactiveCommand.CreateFromTask(async () => await LoadAllItems());
		}

		public async Task LoadAllItems()
		{
			TodoItemCollection.Clear();

			IEnumerable<TodoItem> items = await _todoService.LoadItemsAsync();

			TodoItemCollection.AddRange(items);
		}
	}
}
