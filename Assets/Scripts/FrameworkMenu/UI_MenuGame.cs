using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MaglioneFramework
{
    public class UI_MenuGame : UI_MenuBase
    {
        #region Variables
        [HideInInspector] public List<Button> ButtonArray = new List<Button>();
        [HideInInspector] public List<GameObject> Panel = new List<GameObject>();
        [HideInInspector] public List<string> Name = new List<string>();

        [HideInInspector] public List<bool> ToggleScene = new List<bool>();
        [HideInInspector] public List<bool> TogglePanel = new List<bool>();
        [HideInInspector] public List<bool> ToggleExit = new List<bool>();
        
        int ID = 0;
        public int TotalIndex;
        #endregion

        private void Start()
        {
            #region Set Listener
            foreach (Button item in ButtonArray)
            {
                if (TogglePanel[ID] == true)
                {
                    item.onClick.AddListener(() => SetActivePanel(ID));
                }
                else if (ToggleScene[ID] == true)
                {
                    item.onClick.AddListener(() => SetChangeScene(ButtonArray.IndexOf(item)));
                }
                else if (ToggleExit[ID] == true)
                {
                    item.onClick.AddListener(() => SetExitApplication(ID));
                }
                ID++;
            }
            ID = 0;
            #endregion
        }

        #region Button Method Listener
        /// <summary>
        /// Metodo che svolge la funzione di listener per il cambio di panel
        /// </summary>
        /// <param name="IDListener"> E' l'indice del bottone </param>
        public void SetActivePanel(int IDListener)
        {
            ID = IDListener;
            OnSetup();
            ToggleMenu(false);
        }
        
        /// <summary>
        /// Metodo che svolge la funzione di listener per il cambio di scena
        /// </summary>
        /// <param name="IDListener"> E' l'indice del bottone </param>
        public void SetChangeScene(int IDListener)
        {
            print(IDListener);
            ID = IDListener;
            SceneManager.LoadScene(Name[ID]);
        }

        /// <summary>
        /// Metodo che svolge la funzione di listener per l'uscita dal gioco
        /// </summary>
        /// <param name="IDListener"> E' l'indice del bottone </param>
        public void SetExitApplication(int IDListener)
        {
            ID = IDListener;
            Debug.Log("Esci dal gioco");
        }
        #endregion

        protected override void OnSetup()
        {
            base.OnSetup();
            IsActive = true;
        }

        public override void ToggleMenu(bool _Value)
        {
            base.ToggleMenu(_Value);

            if (Panel[ID] != null)
                Panel[ID].SetActive(true);
        }
    }
}