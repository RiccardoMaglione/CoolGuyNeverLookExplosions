using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    #region
    public float viewRadius;			//Variabile che indica quanto è estesa la sua vista
	[Range(0, 360)]
	public float viewAngle;				//Variabile che indica il cono di visione

	public LayerMask targetMask;		//Layer per gli oggetti visibili nel field of view
	public LayerMask obstacleMask;		//Layer per gli ostacoli del field of view

	public List<Transform> visibleTargets = new List<Transform>();

	float timerAim = 0;
	public float CountTimerAim;
	float timerAimCooldown = 0;
	public float CountCooldownTimerAim;
	public bool ActivateTower;							//Booleano che attiva il line renderer della torretta
	public bool CanShot;								//Booleano utilizzato per sparare solo una volta
	public GameObject BulletPrefab;
	public GameObject Eye;
	public GameObject SpawnPoint;
	#endregion

    void Start()
	{
		StartCoroutine("FindTargetsWithDelay", .2f);	//Cerca il target ogni 0.2 secondi
	}

	/// <summary>
	/// IEnumarator che cerca il target ogni delay
	/// </summary>
	/// <param name="delay"></param>
	/// <returns></returns>
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
			GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);														//Resetto il line renderer alla posizione 0
			GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);														//Resetto il line renderer alla posizione 1
		}

		if (ActivateTower == true)
		{
			Eye.transform.LookAt(new Vector3(visibleTargets[0].transform.position.x, transform.position.y, visibleTargets[0].transform.position.z));		//Guarda il player
			timerAim += Time.deltaTime;
			if (timerAim > CountTimerAim)
			{
				GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);													//Resetto il line renderer alla posizione 0
				GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);													//Resetto il line renderer alla posizione 1
				if (CanShot == true)
				{
					print("Spara");
					GameObject Bullet = Instantiate(BulletPrefab, SpawnPoint.transform.position, transform.rotation);		//Istanzia il proiettile nello spawn point
					Bullet.transform.LookAt(visibleTargets[0]);																//Guarda il target player
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
				GetComponent<LineRenderer>().SetPosition(0, transform.localPosition);										//Aggiorna la posizione 0 del line renderer con la posizone della torretta
				GetComponent<LineRenderer>().SetPosition(1, visibleTargets[0].transform.localPosition);						//Aggiorna la posizione 1 del line renderer con la posizone del target
			}
		}
	}

	/// <summary>
	/// Trova il target visibile nell'area di visione
	/// </summary>
	void FindVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);		//Lista di collider degli oggetti dentro il field of view

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