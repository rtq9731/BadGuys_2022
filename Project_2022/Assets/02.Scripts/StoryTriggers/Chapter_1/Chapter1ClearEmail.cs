using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Chapter1
{
    public class Chapter1ClearEmail : EmailTrigger
    {
        public override void OnTriggered()
        {
            if (GameManager.Instance.jsonData.emails.Find(item => item.id == data.id) == null)
            {
                base.OnTriggered();
            }
        }
    }
}
