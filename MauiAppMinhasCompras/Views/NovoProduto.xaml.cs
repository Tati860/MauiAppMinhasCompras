using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Views;
using System.Threading.Tasks;


namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}
    
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			if (string.IsNullOrWhiteSpace(txt_descricao.Text))
			{
				await DisplayAlert("Ops", "Informe a descrição do produto", "OK");
				return;
			}
			//validação da categoria para o desafio
			if (pck_categoria.SelectedIndex == null)
			{
				await DisplayAlert("Ops", "Selecione a categoria ", "OK");
				return;
			}
			Produto p = new Produto

			{
				Descricao = txt_descricao.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text),
				Categoria = pck_categoria.SelectedItem.ToString()
			};
			await App.Db.Insert(p);
			await DisplayAlert("Sucesso", "Registro Inserido", "OK");
			await Navigation.PopAsync();
			

		}catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}

    }
}