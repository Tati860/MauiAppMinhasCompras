namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //mainpage = new MainPage();
            MainPage = new NavigationPage (new Views.ListaProduto());
        }

        
       
            
        
    }
}