using UnityEngine;

namespace TF.ColoredHeader
{
    public enum HeaderAlignment { Left, Center, Right }
        
    public static class HeaderAlignmentExtensionMethods
    {
        public static TextAnchor ToTextAnchor(this HeaderAlignment alignment)
        {
            var anchor = alignment switch
            {
                HeaderAlignment.Left => TextAnchor.MiddleLeft,
                HeaderAlignment.Center => TextAnchor.MiddleCenter,
                HeaderAlignment.Right => TextAnchor.MiddleRight,
                _ => default
            };
    
            return anchor;
        }
    }
}
