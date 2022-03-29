using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternGroup : MonoBehaviour
{
    List<LanternScript> lanterns = new List<LanternScript>();

    private void Start()
    {
        lanterns = GetComponentsInChildren<LanternScript>().ToList();
        lanterns.ForEach(item =>
        {
            item.SetActiveLight(false);
        });
    }

    public void SetActive(bool active)
    {
        if(active)
        {
            StartCoroutine(TurnOnLanterns(lanterns));
        }
        else
        {
            lanterns.ForEach(item => item.SetActiveLight(false));
        }
    }

    private IEnumerator TurnOnLanterns(List<LanternScript> lanterns)
    {
        Queue<LanternScript> lanternQueue = new Queue<LanternScript>(lanterns);
        while (lanternQueue.Count > 0)
        {
            lanternQueue.Dequeue().SetActiveLight(true);
            yield return new WaitForSeconds(1f);
        }
    }

}
