using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> Lista = new ObservableCollection<Produto>();

    public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = Lista;
	}
	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await AtualizarDados();
	}
	private async Task AtualizarDados()
	{
		try
		{
			//busca todos os produtos e filtra por nome e categoria
			string busca = txt_search.Text ?? "";
			List<Produto> ResultadoBusca = await App.Db.Search(busca);
			List<Produto> tmp = ResultadoBusca;

			//Aplica o filtro de categoria, que é o pedido do desafio
			if (pck_filtro.SelectedItem != null)
			{
				string selecionada = pck_filtro.SelectedItem.ToString();
				if (selecionada != "Todos"  && selecionada != "Todas")
				{
					tmp = tmp.Where(i => (i.Categoria ?? string.Empty).ToLower() == selecionada.ToLower()).ToList();
				}
			}
			//Atualiza o visual da lista
			Lista.Clear();
			foreach (var item in tmp)
			{
				Lista.Add(item);
			}

			//relatório de gastos por categoria
			double soma = tmp.Sum(i => i.Quantidade * i.Preco);
			lbl_total_categoria.Text = soma.ToString("C2");
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
		finally
		{
			lst_produtos.IsRefreshing = false;
		}
	}
	private async void txt_search_TextChanged(object sender, TextChangedEventArgs e) =>
		await AtualizarDados();

	private async void pck_filtro_SelectedIndexChanged(object sender, EventArgs e) =>
	
		await AtualizarDados();
private async void ToolbarItem_Clicked(object sender, EventArgs e) //novo produto
{
	await Navigation.PushAsync(new Views.NovoProduto());
}
	private async void MenuItem_Clicked(object sender, EventArgs e) //deletar produto
	{
		try
		{
			var p = (Produto)((MenuItem)sender).BindingContext;
			if (await DisplayAlert("Confirmação", $"Deseja remover o produto {p.Descricao}?", "Sim", "Não"))
			{
				await App.Db.Delete(p.Id);
				await AtualizarDados();
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
	}

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)//editar produto
    {
        if (e.SelectedItem == null) return;

        Navigation.PushAsync(new Views.EditarProduto
        {
            BindingContext = (Produto)e.SelectedItem
        });
        ((ListView)sender).SelectedItem = null;
	}

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
	
	await AtualizarDados();
	}

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // Implemente aqui a lógica desejada para o botão "Somar"
    }
}


