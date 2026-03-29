using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            var p = (Produto)BindingContext;
            p.Descricao = txt_descricao.Text;
            p.Quantidade = Convert.ToDouble(txt_quantidade.Text);
            p.Preco = Convert.ToDouble(txt_preco.Text);
            if (pck_categoria.SelectedItem != null)
            {
                p.Categoria = pck_categoria.SelectedItem.ToString();
            }
                
            await App.Db.Update(p);
            await DisplayAlert("Sucesso", "Registro Atualizado", "OK");
            await Navigation.PopAsync();


        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");

        }
    }
}