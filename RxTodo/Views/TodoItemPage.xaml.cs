using System;
using System.Collections.Generic;

using ReactiveUI;

using Xamarin.Forms;

namespace RxTodo
{
	public partial class TodoItemPage : ContentPage, IViewFor<TodoItemViewModel>
	{
		public TodoItemPage() : this(null)
		{
			
		}

		public TodoItemPage(TodoItem item)
		{
			InitializeComponent();

			var cacheService = new TodoService();

			ViewModel = new TodoItemViewModel(cacheService, item);

			this.Bind(ViewModel, vm => vm.Name, v => v.name.Text);
			this.Bind(ViewModel, vm => vm.Notes, v => v.notes.Text);

			this.BindCommand(ViewModel, vm => vm.SaveCommand, v => v.save);
		}

		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create(
				nameof(ViewModel),
				typeof(TodoItemViewModel),
				typeof(TodoItemPage), null,
				BindingMode.OneWay);

		public TodoItemViewModel ViewModel
		{
			get { return (TodoItemViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (TodoItemViewModel)value; }
		}
	}
}
