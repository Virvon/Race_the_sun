using System;

namespace Assets.RaceTheSun.Sources.Data
{
    [Serializable]
    public class MysteryBoxesData
    {
        public int Count;
        public string EndDate;

        public MysteryBoxesData() =>
            EndDate = DateTime.MinValue.ToString();

        public event Action<int> CountChanged;

        public void Take()
        {
            Count--;
            CountChanged?.Invoke(Count);
        }

        public void Give()
        {
            if (Count == 0)
            {
                DateTime endDate = DateTime.Now;
                endDate = endDate.AddDays(1);
                EndDate = endDate.ToString();
            }

            Count++;
            CountChanged?.Invoke(Count);
        }

        public DateTime GetEndDate() =>
            DateTime.Parse(EndDate);
    }
}