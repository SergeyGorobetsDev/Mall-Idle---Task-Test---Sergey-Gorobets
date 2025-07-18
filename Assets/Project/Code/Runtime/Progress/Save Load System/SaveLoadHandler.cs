using Assets.Project.Code.Runtime.Architecture.FileSystem;
using Assets.Project.Code.Runtime.Progress;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveLoadHandler : MonoBehaviour, ISaveLoadHandler
{
    public const string SavesFileName = "ProgressData";
    public const string SavesFileFormat = ".json";

    private readonly IFileProvider fileProvider = new FileProvider();

    [SerializeField]
    private ProgressData progressData;

    public ProgressData ProgressData
    {
        get => progressData;
        private set => progressData = value;
    }

    public async Task LoadAsync()
    {
        string toLoad = await fileProvider.ReadFileAsync(GetPlatformPath());
        if (string.IsNullOrEmpty(toLoad))
        {
            progressData = new();
            progressData.Version = Application.version;
            progressData.NewGame = true;
            progressData.Money = 100;
            fileProvider.Cancel();
#if UNITY_EDITOR
            Debug.Log("Create new user data");
#endif
            return;
        }

        progressData = JsonConvert.DeserializeObject<ProgressData>(toLoad);

#if UNITY_EDITOR
        Debug.Log($"User Data Loaded {progressData}");
#endif
    }

    public async Task SaveAsync()
    {
        progressData.Version = Application.version;
        string toSave = JsonConvert.SerializeObject(progressData);
        await fileProvider.WriteFileAsync(GetPlatformPath(), toSave);
#if UNITY_EDITOR
        Debug.Log($"User Data Saved {progressData}");
#endif
    }

    private string GetPlatformPath()
    {
#if UNITY_EDITOR
        return Path.Combine(Application.dataPath, SavesFileName + SavesFileFormat);
#elif UNITY_ANDROID
            return Path.Combine(Application.persistentDataPath, SavesFileName + SavesFileFormat);
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE
            return Path.Combine(Application.persistentDataPath, SavesFileName + SavesFileFormat);
#endif
    }
}
