using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using TMPro;
 
public class saber : MonoBehaviour
{
    public LayerMask layer;
    private Vector3 prevPos;
    public int ss=0;
    public GameObject flex;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dataReader ang = flex.GetComponent<dataReader>();
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit,1,layer)){
            if(Vector3.Angle(transform.position-prevPos,hit.transform.up)>130){
                if(ang.ang0!=0 || ang.ang1!=0){
                    Destroy(hit.transform.gameObject);
                    ss++;
                }
               
            }           
        }
        prevPos=transform.position;
    }
}
