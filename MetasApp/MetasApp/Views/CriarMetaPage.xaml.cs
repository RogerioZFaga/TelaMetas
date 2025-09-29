using System;
using MetasApp.Models;
using MetasApp.ViewModels;
using Microsoft.Maui.Controls;

namespace MetasApp.Views;
public partial class CriarMetaPage : ContentPage
{
    MetasViewModel _vm;
    public CriarMetaPage(MetasViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        DataPicker.Date = DateTime.Today;
    }

    private async void OnCriarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NomeEntry.Text))
        {
            await DisplayAlert("Erro", "Título obrigatório", "OK");
            return;
        }

        var m = new Meta
        {
            Id = new Random().Next(1000, int.MaxValue),
            Nome = NomeEntry.Text.Trim(),
            Descricao = DescricaoEditor.Text ?? string.Empty,
            DataLimite = DataPicker.Date,
            IsCompleted = false
        };

        _vm.AddNovaMeta(m);
        await Navigation.PopAsync();
    }
}