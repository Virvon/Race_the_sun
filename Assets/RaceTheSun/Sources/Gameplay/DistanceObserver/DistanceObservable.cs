using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Gameplay.DistanceObserver
{
    public class DistanceObservable : ITickable
    {
        private Transform _spaceship;
        private List<ObserverInfo> _observers;

        public DistanceObservable()
        {
            _observers = new();
        }

        public void Init(Spaceship.Spaceship spaceship)
        {
            _spaceship = spaceship.transform;
        }

        public void Tick()
        {
            if (_spaceship == null)
                return;

            Vector3 SpaceshipPosition = _spaceship.position;
            List<ObserverInfo> removedObservers = new();

            foreach(ObserverInfo info in _observers)
            {
                if (SpaceshipPosition.z >= info.InvokePosition.z)
                {
                    info.Observer.Invoke();
                    removedObservers.Add(info);
                }
            }

            RemoveObservers(removedObservers);
        }

        public void RegisterObserver(IObserver observer, Vector3 invokePosition)
        {
            _observers.Add(new ObserverInfo(observer, invokePosition));
        }

        private void RemoveObservers(List<ObserverInfo> observers)
        {
            foreach(ObserverInfo removedInfos in observers)
                _observers.Remove(removedInfos);
        }
    }
}
