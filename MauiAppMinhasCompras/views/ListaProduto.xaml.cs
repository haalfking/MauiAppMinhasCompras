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
		List<Produto> tmp = await App.Db.Getall();

		tmp.ForEach( i => lista.Add(i));
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
		string q = e.NewTextValue;

		lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);

		string msg = $"O Total é {soma:c}";

		DisplayAlert("Total dos Produtos", msg, "ok");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}