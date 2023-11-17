using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public int carrotCount;
    public Text carrotText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        carrotText.text = carrotCount.ToString();
    }
}
