using UnityEditor;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect.Editor
{
    [CustomEditor(typeof(SpellSO))]
    public class SpellEditor : UnityEditor.Editor
    {
        private SpellSO _spellSO => target as SpellSO;

        [SerializeField] private EffectSO _effectSo;

        [MenuItem("Effect properties")]
        public void EditEffect()
        {
            if (_effectSo != null)
                EditorUtility.OpenPropertyEditor(_effectSo);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // EditorGUILayout.BeginHorizontal();
            // _effectSo = (EffectSO)EditorGUILayout.ObjectField(_effectSo, typeof(EffectSO));
            // EditorGUILayout.EndHorizontal();
            //
            // if (_effectSo != null && GUILayout.Button("Add effect"))
            // {
            //     _spellSO.AddEffect(_effectSo);
            // }

            if (_spellSO.EffectsSO.Count != 0 && GUILayout.Button("Initialize Spell"))
            {
                _spellSO.InitializeSpell();
            }
        }
    }
}