using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace RxTodo
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			Resources = new ResourceDictionary();
			Resources.Add("primaryGreen", Color.FromHex("91CA47"));
			Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

			var nav = new NavigationPage(new TodoListPage());
			nav.BarBackgroundColor = (Color)Resources["primaryGreen"];
			nav.BarTextColor = Color.White;

			MainPage = nav;
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
