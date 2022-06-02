using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Title
{
    public class StartEmail : EmailTrigger
    {
        private void Start()
        {
            if (GameManager.Instance.jsonData.emails.Find(item => item.id == data.id) == null)
            {
                base.OnTriggered();
            }
        }
    }
}
