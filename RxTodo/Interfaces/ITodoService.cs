using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RxTodo
{
	public interface ITodoService
	{
		Task SaveItemAsync(TodoItem item);
		Task<IEnumerable<TodoItem>> LoadItemsAsync();
	}
}
