using MauiAppMinhasCompras.models;

namespace MauiAppMinhasCompras.views;

public partial class RelatorioPage : ContentPage
{
    public RelatorioPage()
    {
        InitializeComponent();
    }

    private async void OnFiltrarClicked(object sender, EventArgs e)
    {
        try
        {
            DateTime inicio = dtInicio.Date;
            DateTime fim = dtFim.Date.AddDays(1).AddSeconds(-1);

            var lista = await App.Db.GetByPeriodo(inicio, fim);
                
            listaProdutos.ItemsSource = lista;

            // 🔥 cálculo do total (diferencial)
            double total = lista.Sum(p => p.Total);

            await DisplayAlert("Total do Período",
                $"Total gasto: R$ {total}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}