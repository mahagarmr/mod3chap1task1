using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod3chap1task1
{
    public class ProgressBarData
    {
        public int CurrentValue { get; set; }
        public int MaxValue { get; set; }
        public Color Color { get; set; }
        public bool IsActive { get; set; }

        public ProgressBarData(int maxValue, Color color)
        {
            MaxValue = maxValue;
            Color = color;
            IsActive = true;
        }
    }
}
