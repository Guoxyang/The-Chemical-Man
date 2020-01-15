using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicChangeShader : MonoBehaviour {

    public bool _ChangeMaterial;
    public Material source;
    public Material dissolve;

    float i = 0;
    float j = 0;
    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    void Update()
    {
        if (_ChangeMaterial)
        {
            
            if (j <= 0.2f)
                j += 0.005f;
            ChangeShader();
            GetComponent<Renderer>().material.SetFloat("_EdgeLength", j);
            if (i <= 1 && j>=0.2f)
                i += 0.005f;
            GetComponent<Renderer>().material.SetFloat("_Threshold",i);
            
        }
        else
        {
            GetComponent<Renderer>().material = source;
        }

    }

    private void ChangeShader()
    {
        GetComponent<Renderer>().material = dissolve;
    }
}
