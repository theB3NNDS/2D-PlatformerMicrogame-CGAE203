using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class itemCollector : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] private Text coinCounter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coins++;
            coinCounter.text = "Coins: " + coins;
        }
    }
}
