using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaglioneFramework
{
    public class UI_ManagerGame : UI_ManagerBase
    {
        void Start()
        {
            Setup();
        }

        protected override void OnSetup()
        {
            for (int i = 0; i < Menues.Count; i++)
            {
                if(i != 0)
                {
                    Menues[i].gameObject.SetActive(false);
                    Menues[i].Setup(this);
                }
                else
                {
                    GetCurrentMenu();
                }
            }
        }
    }
}
