using System.Collections.ObjectModel;
using System.Linq;
using MetasApp.Models;

namespace MetasApp.Services;
public class MetaService
{
    private int seq = 1;
    public ObservableCollection<Meta> Metas { get; } = new();


    public void Add(Meta m)
    {
        m.Id = seq++;
        Metas.Insert(0, m);
    }

    public void Remove(Meta m)
    {
        Metas.Remove(m);
    }

    public void Update(Meta m)
    {
        var idx = Metas.ToList().FindIndex(x => x.Id == m.Id);
        if (idx >= 0) Metas[idx] = m;
    }

    public int Total() => Metas.Count;
    public int Concluidas() => Metas.Count(m => m.IsCompleted);
    public int Pendentes() => Metas.Count(m => !m.IsCompleted);
}
