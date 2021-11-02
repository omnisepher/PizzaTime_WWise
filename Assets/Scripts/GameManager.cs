using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject arrow;

    private GameObject[] totalPeople;
    static public int numTotalPeople;
    static public int fedPeople = 0;

    private GameObject mainPlayer;
    private Transform targetPerson;
    private GameObject uiCam;
    private float minDistance;

    private void Awake()
    {
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        uiCam = GameObject.Find("UICam");
    }

    private void Start()
    {
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
        UpdateDistances();
        UpdateArrow();
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
}
