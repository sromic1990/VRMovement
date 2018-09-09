using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehavior : MonoBehaviour 
{
    // Public variables
    public Color targetedColor;
    public Color normalColor;

    // Private variables
    Material mat;

	private void Start()
	{
        mat = GetComponent<Renderer>().material;
        mat.color = normalColor;
	}

	public void OrbTargeted()
    {
        mat.color = targetedColor;
    }

    public void OrbReset()
    {
        mat.color = normalColor;
    }
}
