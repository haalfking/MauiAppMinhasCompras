using MauiAppMinhasCompras.models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.views;


public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.Getall();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("ops", ex.Message, "ok");
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new views.NovoProduto());

		}catch (Exception ex)
		{
			DisplayAlert("ops", ex.Message, "ok");
		}

    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            lst_produtos.IsRefreshing = true;

            lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("ops", ex.Message, "ok");
        }
        finally
        {
                       lst_produtos.IsRefreshing = false;
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);

		string msg = $"O Total é {soma:c}";

		DisplayAlert("Total dos Produtos", msg, "ok");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem menuItem = sender as MenuItem;

            Produto produtoSelecionado = menuItem.BindingContext as Produto;

            bool confirmar = await DisplayAlert(
                "Confirmar",
                "Deseja remover o produto?",
                "Sim",
                "Não"
            );

            if (confirmar)
            {
                await App.Db.Delete(produtoSelecionado.Id);

                lista.Remove(produtoSelecionado);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;

            Navigation.PushAsync(new views.EditarProduto
                {
                BindingContext = p
            });

        }
        catch (Exception ex)
        {
             DisplayAlert("Erro", ex.Message, "OK");
        }

    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.Getall();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("ops", ex.Message, "ok");
        }
        finally
        {
                       lst_produtos.IsRefreshing = false;
        }

    }
    private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RelatorioPage());
    }
}