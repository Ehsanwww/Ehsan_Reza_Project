﻿using UnityEditor;
using UnityEngine;
using ImmersiveVRTools.Runtime.Common.PropertyDrawer;

namespace ImmersiveVRTools.Editor.Common.PropertyDrawer
{
    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderDrawer : UnityEditor.PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var minMaxAttribute = (MinMaxSliderAttribute)attribute;
            var propertyType = property.propertyType;

            label.tooltip = minMaxAttribute.Min.ToString("F2") + " to " + minMaxAttribute.Max.ToString("F2");

            //PrefixLabel returns the rect of the right part of the control. It leaves out the label section. We don't have to worry about it. Nice!
            var controlRect = EditorGUI.PrefixLabel(position, label);

            var splittedRect = SplitRect(controlRect, 3);

            if (propertyType == SerializedPropertyType.Vector2)
            {
                EditorGUI.BeginChangeCheck();

                var vector = property.vector2Value;
                var minVal = vector.x;
                var maxVal = vector.y;

                //F2 limits the float to two decimal places (0.00).
                minVal = EditorGUI.FloatField(splittedRect[0], float.Parse(minVal.ToString("F2")));
                maxVal = EditorGUI.FloatField(splittedRect[2], float.Parse(maxVal.ToString("F2")));

                EditorGUI.MinMaxSlider(splittedRect[1], ref minVal, ref maxVal,
                minMaxAttribute.Min, minMaxAttribute.Max);

                if (minVal < minMaxAttribute.Min)
                {
                    minVal = minMaxAttribute.Min;
                }

                if (maxVal > minMaxAttribute.Max)
                {
                    maxVal = minMaxAttribute.Max;
                }

                vector = new Vector2(minVal > maxVal ? maxVal : minVal, maxVal);

                if (EditorGUI.EndChangeCheck())
                {
                    property.vector2Value = vector;
                }

            }
            else if (propertyType == SerializedPropertyType.Vector2Int)
            {

                EditorGUI.BeginChangeCheck();

                var vector = property.vector2IntValue;
                float minVal = vector.x;
                float maxVal = vector.y;

                minVal = EditorGUI.FloatField(splittedRect[0], minVal);
                maxVal = EditorGUI.FloatField(splittedRect[2], maxVal);

                EditorGUI.MinMaxSlider(splittedRect[1], ref minVal, ref maxVal,
                minMaxAttribute.Min, minMaxAttribute.Max);

                if (minVal < minMaxAttribute.Min)
                {
                    maxVal = minMaxAttribute.Min;
                }

                if (minVal > minMaxAttribute.Max)
                {
                    maxVal = minMaxAttribute.Max;
                }

                vector = new Vector2Int(Mathf.FloorToInt(minVal > maxVal ? maxVal : minVal), Mathf.FloorToInt(maxVal));

                if (EditorGUI.EndChangeCheck())
                {
                    property.vector2IntValue = vector;
                }
            }
        }

        private static Rect[] SplitRect(Rect rectToSplit, int n)
        {
            var rects = new Rect[n];
            for (int i = 0; i < n; i++)
            {
                rects[i] = new Rect(rectToSplit.position.x + (i * rectToSplit.width / n), rectToSplit.position.y, rectToSplit.width / n, rectToSplit.height);
            }

            var padding = (int)rects[0].width - 40;
            var space = 5;

            rects[0].width -= padding + space;
            rects[2].width -= padding + space;

            rects[1].x -= padding;
            rects[1].width += padding * 2;

            rects[2].x += padding + space;

            return rects;

        }
    }
}
