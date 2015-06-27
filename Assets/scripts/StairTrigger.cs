using UnityEngine;
using System.Collections;

public class StairTrigger : MonoBehaviour {
	StairManagement stairs;

	// Use this for initialization
	void Start () {
		stairs = transform.parent.GetComponent <StairManagement> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider otherCollider)
	{
		if (otherCollider.gameObject.name == "Player")
		{
			stairs.setApproachingStairs (true);
		}
	}
	
	void OnTriggerExit (Collider otherCollider)
	{
		stairs.setApproachingStairs (false);
	}
}
