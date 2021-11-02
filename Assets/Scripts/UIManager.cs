using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI progressTest;

    private void Update()
    {
        ProgressTextUpdate();
    }

    private void ProgressTextUpdate()
    {
        progressTest.text = GameManager.fedPeople + "/" + GameManager.numTotalPeople;
    }

}
