using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MaLight : MonoBehaviour {


	// Use this for initialization

    private List<Material> lightMats;
    
	void Start () {

      //  Screen.showCursor = false;

        lightMats = new List<Material>();
        for (int i = 0; i < transform.childCount; i++)
        {
            lightMats.Add(transform.GetChild(i).renderer.material);
        }
	}

    private float timeinterval = 0.3f;
    private float lasttime = -1f;

    float r, g, b;
    Color myColor;
    
	// Update is called once per frame
	void Update () {

        
        
        if (Time.time > lasttime + timeinterval)
        {
            lasttime = Time.time;


            foreach (var mat in lightMats)
            {
                r = Random.Range(0.0f, 1.0f);
                g = Random.Range(0.0f, 1.0f);
                b = Random.Range(0.0f, 1.0f);



                myColor = new Color(r, g, b, 1f);

                mat.color = myColor;
            }
            for (int i = 0; i < transform.childCount; i++)
            {
               // lightMats.Add(transform.GetChild(i).renderer.material);
                transform.GetChild(i).renderer.material.color = lightMats[i].color;
            }
        }
	}
}
