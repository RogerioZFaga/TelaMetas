using System;
using MetasApp.Models;
using MetasApp.ViewModels;
using Microsoft.Maui.Controls;

namespace MetasApp.Views;
public partial class MetaDetailPage : ContentPage
{
    Meta _meta;
    MetasViewModel _vm;

    public MetaDetailPage(Meta meta, MetasViewModel vm)
    {
        InitializeComponent();
        _meta = meta;
        _vm = vm;
        Bind();
    }

    void Bind()
    {
        NomeLabel.Text = _meta.Nome;
        DescricaoLabel.Text = _meta.Descricao;
        DataLimiteLabel.Text = _meta.DataLimite.ToString("dd/MM/yyyy");
        DataConclusaoLabel.Text = _meta.DataConclusao?.ToString("dd/MM/yyyy") ?? "Pendente";
    }

    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        bool ok = await DisplayAlert("Confirmar", "Deseja deletar esta meta?", "Sim", "Não");
        if (!ok) return;
        await _vm.DeleteAsync(_meta);
        await Navigation.PopAsync();
    }
}