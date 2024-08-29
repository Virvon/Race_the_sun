using Assets.RaceTheSun.Sources.Data;

namespace Assets.RaceTheSun.Sources.Services.PersistentProgress
{
    public interface IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}