using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaLightLight1 : MonoBehaviour {

	// Use this for initialization
    List<Light> mylight;
    public GameObject refence;
    private float flashrate;
	void Start () {
        mylight = new List<Light>();
        for (int i = 0; i < transform.childCount; i++)
        {
            mylight.Add(transform.GetChild(i).light);
        }
        flashrate = refence.GetComponent<MaLightLight>().flashrate;
	}
	
	// Update is called once per frame

    private float lasttime=-1f;
    //private float timeinteval = 0.3f;
    float r, g, b;
    Color myColor;
   // MaLightLight ml;
   
	void Update () {
       
        transform.Rotate(0,300*Time.deltaTime, 0);
        if (Time.time > lasttime + flashrate)
        {
            lasttime = Time.time;
            foreach (var lt in mylight)
            {
                r = Random.Range(0.0f, 1.0f);
                g = Random.Range(0.0f, 1.0f);
                b = Random.Range(0.0f, 1.0f);



                myColor = new Color(r, g, b, 1f);

                lt.color = myColor;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                // lightMats.Add(transform.GetChild(i).renderer.material);
               // transform.GetChild(i).renderer.material.color = lightMats[i].color;
                transform.GetChild(i).light.color = mylight[i].color;
            }
        }
	}
}
