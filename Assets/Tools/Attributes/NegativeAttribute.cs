using UnityEngine;
using UnityEditor;

public class NegativeAttribute : PropertyAttribute { }

[CustomPropertyDrawer(typeof(NegativeAttribute))]
public class NegativePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            property.intValue = EditorGUI.IntField(position, label, Mathf.Clamp(property.intValue, int.MinValue, 0));
        }
        else if (property.propertyType == SerializedPropertyType.Float)
        {
            property.floatValue = EditorGUI.FloatField(position, label, Mathf.Clamp(property.floatValue, float.MinValue, 0f));
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
