using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> pickableList = new List<Pickable>();

    [SerializeField] Player player;
    [SerializeField] ScoreManager scoreManager;

    Coroutine powerUpRoutine;
    private void Start()
    {
        InitPickableList();
    }

    void InitPickableList()
    {
        //Find all GameObject that has Pickable script on it
        Pickable[] pickables = FindObjectsOfType<Pickable>();

        int maxScore = 0;

        //Loop through the list
        foreach (var pickable in pickables)
        {
            if(pickable.PickableType == PickableType.Coin)
            {
                maxScore++;
            }
            //add to pickableList
            pickableList.Add(pickable);

            //subscribe to the OnPicked event
            pickable.OnPicked += Pickable_OnPicked;
        }
        //set max score
        scoreManager.SetMaxScore(maxScore);
    }

    private void Pickable_OnPicked(Pickable pickable)
    {
        //Check if the pickable is a "PowerUp"
        if (pickable.PickableType == PickableType.PowerUp)
        {
            //start powerUP
            PickupPowerUp();
        }
        else
        {
            //add score when pickup coins
            scoreManager.AddScore();
        }
        //remove it from the list
        pickableList.Remove(pickable);
        //check if no more coins to be picked
        if (pickableList.Count <= 0)
        {
            //player win the game
            SceneManager.LoadScene("WinScene");
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
