using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers
{
    public abstract class EmailTrigger : MonoBehaviour
    {
        [SerializeField] protected EmailData data;
        public virtual void OnTriggered()
        {
            if(GameManager._instance.jsonData.emails.Find(x => x.id == data.id) == null)
            {
                data.sendTime = new System.DateTime(2040, System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);
                GameManager._instance.jsonData.emails.Add(data);
                GameManager._instance.SaveEmailData();
            }
            else
            {
                EmailData curData = GameManager._instance.jsonData.emails.Find(x => x.id == data.id);

                curData.textDataID = data.textDataID;
            }
        }
    }
}
