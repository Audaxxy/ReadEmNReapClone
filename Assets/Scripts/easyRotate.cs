using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class easyRotate : MonoBehaviour
{
	// Start is called before the first frame update
	private void FixedUpdate()
	{
		gameObject.transform.Rotate(0,1,0);
	}
}
