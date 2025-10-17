using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerMovementBase>(out PlayerMovementBase _Player))
        {
             GameController.Instance.nextMap();
        }    
    }
}
