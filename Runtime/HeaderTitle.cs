using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TF.ColoredHeader
{
    [System.Serializable]
    public class HeaderTitle : MonoBehaviour
    {
        [TabGroup("Color")] public Color fontColor = Color.white;
        [TabGroup("Color")] public Color backgroundColor = Color.gray;
        
        [TabGroup("Font")] public Font font;
        [TabGroup("Font")] public FontStyle fontStyle = FontStyle.Bold;
        [TabGroup("Font")] public HeaderAlignment alignment = HeaderAlignment.Center;
        
        private const string EditorOnlyTag = "EditorOnly";

        #if UNITY_EDITOR
        [MenuItem("GameObject/Header Title", false, -100)]
        internal static void CreateHeader(MenuCommand menu)
        {
            var go = new GameObject();
            go.AddComponent<HeaderTitle>();
            GameObjectUtility.SetParentAndAlign(go, menu.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Created MyHierarchy GO: " + go.name);
            Selection.activeObject = go;
            EditorApplication.RepaintHierarchyWindow();
        }
        #endif

        private void OnDrawGizmosSelected() => gameObject.tag = EditorOnlyTag;

        #if UNITY_EDITOR
        private void OnValidate() => EditorApplication.RepaintHierarchyWindow();
        #endif
    }
}

