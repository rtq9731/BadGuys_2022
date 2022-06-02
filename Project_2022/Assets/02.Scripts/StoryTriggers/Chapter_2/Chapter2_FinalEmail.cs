using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Chapter2
{

    public class Chapter2_FinalEmail : Triggers.EmailTrigger
    {
        public void TriggerOn()
        {
            if (GameManager.Instance.jsonData.emails.Find(item => item.id == data.id) == null)
            {
                OnTriggered();
            }
        }
    }
}
