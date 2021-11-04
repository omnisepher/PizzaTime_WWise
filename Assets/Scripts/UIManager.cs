using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI progressTest;
    [SerializeField]
    static private Animator addTimeAnim;

    private void Awake()
    {
        addTimeAnim = GameObject.Find("AddTime").GetComponent<Animator>();
    }

    private void Update()
    {
        ProgressTextUpdate();
    }

    private void ProgressTextUpdate()
    {
        progressTest.text = GameManager.fedPeople + "/" + GameManager.numTotalPeople;
    }

    static public void AddTime()
    {
        addTimeAnim.SetTrigger("AddTime");
    }

}
