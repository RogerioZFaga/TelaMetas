using MetasApp.Models;
using MetasApp.ViewModels;
using Microsoft.Maui.Controls;

namespace MetasApp.Views;
public partial class MetasPage : ContentPage
{
    MetasViewModel Vm => BindingContext as MetasViewModel;

    public MetasPage()
    {
        InitializeComponent();
    }

    private async void OnDetalhesClicked(object sender, EventArgs e)
    {
        if (sender is Button b && b.CommandParameter is Meta m)
        {
            await Navigation.PushAsync(new MetaDetailPage(m, Vm));
        }
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox cb && cb.BindingContext is Meta m)
        {
            Vm?.ToggleStatusCommand.Execute(m);
        }
    }
}