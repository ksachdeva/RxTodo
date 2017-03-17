using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Akavache;

namespace RxTodo
{
	public class TodoService : ITodoService
	{
		private const string CACHE_NAME = "TodoCache";

		public TodoService()
		{
			BlobCache.ApplicationName = CACHE_NAME;
		}

		public async Task SaveItemAsync(TodoItem item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			await BlobCache.LocalMachine.InsertObject<TodoItem>(item.ID, item);
		}

		public async Task<IEnumerable<TodoItem>> LoadItemsAsync()
		{
			return await BlobCache.LocalMachine.GetAllObjects<TodoItem>();
		}
	}
}
