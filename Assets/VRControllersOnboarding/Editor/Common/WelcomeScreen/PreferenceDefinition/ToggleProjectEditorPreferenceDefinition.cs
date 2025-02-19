﻿using UnityEditor;
using UnityEngine;

namespace ImmersiveVRTools.Editor.Common.WelcomeScreen.PreferenceDefinition
{
    public class ToggleProjectEditorPreferenceDefinition : ProjectEditorPreferenceDefinitionBase
    {
        public ToggleProjectEditorPreferenceDefinition(string label, string preferenceKey, object defaultValue, HandleOnEditorPersistedValueChange handleOnEditorPersistedValueChange = null,
            EditorPreferenceInitialize onInitialize = null)
            : base(label, preferenceKey, defaultValue, handleOnEditorPersistedValueChange, onInitialize)
        {
        }

        public override object GetEditorPersistedValueInternal()
        {
            return EditorPrefs.GetBool(PreferenceKey, (bool)DefaultValue);
        }

        protected override void SetEditorPersistedValueInternal(object newValue)
        {
            EditorPrefs.SetBool(PreferenceKey, (bool)newValue);
        }

        public override object RenderEditorAndCaptureInput(object currentValue, GUIStyle style, params GUILayoutOption[] layoutOptions)
        {
            var val = currentValue is null ? false : (bool)currentValue;
            return EditorGUILayout.Toggle(GuiContent, val, style ?? EditorStyles.toggle, layoutOptions);
        }
    }
}