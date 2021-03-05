using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace MaglioneFramework
{
    public abstract class UI_ManagerBase : MonoBehaviour
    {
        /// <summary>
        /// Lista dei menu
        /// </summary>
        protected List<UI_MenuBase> Menues;
        /// <summary>
        /// Menu attualmente selezionato
        /// </summary>
        protected UI_MenuBase CurrentMenu;

        /// <summary>
        /// Funzione che ritorna il menu attivo
        /// </summary>
        /// <returns> Menu attualmente selezionato </returns>
        protected UI_MenuBase GetCurrentMenu()
        {
            return CurrentMenu;
        }

        /// <summary>
        /// Funzione chiamat al setup della classe base
        /// </summary>
        protected virtual void OnSetup()
        {

        }

        /// <summary>
        /// Setup della classe
        /// </summary>
        public void Setup()
        {
            Menues = GetComponentsInChildren<UI_MenuBase>().ToList();

            foreach (UI_MenuBase item in Menues)
                item.Setup(this);
            OnSetup();
        }
    }
}