using Assets.RaceTheSun.Sources.Data;

public interface IProgressSaver : ISaveProgressReader
{
    void UpdateProgress(PlayerProgress playerProgress);
}
