using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField] GameObject Circle;
    [SerializeField] GameObject Triangle;
    [SerializeField] GameObject Square;
    [SerializeField] GameObject Cross;

    // Update is called once per frame
    void Update()
    {
        Sequence();
    }

    private void Sequence()
    {
        //Sequence the lights to prevent lighting glitch when all on at the same time
        if (Circle.activeSelf) Cross.SetActive(false); Circle.SetActive(true);
        if (Triangle.activeSelf) Circle.SetActive(false); Triangle.SetActive(true);
        if (Square.activeSelf) Triangle.SetActive(false); Square.SetActive(true);
        if (Cross.activeSelf) Square.SetActive(false); Cross.SetActive(true);
    }
}
