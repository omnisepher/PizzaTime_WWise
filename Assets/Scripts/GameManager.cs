using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Sprite levelClearText, levelLostText;
    [SerializeField]
    public Image endLevelText;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private Image clockBar;
    static public float maxTime;
    static public float timer;

    private GameObject[] totalPeople;
    static public int numTotalPeople;
    static public int fedPeople = 0;
    static public bool levelClear = false;
    static public bool levelLose = false;

    private GameObject mainPlayer;
    private Transform targetPerson;
    private GameObject uiCam;
    private float minDistance;


    private static float addTime = 15f;

    private void Awake()
    {
        maxTime = 20f;
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        uiCam = GameObject.Find("UICam");
        timer = maxTime;
    }

    private void Start()
    {
        endLevelText.sprite = levelLostText;

        levelClear = false;
        levelLose = false;

        uiCam.SetActive(false);
        uiCam.SetActive(true);
        fedPeople = 0;

        if (totalPeople == null)
        {
            totalPeople = GameObject.FindGameObjectsWithTag("People");
        }
        numTotalPeople = totalPeople.Length;


    }

    private void Update()
    {
        ClockTick();
        UpdateDistances();
        UpdateArrow();
        CheckGameStatus();
    }

    private void UpdateDistances()
    {

        if (numTotalPeople - fedPeople > 0)
        {
            Transform localTargetPerson = null;
            float localMinDistance = float.MaxValue;
            foreach (var person in totalPeople)
            {
                if (!(person.GetComponent<PeopleBase>().fed))
                {
                    float distance = Vector3.Distance(mainPlayer.transform.position, person.transform.position);
                    if (distance < localMinDistance)
                    {
                        localMinDistance = distance;
                        localTargetPerson = person.transform;

                    }
                }

            }
            minDistance = localMinDistance;
            targetPerson = localTargetPerson;

        }
        else
        {
            targetPerson = null;
        }



    }

    private void UpdateArrow()
    {
        if (targetPerson)
        {
            arrow.SetActive(true);
            arrow.transform.LookAt(targetPerson);
            arrow.transform.Rotate(0, 180, 0, Space.Self);
        }
        else
        {
            arrow.SetActive(false);
        }
    }

    private void CheckGameStatus()
    {
        if (fedPeople == numTotalPeople)
        {
            mainPlayer.GetComponent<Animator>().SetBool("Cheer", true);
            levelClear = true;
            endLevelText.sprite = levelClearText;
        }
        else
        {
            levelClear = false;
        }
    }

    private void ClockTick()
    {
        if (!levelLose)
        {
            if (timer > 0f)
            {

                timer = timer < 0f ? 0f : timer - Time.deltaTime;
                clockBar.fillAmount = timer / maxTime;

            }
            else
            {
                levelLose = true;
            }

        }
        else
        {
            mainPlayer.GetComponent<Animator>().SetTrigger("Cry");
        }
    }

    static public void AddTime()
    {
        timer = timer + addTime >= maxTime ? maxTime : timer + addTime;
        UIManager.AddTime();
    }
}
