using Assets.RaceTheSun.Sources.Data;
using UnityEngine;

internal class SaveLoadService : ISaveLoadService
{
    private const string Key = "Progress";

    private readonly IPersistentProgressService _progressService;

    public SaveLoadService(IPersistentProgressService progressService) =>
        _progressService = progressService;

    public PlayerProgress LoadProgress() =>
        PlayerPrefs.GetString(Key)?.ToDeserialized<PlayerProgress>();

    public void SaveProgress() =>
        PlayerPrefs.SetString(Key, _progressService.Progress.ToJson());
}