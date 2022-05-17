using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Tutorial
{
    public class TutorialClearEmail : EmailTrigger
    {
        private void OnDestroy()
        {
            if (GameManager.Instance.jsonData.emails.Find(item => item.id == data.id) == null)
            {
                OnTriggered();
            }
        }
    }
}
