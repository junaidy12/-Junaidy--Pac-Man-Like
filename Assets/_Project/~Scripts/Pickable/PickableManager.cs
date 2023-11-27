using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> pickableList = new List<Pickable>();

    private void Start()
    {
        InitPickableList();
    }

    void InitPickableList()
    {
        Pickable[] pickables = FindObjectsOfType<Pickable>();
        foreach (var pickable in pickables)
        {
            pickableList.Add(pickable);
            pickable.OnPicked += (Pickable pickable) =>
            {
                pickableList.Remove(pickable);
                if(pickableList.Count <= 0)
                {
                    Debug.Log("Win");
                }
            };
        }
        Debug.Log(pickableList.Count);
    }
}
