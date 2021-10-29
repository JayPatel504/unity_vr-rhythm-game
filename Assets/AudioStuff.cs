using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioStuff : MonoBehaviour
{
    AudioSource m_AudioSource;
    public float time;
    public TMP_Text m_TextComponent;
    public GameObject hades,thor,loki;
    public TMP_Text time_text;
    private string temp;
    private float countDown;
    AudioClip m_AudioClip;
    public string pathName = "data.txt";
    public Slider volRocker;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioClip = m_AudioSource.clip;
        time = m_AudioClip.length;
        m_AudioSource.PlayDelayed(12);
        countDown= time+6;        
    }

    // Update is called once per frame
    void Update()
    {
        m_AudioSource.volume = volRocker.value;
        if (countDown>time-6){
            countDown-=Time.deltaTime;
            time_text.text = "Time Left : " +countDown.ToString("F2");
        }
        else{
            if (countDown>0){
                               
                HadesDestroyer hadesDestroy = hades.GetComponent<HadesDestroyer>();
                saber rightThor = thor.GetComponent<saber>();
                saberL leftLoki = loki.GetComponent<saberL>();
                temp = ((leftLoki.ss+rightThor.ss+hadesDestroy.missedCount == 0) ? 100 : (((float) leftLoki.ss+rightThor.ss)/(leftLoki.ss+rightThor.ss+hadesDestroy.missedCount)*100)).ToString("F2");
                m_TextComponent.text = ("Accuracy: "+temp+"%");
                countDown-=Time.deltaTime;
                time_text.text = "Time Left : " +countDown.ToString("F2");
            }
            else{
                HadesDestroyer hadesDestroy = hades.GetComponent<HadesDestroyer>();
                saber rightThor = thor.GetComponent<saber>();
                saberL leftLoki = loki.GetComponent<saberL>();
                using (StreamWriter file=new StreamWriter(pathName,true)){
                    string output = string.Format("Hit:,{0},Missed:,{1},Total:,{2},Accuracy:,{3}",leftLoki.ss+rightThor.ss,hadesDestroy.missedCount,leftLoki.ss+rightThor.ss+hadesDestroy.missedCount,temp);
                    file.WriteLine(output);
                }
                SceneManager.LoadScene(sceneName:"SampleScene");
            }
        }
    }  
}
