using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEnemyController : EnemyController {

	void Fire ()
	{
		if (target != null) {
			float distance = Vector3.Distance (transform.position, target.position);
			if (distance < attackDistance) {
				GameObject obj = (GameObject) Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				obj.GetComponent<TargetingMissile> ().SetTarget (target);
			}
		}
	}
}
