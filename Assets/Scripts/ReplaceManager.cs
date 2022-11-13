using System;
using System.Linq;
using RTLTMPro;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ReplaceManager : MonoBehaviour
    {
        [MenuItem("Bazaar/Replace")]
        static void Replace()
        {
            Debug.Log("Start Replacing");


            var gameObjects = AssetDatabase
                .GetAllAssetPaths()
                .ToList()
                .Where(s => s.Contains("Assets/Scenes"))
                .ToList()
                .Select(p => AssetDatabase.LoadAssetAtPath<GameObject>(p))
                .Where(go => go != null)
                .ToList();

            gameObjects
                .Select(go => go.GetComponentInChildren<Text>())
                .Where(c => c != null)
                .ToList()
                .ForEach(t =>
                {
                    Debug.Log(t.name);
                    var gameObject = t.gameObject;
                    var alignment = t.alignment;
                    var fontSize = t.fontSize;
                    var color = t.color;
                    var horizontalOverflow = t.horizontalOverflow;
                    var verticalOverflow = t.verticalOverflow;
                    var text = t.text;
                    var fontStyle = t.fontStyle;
                    DestroyImmediate(t, true);
                    var tmp = gameObject.AddComponent<RTLTextMeshPro>();
                    tmp.alignment = getAlignment(alignment);
                    tmp.text = text;
                    tmp.fontSize = fontSize;
                    tmp.color = color;
                    tmp.overflowMode = getOverflow(horizontalOverflow, verticalOverflow);
                    tmp.fontStyle = getFontStyle(fontStyle);
                });
        }

        private static FontStyles getFontStyle(FontStyle fontStyle)
        {
            switch (fontStyle)
            {
                case FontStyle.Bold:
                    return FontStyles.Bold;
                case FontStyle.Italic:
                    return FontStyles.Italic;
                case FontStyle.Normal:
                    return FontStyles.Normal;
                case FontStyle.BoldAndItalic:
                    return FontStyles.Bold | FontStyles.Italic;
            }

            return default;
        }

        private static TextOverflowModes getOverflow(HorizontalWrapMode horizontalOverflow, VerticalWrapMode verticalOverflow)
        {
            if (horizontalOverflow == HorizontalWrapMode.Wrap || verticalOverflow == VerticalWrapMode.Truncate)
            {
                return TextOverflowModes.Truncate;
            }

            return TextOverflowModes.Overflow;
        }

        private static TextAlignmentOptions getAlignment(TextAnchor alignment)
        {
            switch (alignment)
            {
                case TextAnchor.LowerCenter:
                    return TextAlignmentOptions.Bottom;
                case TextAnchor.LowerLeft:
                    return TextAlignmentOptions.BottomLeft;
                case TextAnchor.LowerRight:
                    return TextAlignmentOptions.BottomRight;
                case TextAnchor.MiddleCenter:
                    return TextAlignmentOptions.Center;
                case TextAnchor.MiddleLeft:
                    return TextAlignmentOptions.BaselineLeft;
                case TextAnchor.MiddleRight:
                    return TextAlignmentOptions.BaselineRight;
                case TextAnchor.UpperCenter:
                    return TextAlignmentOptions.Top;
                case TextAnchor.UpperLeft:
                    return TextAlignmentOptions.TopLeft;
                case TextAnchor.UpperRight:
                    return TextAlignmentOptions.TopRight;
            }

            return default;
        }
    }
}