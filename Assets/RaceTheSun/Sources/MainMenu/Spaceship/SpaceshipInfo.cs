using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RaceTheSun.Sources.MainMenu.Spaceship
{
    public class SpaceshipInfo : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _isUnlocked;
        [SerializeField] private int _buyCost;
        [SerializeField] private Image _image;
        [SerializeField] private StatType _unlockedStatType;

        public string Name => _name;

        public bool IsUnlocked => _isUnlocked;
        public int BuyCost => _buyCost;
        public StatType UnlockedStatType => _unlockedStatType;

        private void Start()
        {
            if(_isUnlocked == false)
                _image.color = Color.gray;
        }

        public void Unlock()
        {
            _isUnlocked = true;
            _image.color = Color.white;
        }

        public void Select()
        {
            _image.color = Color.blue;
        }

        public void Deselect()
        {
            _image.color = Color.white;
        }
    }
}
