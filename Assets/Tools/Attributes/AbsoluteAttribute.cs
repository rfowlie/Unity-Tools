using UnityEditor;
using UnityEngine;



//TODO: LOOK INTO THIS

//this should exist in a regular script file
public class AbsoluteValueAttribute : PropertyAttribute
{
    public AbsoluteValueAttribute(bool flip)
    {
        this.flip = flip;
    }

    public readonly bool flip;
}

//this should exist in a script written stored in an Editor folder as and Editor script
[CustomPropertyDrawer(typeof(AbsoluteValueAttribute))]
public class AbsoluteValuePropertyDrawer : PropertyDrawer
{
    /*Ensures int or float value can only be positive*/

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //convert attribute to this type in order to get access values
        AbsoluteValueAttribute a = attribute as AbsoluteValueAttribute;

        if (property.propertyType == SerializedPropertyType.Integer)
        {
            if(a.flip)
            {
                property.intValue = EditorGUI.IntField(position, label, Mathf.Clamp(property.intValue, int.MinValue, 0));
            }
            else
            {
                property.intValue = EditorGUI.IntField(position, label, Mathf.Clamp(property.intValue, 0, int.MaxValue));
            }
        }
        else if (property.propertyType == SerializedPropertyType.Float)
        {
            if(a.flip)
            {
                property.floatValue = EditorGUI.FloatField(position, label, Mathf.Clamp(property.floatValue, float.MinValue, 0));
            }
            else
            {
                property.floatValue = EditorGUI.FloatField(position, label, Mathf.Clamp(property.floatValue, 0f, float.MaxValue));
            }
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}