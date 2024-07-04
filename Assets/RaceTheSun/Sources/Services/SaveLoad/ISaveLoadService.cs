using Assets.RaceTheSun.Sources.Data;

public interface ISaveLoadService
{
    void SaveProgress();

    PlayerProgress LoadProgress();
}
