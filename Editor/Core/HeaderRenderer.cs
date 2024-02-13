using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TF.ColoredHeader
{
    #if UNITY_EDITOR
    [InitializeOnLoad]
    public static class HeaderRenderer
    {
        private const int HierarchyItemRectRightMargin = 16;
        private const int SceneVisibilityAndPickabilityControlXMax = 32;
        
        static HeaderRenderer() => EditorApplication.hierarchyWindowItemOnGUI += OnGameObjectItemRender;

        private static void OnGameObjectItemRender(int instanceID, Rect selectionRect)
        {
            var gameObject = (GameObject)EditorUtility.InstanceIDToObject(instanceID);
            if (gameObject == null)
                return;

            if (gameObject.TryGetComponent<HeaderTitle>(out var header))
            {
                DrawHeader(header, selectionRect, gameObject.transform);
            }
        }

        private static void DrawHeader(HeaderTitle header, Rect rect, Transform goTransform)
        {
            var headerStyle = new GUIStyle(EditorStyles.label);
            
            rect.xMin = SceneVisibilityAndPickabilityControlXMax;
            rect.xMax += HierarchyItemRectRightMargin;

            if (goTransform.childCount > 0 || goTransform.parent != null)
            {
                EditorGUI.DrawRect(rect, Color.red);
                headerStyle.normal.textColor = Color.white;
                
                headerStyle.font = null;
                headerStyle.fontStyle = FontStyle.Bold;
                headerStyle.alignment = TextAnchor.MiddleCenter;

                EditorGUI.LabelField(rect,
                    goTransform.childCount > 0
                        ? "Headers shouldn't have child".ToUpper()
                        : "Headers shouldn't be parented".ToUpper(), headerStyle);

                return;
            } 
            
            // font
            headerStyle.font = header.font;
            headerStyle.fontStyle = header.fontStyle;
            headerStyle.alignment = header.alignment.ToTextAnchor();
            
            // font color
            headerStyle.normal.textColor = header.fontColor;
            
            // background color
            EditorGUI.DrawRect(rect, header.backgroundColor);
            
            EditorGUI.LabelField(rect, $" {goTransform.name} ", headerStyle);
        }
    }
    #endif
}
