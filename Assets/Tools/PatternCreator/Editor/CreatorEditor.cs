﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace PatternCreator
{
    [CustomEditor(typeof(Creator))]
    public class CreatorEditor : Editor
    {
        Creator o;

        [SerializeField] public static string assetName = string.Empty;
        [SerializeField] public static string assetPath = string.Empty;
        [SerializeField] public static Object folder = null;

        private void OnEnable()
        {
            o = (Creator)target;
        }

        public override void OnInspectorGUI()
        {

            EditorGUILayout.BeginVertical();
            if(GUILayout.Button("Reset"))
            {
                o.ResetValues();
            }
            EditorGUILayout.Space(10f);

            base.OnInspectorGUI();
            //o.spawnShape = (SpawnShape)EditorGUILayout.EnumPopup(o.spawnShape);
            //o.gizmoColour = EditorGUILayout.ColorField(o.gizmoColour);
            //o.amountOfPoints = EditorGUILayout.IntField("Amount of Points", o.amountOfPoints);
            //o.radius = EditorGUILayout.FloatField("Radius", o.radius);
            //o.angleOffset = EditorGUILayout.FloatField("Angle Offset", o.angleOffset);

            //GUILayout.Label("Editor", EditorStyles.boldLabel);
            //add to temp list
            if (GUILayout.Button("Set"))
            {
                o.isOn = EditorGUILayout.Toggle("Display Stored Patterns", o.isOn);

                //don't creat if no name...
                if (o.patternName == string.Empty)
                {
                    Debug.Log("<color=red>Pattern name not provided</color>");
                }
                else
                {
                    o.patternNames.Add(o.patternName);
                    o.colors.Add(o.gizmoColour);
                    o.patterns.Add(o.CreatePoints());
                    o.toggles.Add(true);
                    //gets current setup for creator, saves the info for if editing later
                    o.info.Add(o.CreateInfo());

                    //clear name after sending to drawer
                    o.patternName = string.Empty;
                }
            }
            
           
            EditorGUILayout.Space(30f);
            EditorGUILayout.LabelField("Stored Patterns", EditorStyles.boldLabel);
            o.isOn = EditorGUILayout.Toggle("Show", o.isOn);
            EditorGUILayout.Space(10f);
            GUILayout.Label("Patterns", EditorStyles.boldLabel);

            //guistyle???
            GUIStyle s = new GUIStyle();
            s.padding = new RectOffset(0, 0, 4, 4);
            s.stretchWidth = true;
            //display values in list
            for (int i = 0; i < o.patternNames.Count; i++)
            {
                //allow name to be adjustable after the fact...                
                EditorGUILayout.BeginHorizontal(s);
                o.toggles[i] = EditorGUILayout.Toggle(o.toggles[i]);
                o.patternNames[i] = EditorGUILayout.TextField(o.patternNames[i], GUILayout.MaxWidth(140));
                o.colors[i] = EditorGUILayout.ColorField(o.colors[i], GUILayout.MaxWidth(80));
                if (GUILayout.Button("Edit", GUILayout.MaxWidth(100)))
                {
                    //return back to creator for editing...
                    //save here but disable??? or should just remove?
                    //for now just remove and set creator to the info settings...
                    o.SetInfo(o.info[i]);
                    //colour is seperate...
                    o.gizmoColour = o.colors[i];

                    o.patternNames.RemoveAt(i);
                    o.patterns.RemoveAt(i);
                    o.colors.RemoveAt(i);
                    o.toggles.RemoveAt(i);
                    o.info.RemoveAt(i);
                }
                if (GUILayout.Button("Remove", GUILayout.MaxWidth(100)))
                {
                    if (o.patterns.Count > 0)
                    {
                        o.patternNames.RemoveAt(i);
                        o.patterns.RemoveAt(i);
                        o.colors.RemoveAt(i);
                        o.toggles.RemoveAt(i);
                        o.info.RemoveAt(i);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(5f);
            if (GUILayout.Button("Clear"))
            {
                //clear all lists
                o.patternNames.Clear();
                o.colors.Clear();
                o.patterns.Clear();
                o.toggles.Clear();
                o.info.Clear();
            }

            EditorGUILayout.Space(30f);
            GUILayout.Label("Save Pattern", EditorStyles.boldLabel);
            assetName = EditorGUILayout.TextField("Asset Name", assetName);
            folder = EditorGUILayout.ObjectField("Folder", folder, typeof(Object));
            assetPath = AssetDatabase.GetAssetPath(folder);
            if (GUILayout.Button("Create"))
            {
                if (assetPath != null && assetName != string.Empty)
                {
                    SO_PatternArray temp = new SO_PatternArray(assetName, o.GetPoints());
                    AssetDatabase.CreateAsset(temp, assetPath + "/" + assetName + ".asset");
                    //remove name after creation, ensures another asset isn't created with same name
                    assetName = string.Empty;
                }
                else
                {
                    Debug.LogError("Either asset name or folder are undefined!");
                }
            }

            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(o);
        }
    }
}