using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class dataReader : MonoBehaviour{
    //Create serial object
    public SerialPort serial = new SerialPort("\\\\.\\COM4", 9600);
    public GameObject hades,thor,loki;

    // Create data structures
    public string dataPacket;
    public string[] dataParse;
    private string dataStringX, dataStringY, dataStringZ, angle0, angle1;
    public float dataFX, dataFY, dataFZ, dataFloatX, dataFloatY, dataFloatZ, ang0, ang1;
    private int miss =0;
    private int hits = 0;

    void Start(){
        serial.Open();
        // Debug.Log("Data Reader Open");
        dataPacket = serial.ReadLine(); //Read Data coming from Arduino
        dataParse = dataPacket.Split(','); //Split data into string array

        dataStringX = dataParse[0]; //Parse data into individual string variable based on array position
        dataStringY = dataParse[1];
        dataStringZ = dataParse[2];


        float.TryParse(dataStringX, out dataFX); // Convert strings to floats for all variables
        float.TryParse(dataStringY, out dataFY);
        float.TryParse(dataStringZ, out dataFZ);
    
    }

    // Update is called once per frame
    void Update(){
        HadesDestroyer hadesDestroy = hades.GetComponent<HadesDestroyer>();
        saber rightThor = thor.GetComponent<saber>();
        saberL leftLoki = loki.GetComponent<saberL>();
        if(leftLoki.ss+rightThor.ss>hits){
            serial.Write("1");
            hits = leftLoki.ss+rightThor.ss;
        }
        if(hadesDestroy.missedCount>miss){
            serial.Write("2");
            miss = hadesDestroy.missedCount;
        }

        dataPacket = serial.ReadLine(); //Read Data coming from Arduino
        dataParse = dataPacket.Split(','); //Split data into string array

        float.TryParse(dataParse[0], out dataFloatX); // Convert strings to floats for all variables
        float.TryParse(dataParse[1], out dataFloatY);
        float.TryParse(dataParse[2], out dataFloatZ);
        float.TryParse(dataParse[3], out ang0);
        float.TryParse(dataParse[4], out ang1);

        // Debug.Log("Ang0: "+angle0);
        // Debug.Log("Ang1: "+angle1); 
        
        if(dataFloatZ-dataFZ<10){
        transform.position += (new Vector3((dataFloatX-dataFX)/10*Time.deltaTime, (dataFloatY-dataFY)/10*Time.deltaTime, 0));
        }
        if(dataFloatX-dataFX<10){
            transform.Rotate(-(dataFloatZ-dataFZ), -(dataFloatY-dataFY),0);
        }
        
        dataFZ=dataFloatZ;
        dataFY=dataFloatY;
        dataFX=dataFloatX;
        
    }
}