﻿using UnityEngine;

namespace ImmersiveVRTools.Runtime.Common.PropertyDrawer
{
    public class MinMaxSliderAttribute : PropertyAttribute
    {
        public float Min { get; }
        public float Max { get; }

        public MinMaxSliderAttribute(float min, float max)
        {
            this.Min = min;
            this.Max = max;
        }
    }
}
