using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaLightLight : MonoBehaviour {

	// Use this for initialization
    public  float flashrate = 0.3f;
   
    List<Light> mylight;
	void Start () {
        mylight = new List<Light>();
        for (int i = 0; i < transform.childCount; i++)
        {
            mylight.Add(transform.GetChild(i).light);
        }

	}
	
	// Update is called once per frame

    private float lasttime=-1f;
    
    float r, g, b;
    Color myColor;
   
	void Update () {

        flashrate =(float)((flashrate < 0.3f) ? 0.3 : flashrate);

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
