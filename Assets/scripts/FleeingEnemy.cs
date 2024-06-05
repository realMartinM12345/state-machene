using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingEnemy : MonoBehaviour
{
    private RandomColour randomColour;
    public Transform player;
    public enum States
    {
        Search,
        Flee,
        idle,
    }
    public States state = States.Flee;

    private void Start()
    {
        randomColour = GetComponent<RandomColour>();
        NextState();
    }

    void NextState()
    {
        switch (state)
        {
            case States.Search:
                StartCoroutine(SearchState());
                break;
            case States.Flee:
                StartCoroutine(FleeState());
                break;
            case States.idle:
                StartCoroutine(IdleState());
                break;

        }

    }

    IEnumerator SearchState()
    {
        Debug.Log("entering searchingstate");

        int rotleft = Random.Range(0, 2);

        float rotateTime = Random.Range(1f,6f);

        
        while (state == States.Search)
        {
            randomColour.SetYellowColour();
            rotateTime -= Time.deltaTime;
            if(rotateTime < 0f)
            {
                state = States.idle;
                break;
            }

            if(rotleft == 1)
            {
                transform.rotation *= Quaternion.Euler(0f, 60f * Time.deltaTime, 0f);
            }
            else
            {
                transform.rotation *= Quaternion.Euler(0f, -60f * Time.deltaTime, 0f);
            }
            

            Vector3 directionToPlayer = player.position - transform.position;
            if(directionToPlayer.magnitude < 8f)
            {
                directionToPlayer.Normalize();
                float result = Vector3.Dot(transform.right, directionToPlayer);
                if (result > 0.95)
                {
                    state = States.Flee;
                }
            }




            yield return null;

        }


        Debug.Log("exiting Searching state");
        NextState();

    }



    IEnumerator FleeState()
    {
        Debug.Log("enting fleeing state");
        float startTime = Time.time;


        while (state == States.Flee)
        {
            randomColour.SetGreenColour();
            float wave = Mathf.Sin(Time.time * 30f) * 0.1f + 1f;
            float wave2 = Mathf.Cos(Time.time * 30f) * 0.1f + 1f;
            transform.localScale = new Vector3(wave, wave2, 1f);

            float shimmy = Mathf.Sin(Time.time * 30f) * 1f + 1f;
            transform.position -= transform.right * shimmy * 3.5f * Time.deltaTime;

            if (Time.time - startTime > 4f)
            {
                state = States.idle;
            }

            yield return null;
        }
        Debug.Log("exiting run state");
        NextState();
    }

    IEnumerator IdleState()
    {
        float startTime = Time.time;
        while (state == States.idle)
        {
            //Change to random colours
            randomColour.SetBlueColour();

            Vector3 directionToPlayer = player.position - transform.position;
            if (directionToPlayer.magnitude < 7f)
            {
                directionToPlayer.Normalize();
                float result = Vector3.Dot(transform.right, directionToPlayer);
                if (result > 0.95)
                {
                    state = States.Flee;
                }
            }

            float shimmy = Mathf.Sin(Time.time * 30f) * 1f + 1f;
            transform.position -= transform.right * shimmy * 2.5f * Time.deltaTime;
            
            if (Time.time - startTime > 3f)
            {
                
                state = States.Search;
            }
            yield return null;
        }
        NextState();
    }


}
