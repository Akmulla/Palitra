//using UnityEngine;
//using System.Collections;
//using UnityEditor;

//[CustomEditor(typeof(LvlData))]
//public class LineEditor : Editor
//{

//    public override void OnInspectorGUI()
//    {
//        LvlData myTarget = (LvlData)target;
//        SerializedObject so = new SerializedObject(target);
//        SerializedProperty lines = so.FindProperty("lines");
//        EditorGUILayout.PropertyField(lines, true); // True means show children
//        so.ApplyModifiedProperties(); // Remember to apply modified properties

//        myTarget.accel = EditorGUILayout.FloatField( myTarget.accel);
//        //EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
//    }
//}
