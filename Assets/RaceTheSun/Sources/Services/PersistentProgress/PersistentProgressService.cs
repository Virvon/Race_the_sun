using Assets.RaceTheSun.Sources.Data;

namespace Assets.RaceTheSun.Sources.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}