using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	public List<Transform> visibleTargets = new List<Transform>();


	float timerAim = 0;
	public float CountTimerAim;
	float timerAimCooldown = 0;
	public float CountCooldownTimerAim;
	public bool ActivateTower;
	public bool CanShot;
	public GameObject BulletPrefab;
	public GameObject Eye;
	void Start()
	{
		StartCoroutine("FindTargetsWithDelay", .2f);
	}


	IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	private void Update()
	{
		if (visibleTargets.Count == 0)
		{
			ActivateTower = false;
			GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
			GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
		}

		if (ActivateTower == true)
		{
			Eye.transform.LookAt(new Vector3(visibleTargets[0].transform.position.x, transform.position.y, visibleTargets[0].transform.position.z));
			timerAim += Time.deltaTime;
			if (timerAim > CountTimerAim)
			{
				GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
				GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
				if (CanShot == true)
				{
					print("Spara");
					GameObject Bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
					Bullet.transform.LookAt(visibleTargets[0]);
					CanShot = false;
				}
				timerAimCooldown += Time.deltaTime;
				if (timerAimCooldown > CountCooldownTimerAim)
				{
					timerAim = 0;
					timerAimCooldown = 0;
					CanShot = true;
				}
			}
			else
			{
				GetComponent<LineRenderer>().SetPosition(0, transform.localPosition);
				GetComponent<LineRenderer>().SetPosition(1, visibleTargets[0].transform.localPosition);
			}
		}
	}

	void FindVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);

				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					visibleTargets.Add(target);
					ActivateTower = true;
				}
			}
		}
	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}