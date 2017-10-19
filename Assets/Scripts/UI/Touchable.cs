using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(Touchable))]
public class TouchableEditor : Editor
{ public override void OnInspectorGUI() { } }
#endif
public class Touchable : Text
{ protected override void Awake() { base.Awake(); } }