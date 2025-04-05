using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int distance = 0;
    public int coins = 0;

    public Text distanceText;
    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Score());
    }

    // Update is called once per frame
    void Update()
    {
        distanceText.text = $"Distance: {distance}";
        coinsText.text = $"Coins: {coins}";

    }
    public void AddCoin()
    {
        coins++;
        Debug.Log("Coins: " + coins);
    }

    IEnumerator Score()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.8f);
            distance++;
            Debug.Log("Distance: " + distance);
        }
    }
}
