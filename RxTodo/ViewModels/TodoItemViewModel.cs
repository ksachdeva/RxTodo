using System;

using ReactiveUI;

namespace RxTodo
{
	public class TodoItemViewModel : ReactiveObject
	{
		private readonly TodoItem _item;
		private readonly ITodoService _cacheService;

		private String _name;
		private String _notes;
		private bool _isDone;

		public ReactiveCommand SaveCommand { get; }

		public TodoItemViewModel(ITodoService cacheService, TodoItem item = null)
		{
			if (item == null)
				item = new TodoItem();

			_item = item;
			_cacheService = cacheService;

			Name = _item.Name;
			Notes = _item.Notes;
			IsDone = _item.IsDone;

			var canSave = this.WhenAnyValue(
				x => x.Name,
				x => x.Notes,
				(name, notes) =>
				{
					return !String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(notes);
				});

			SaveCommand = ReactiveCommand.CreateFromTask(async () =>
			{
				// make sure to update the _item
				_item.IsDone = IsDone;
				_item.Notes = Notes;
				_item.Name = Name;

				if (_item.ID == null)
					_item.ID = Guid.NewGuid().ToString();

				await _cacheService.SaveItemAsync(_item);
			}, canSave);
		}

		public String Name
		{
			get { return this._name; }
			set { this.RaiseAndSetIfChanged(ref _name, value); }
		}

		public String Notes
		{
			get { return this._notes; }
			set { this.RaiseAndSetIfChanged(ref _notes, value); }
		}

		public bool IsDone
		{
			get { return this._isDone; }
			set { this.RaiseAndSetIfChanged(ref _isDone, value); }
		}
	}
}
