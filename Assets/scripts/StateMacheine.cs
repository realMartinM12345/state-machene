using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMacheine : MonoBehaviour
{
    private RandomColour randomColour;
    public Transform player;
    public enum States
    { 
        Patrol,
        Run,
        idle,
    }
    public States state = States.Patrol;

    private void Start()
    {
        randomColour = GetComponent<RandomColour>();
        NextState();
    }

    void NextState()
    {  
        switch(state)
        {
            case States.Patrol:
            StartCoroutine(PatrolState());
                break;
            case States.Run:
            StartCoroutine(RunState());
                break;
            case States.idle:
                StartCoroutine(IdleState());
                break;

        }
        
    }

    IEnumerator PatrolState()
    {
        Debug.Log("entering patrol state");

        while(state == States.Patrol)
        {
            transform.rotation *= Quaternion.Euler(0f,50f * Time.deltaTime,0f);

            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.Normalize();

            float result = Vector3.Dot(transform.right, directionToPlayer);

            if (result > 0.99)
            {
                state = States.Run;
            }

            yield return null;

        }

        
        Debug.Log("exiting patroll state");
        NextState();

    }



    IEnumerator RunState()
    {
        Debug.Log("enting run state");
        float startTime = Time.time;
        

        while (state == States.Run)
        {   
            randomColour.SetRedColour();
            float wave = Mathf.Sin(Time.time * 30f) * 0.1f + 1f;
            float wave2 = Mathf.Cos(Time.time * 30f) * 0.1f + 1f;
            transform.localScale = new Vector3(wave, wave2,1f);

            float shimmy = Mathf.Sin(Time.time * 30f) * 1f + 1f;
            transform.position += transform.right * shimmy * 5.5f * Time.deltaTime;

            if(Time.time - startTime > 3f)
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
            randomColour.SetOrangeColour();
            if (Time.time - startTime > 3f)
            {
                state = States.Patrol;
            }
            yield return null;
        }
        NextState();
    }


}
