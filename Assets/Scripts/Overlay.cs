using UnityEngine;
using System.Collections;

public class Overlay : MonoBehaviour {
	void Update () {
		transform.Translate(new Vector3(10f * Time.deltaTime, 0f));
	}
}
