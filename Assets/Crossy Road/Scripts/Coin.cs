using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player picked up a coin");
            Manager.instance.UpdateCoinCount(coinValue);
            Destroy(this.gameObject);
        }
    }
}
