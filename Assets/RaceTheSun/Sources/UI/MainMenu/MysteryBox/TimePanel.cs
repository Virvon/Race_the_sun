using Assets.RaceTheSun.Sources.Services.PersistentProgress;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI.MainMenu.MysteryBox
{
    public class TimePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;

        private DateTime _endDate;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgressService) =>
            _endDate = persistentProgressService.Progress.MysteryBoxes.GetEndDate();

        private void Update()
        {
            TimeSpan timeSpan = _endDate.Subtract(DateTime.Now);

            _value.text = timeSpan <= TimeSpan.Zero ? ToReadableString(TimeSpan.Zero) : ToReadableString(timeSpan);
        }

        private string ToReadableString(TimeSpan span) =>
            span.Hours + ":" + span.Minutes + ":" + span.Seconds;
    }
}
