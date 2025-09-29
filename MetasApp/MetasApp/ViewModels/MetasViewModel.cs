using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using MetasApp.Models;
using Microsoft.Maui;

namespace MetasApp.ViewModels;
public class MetasViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Meta> Pendentes { get; } = new();
    public ObservableCollection<Meta> Concluidas { get; } = new();

    public ICommand ToggleStatusCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand AddMetaCommand { get; }

    public MetasViewModel()
    {
        ToggleStatusCommand = new Command<Meta>(async m => await ToggleStatusAsync(m));
        DeleteCommand = new Command<Meta>(async m => await DeleteAsync(m));
        AddMetaCommand = new Command(AddMeta);

        Seed();
    }

    void Seed()
    {
        Pendentes.Add(new Meta { Id = 1, Nome = "Estudar MAUI", Descricao = "Finalizar tela de metas", DataLimite = DateTime.Today.AddDays(5) });
        Pendentes.Add(new Meta { Id = 2, Nome = "Praticar Algoritmos", Descricao = "Resolver 5 questões", DataLimite = DateTime.Today.AddDays(2) });
        Concluidas.Add(new Meta { Id = 3, Nome = "Ler livro", Descricao = "Capítulos 1-5", DataLimite = DateTime.Today.AddDays(-3), IsCompleted = true, DataConclusao = DateTime.Today.AddDays(-1) });
    }

    public async Task ToggleStatusAsync(Meta meta)
    {
        if (meta == null) return;
        var becameCompleted = !meta.IsCompleted;
        meta.IsCompleted = becameCompleted;
        meta.DataConclusao = becameCompleted ? DateTime.Now : (DateTime?)null;

        await Task.Delay(50);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (becameCompleted)
            {
                if (Pendentes.Contains(meta)) Pendentes.Remove(meta);
                if (!Concluidas.Contains(meta)) Concluidas.Insert(0, meta);
            }
            else
            {
                if (Concluidas.Contains(meta)) Concluidas.Remove(meta);
                if (!Pendentes.Contains(meta)) Pendentes.Insert(0, meta);
            }
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(Percentual));
        });
    }

    public async Task DeleteAsync(Meta meta)
    {
        if (meta == null) return;
        await Task.Delay(1);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (meta.IsCompleted) Concluidas.Remove(meta);
            else Pendentes.Remove(meta);
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(Percentual));
        });
    }

    void AddMeta()
    {
        Application.Current.MainPage.Navigation.PushAsync(new Views.CriarMetaPage(this));
    }

    public void AddNovaMeta(Meta meta)
    {
        if (meta == null) return;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Pendentes.Insert(0, meta);
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(Percentual));
        });
    }

    public int Total => Pendentes.Count + Concluidas.Count;
    public double Percentual => Total == 0 ? 0 : Math.Round((double)Concluidas.Count / Total * 100.0, 1);

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}