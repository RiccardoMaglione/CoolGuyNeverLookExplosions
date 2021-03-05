using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaglioneFramework
{
    public abstract class UI_MenuBase : MonoBehaviour
    {
        /// <summary>
        /// E' un riferimento al proprio manager
        /// </summary>
        protected UI_ManagerBase ManagerBase;
        /// <summary>
        /// Stati di attivo e disattivo del menu
        /// </summary>
        protected bool IsActive;

        /// <summary>
        /// Funzione che attiva o disattiva il gameobject del menu
        /// </summary>
        public virtual void ToggleMenu(bool _Value)
        {
            if (IsActive == _Value)
                return;
            IsActive = _Value;
            gameObject.SetActive(IsActive);
        }

        /// <summary>
        /// Setup del menu
        /// </summary>
        public void Setup(UI_ManagerBase _uiManagerBase)
        {
            ManagerBase = _uiManagerBase;
            IsActive = true;
        }

        /// <summary>
        /// Funzione chiamata al setup della classe base
        /// </summary>
        protected virtual void OnSetup()
        {
            
        }

        /// <summary>
        /// Funzione che ritorna il valore true o false del menu attivo
        /// </summary>
        /// <returns> Stato di attivo e disattivo del menu </returns>
        public bool IsMenuActive()
        {
            return IsActive;
        }
    }
}