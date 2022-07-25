using UnityEngine;
using UnityEditor;

public class PositiveAttribute : PropertyAttribute { }

[CustomPropertyDrawer(typeof(PositiveAttribute))]
public class PositivePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            property.intValue = EditorGUI.IntField(position, label, Mathf.Clamp(property.intValue, 0, int.MaxValue));
        }
        else if (property.propertyType == SerializedPropertyType.Float)
        {
            property.floatValue = EditorGUI.FloatField(position, label, Mathf.Clamp(property.floatValue, 0f, float.MaxValue));
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
