using Assets.Project.Code.Runtime.Progress;
using System.Threading.Tasks;

public interface ISaveLoadHandler
{
    ProgressData ProgressData { get; }
    Task SaveAsync();
    Task LoadAsync();
}
