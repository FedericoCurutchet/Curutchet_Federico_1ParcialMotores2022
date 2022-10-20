using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMov : MonoBehaviour
{
    public float rapidez = .5f; 
    public float strength = 9f; 
    private float randomOffset; 
    void Start() { 

        randomOffset = Random.Range(0f, 9f); 
    
    }
    void Update() { 

        Vector3 pos = transform.position; 
        pos.x = Mathf.Sin(Time.time * rapidez + randomOffset) * strength; 
        transform.position = pos; 
    
    }
}
