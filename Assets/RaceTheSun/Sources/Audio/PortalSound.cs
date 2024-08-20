﻿using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.Audio
{
    public class PortalSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void Play()
        {
            _audioSource.Play();
        }

        public class Factory : PlaceholderFactory<string, UniTask<PortalSound>>
        {

        }
    }
}
