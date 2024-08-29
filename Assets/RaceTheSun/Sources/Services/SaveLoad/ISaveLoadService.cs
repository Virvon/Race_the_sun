using Assets.RaceTheSun.Sources.Data;

namespace Assets.RaceTheSun.Sources.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveProgress();

        PlayerProgress LoadProgress();
    }
}