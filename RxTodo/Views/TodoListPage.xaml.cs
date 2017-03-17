using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ReactiveUI;
using ReactiveUI.XamForms;

using Xamarin.Forms;

namespace RxTodo
{
	public partial class TodoListPage : ReactiveContentPage<TodoListViewModel> 
	{
		private bool _haveloadedOnActivation;

		public TodoListPage()
		{
			InitializeComponent();

			var cacheService = new TodoService();

			ViewModel = new TodoListViewModel(cacheService);

			this.OneWayBind(this.ViewModel, vm => vm.TodoItemCollection, v => v.listView.ItemsSource, selector: x =>
			{
				return new ReactiveObservableList<TodoItem>(x);
			});

			this.WhenActivated(async (disposable) => await this.ViewModel.LoadAllItems());
		}

		public void OnItemAdded(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new TodoItemPage());
		}

		public void OnListItemSelected(object sender, ItemTappedEventArgs e)
		{
			var selectedItem = e.Item as TodoItem;
			this.Navigation.PushAsync(new TodoItemPage(selectedItem));
		}
	}
}
