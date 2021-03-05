using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace MaglioneFramework
{
    [CustomEditor(typeof(UI_MenuGame))]
    public class CustomMenu : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
    
            serializedObject.Update();
            
            UI_MenuGame ui_MenuGame = (UI_MenuGame)target;
    
            for (int i = 0; i < ui_MenuGame.TotalIndex; i++)
            {
                if (ui_MenuGame.ButtonArray.Count < ui_MenuGame.TotalIndex)
                {
                    ui_MenuGame.ButtonArray.Add(null);
                    ui_MenuGame.Panel.Add(null);
                    ui_MenuGame.ToggleScene.Add(false);
                    ui_MenuGame.TogglePanel.Add(false);
                    ui_MenuGame.ToggleExit.Add(false);
                    ui_MenuGame.Name.Add(null);
                }
                else if (ui_MenuGame.ButtonArray.Count > ui_MenuGame.TotalIndex)
                {
                    ui_MenuGame.ButtonArray.RemoveAt(ui_MenuGame.ButtonArray.Count - 1);
                    ui_MenuGame.Panel.RemoveAt(ui_MenuGame.Panel.Count - 1);
                    ui_MenuGame.ToggleScene.RemoveAt(ui_MenuGame.ToggleScene.Count - 1);
                    ui_MenuGame.TogglePanel.RemoveAt(ui_MenuGame.TogglePanel.Count - 1);
                    ui_MenuGame.ToggleExit.RemoveAt(ui_MenuGame.ToggleExit.Count - 1);
                    ui_MenuGame.Name.RemoveAt(ui_MenuGame.ToggleExit.Count - 1);
                }

                if (ui_MenuGame.ToggleScene[i] == true)
                {
                    ui_MenuGame.TogglePanel[i] = false;
                    ui_MenuGame.ToggleExit[i] = false;
                    ui_MenuGame.ToggleScene[i] = true;
                }
                if (ui_MenuGame.TogglePanel[i] == true)
                {
                    ui_MenuGame.ToggleScene[i] = false;
                    ui_MenuGame.ToggleExit[i] = false;
                    ui_MenuGame.TogglePanel[i] = true;
                }
                if (ui_MenuGame.ToggleExit[i] == true)
                {
                    ui_MenuGame.ToggleScene[i] = false;
                    ui_MenuGame.TogglePanel[i] = false;
                    ui_MenuGame.ToggleExit[i] = true;
                }
            }

            GUIStyle CenterBoldStyle;
            CenterBoldStyle = EditorStyles.boldLabel;
            CenterBoldStyle.alignment = TextAnchor.MiddleCenter;


            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, GUI.color.a * 2f);
            EditorGUILayout.TextField("Button - OnClick", CenterBoldStyle);
            EditorGUILayout.TextField("Name Scene / Panel to Activate / Null", CenterBoldStyle);
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, GUI.color.a / 2f);
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            for (int i = 0; i < ui_MenuGame.TotalIndex; i++)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                ui_MenuGame.ButtonArray[i] = (Button)EditorGUILayout.ObjectField(ui_MenuGame.ButtonArray[i], typeof(Button), true);

                if (ui_MenuGame.ToggleScene[i] == true)
                {
                    ui_MenuGame.Name[i] = EditorGUILayout.TextField(ui_MenuGame.Name[i]);
                }
                else if(ui_MenuGame.TogglePanel[i] == true)
                {
                    ui_MenuGame.Panel[i] = (GameObject)EditorGUILayout.ObjectField(ui_MenuGame.Panel[i], typeof(GameObject), true);
                }


                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginHorizontal();

                GUI.enabled = false;
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, GUI.color.a * 2f);
                EditorGUILayout.TextField("Listener: Is Change Scene:", GUI.skin.label);
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, GUI.color.a / 2f);
                GUI.enabled = true;
                ui_MenuGame.ToggleScene[i] = EditorGUILayout.Toggle(ui_MenuGame.ToggleScene[i]);

                GUI.enabled = false;
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, GUI.color.a * 2f);
                EditorGUILayout.TextField("Listener: Is Active Panel:", GUI.skin.label);
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, GUI.color.a / 2f);
                GUI.enabled = true;
                ui_MenuGame.TogglePanel[i] = EditorGUILayout.Toggle(ui_MenuGame.TogglePanel[i]);

                GUI.enabled = false;
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, GUI.color.a * 2f);
                EditorGUILayout.TextField("Listener: Is Exit:", GUI.skin.label);
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, GUI.color.a / 2f);
                GUI.enabled = true;
                ui_MenuGame.ToggleExit[i] = EditorGUILayout.Toggle(ui_MenuGame.ToggleExit[i]);
                
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(10);
            }
    
            serializedObject.ApplyModifiedProperties();
        }
    }
}
