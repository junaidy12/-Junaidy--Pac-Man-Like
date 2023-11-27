using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    enum PickableType { Coin,PowerUp }
    [SerializeField] PickableType type;

    public Action<Pickable> OnPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(type + ", Triggered");
            OnPicked?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
