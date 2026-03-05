namespace MauiAppMinhasCompras.views;

public partial class ListaProduto : ContentPage
{
	public ListaProduto()
	{
		InitializeComponent();
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
}