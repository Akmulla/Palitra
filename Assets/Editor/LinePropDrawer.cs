//using UnityEngine;
//using UnityEditor;

//[CustomPropertyDrawer(typeof(LineProp))]
//public class ScaledCurveDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
//    {
//        SerializedProperty type = prop.FindPropertyRelative("type");
//        SerializedProperty count = prop.FindPropertyRelative("count");
//        int edge=0;

//        // Draw scale
//        //EditorGUI.Slider(
//        //    new Rect(pos.x, pos.y, pos.width - curveWidth, pos.height),
//        //    scale, min, max, label);

//        pos = EditorGUI.PrefixLabel(pos, GUIUtility.GetControlID(FocusType.Passive), GUIContent.none);

//        EditorGUI.PropertyField(new Rect(pos.x, pos.y, pos.width - 100, pos.height), type, GUIContent.none);
//        switch (type.enumValueIndex)
//        {
//            case (int)PoolType.Chngbl:
//                EditorGUI.PropertyField(new Rect(pos.x+50, pos.y+pos.height, pos.width - 50, pos.height), type, GUIContent.none);
//                break;
//        }
        

//        // Draw curve
//        int indent = EditorGUI.indentLevel;
//        EditorGUI.indentLevel = 0;
//        EditorGUI.PropertyField(
//            new Rect(pos.width - 20, pos.y, 50, pos.height),
//            count, GUIContent.none);
//        EditorGUI.indentLevel = indent;
//    }
//}