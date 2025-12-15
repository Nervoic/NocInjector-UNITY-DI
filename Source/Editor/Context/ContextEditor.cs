// using UnityEditor;
// using UnityEngine;
//
// namespace NocInjector
// {
//     [CustomEditor(typeof(MonoContext), true)]
//     public class ContextEditor : Editor
//     {
//         private const string CustomizeButtonText = "Customize Context";
//         public override void OnInspectorGUI()
//         {
//             DrawEditButton();
//         }
//
//         private void DrawEditButton()
//         {
//             DrawDefaultInspector();
//
//             var currentContext = (MonoContext)target;
//             
//             GUILayout.Space(10);
//
//             GUILayout.BeginHorizontal();
//             {
//                 if (GUILayout.Button(CustomizeButtonText, GetEditButtonStyle(),GUILayout.Height(45)))
//                 {
//                     ContextCustomizerWindow.ShowWindow();
//                     ContextCustomizerWindow.Context = currentContext;
//                 }
//             }
//             GUILayout.EndHorizontal();
//         }
//
//         private GUIStyle GetEditButtonStyle()
//         {
//             var style = new GUIStyle(GUI.skin.button)
//             {
//                 alignment = TextAnchor.MiddleCenter,
//
//                 padding = new RectOffset(10, 10, 5, 5),
//                 margin = new RectOffset(2, 2, 2, 2),
//                 overflow = new RectOffset(1, 1, 1, 1),
//
//                 border = new RectOffset(6, 6, 6, 6),
//                 
//                 fontSize = 12,
//                 fontStyle = FontStyle.Bold,
//             };
//
//             return style;
//         }
//     }
// }
