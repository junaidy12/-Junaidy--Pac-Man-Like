using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> pickableList = new List<Pickable>();

    [SerializeField] Player player;

    Coroutine powerUpRoutine;
    private void Start()
    {
        InitPickableList();
    }

    void InitPickableList()
    {
        //Find all GameObject that has Pickable script on it
        Pickable[] pickables = FindObjectsOfType<Pickable>();
        //Loop through the list
        foreach (var pickable in pickables)
        {
            //add to pickableList
            pickableList.Add(pickable);
            //subscribe to the OnPicked event
            pickable.OnPicked += (Pickable pickable) =>
            {
                //Check if the pickable is a "PowerUp"
                if(pickable.PickableType == PickableType.PowerUp)
                {
                    //start powerUP
                    PickupPowerUp();
                }
                //remove it from the list
                pickableList.Remove(pickable);
                //check if the pickableList is empty
                if(pickableList.Count <= 0)
                {
                    //player win the game
                    Debug.Log("Win");
                }
            };
        }
    }

    void PickupPowerUp()
    {
        //if there is a running routine already stop them
        if(powerUpRoutine != null)
        {
            StopCoroutine(powerUpRoutine);
        }
        //start the powerup routine
        powerUpRoutine = StartCoroutine(player.StartPowerUp());
    }
}
