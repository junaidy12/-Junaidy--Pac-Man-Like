using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [field: SerializeField] public PickableType PickableType { get; private set; }

    public Action<Pickable> OnPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(PickableType + ", Triggered");
            OnPicked?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
public enum PickableType 
{ 
    Coin,
    PowerUp 
}
