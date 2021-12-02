using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] pts;
    public float beat;
    private float timer;
    public GameObject music;
    AudioStuff sounds;
    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("sound"); 
        sounds = music.GetComponent<AudioStuff>();
        timer = sounds.time;//Length of Audio Track
        InvokeRepeating("Realtime", 0f, 1f);//timer/((int)timer));        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Realtime(){

        if(timer>12){ 
            GameObject cube = Instantiate(cubes[Random.Range(0,1)],pts[Random.Range(0,1)]);
            cube.transform.localPosition=Vector3.zero;
            cube.transform.Rotate(transform.forward,90*Random.Range(0,4));
            timer-=beat;
            return;
        }
        CancelInvoke();
    }
}
