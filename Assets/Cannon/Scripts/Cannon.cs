using UnityEngine;
using System.Collections;
using System.Reflection; //make sure you add this!

public class Cannon : MonoBehaviour
{
	public GameObject objetoAInstanciar;
    public Transform lugar ;
	public float timeOfShot = 3f;

	private bool IsAvailable = true;
	private float CooldownDuration = 1.0f;
	private const float plusBullets = .75f;
	private float resetTime = 0f;
	private float tempElapsedSeconds =0f;

	private void Start(){
		resetTime = timeOfShot;
		configReset();
	}

	private void configReset(){
		timeOfShot = resetTime;
		tempElapsedSeconds = timeOfShot;
	}


	public void ClearLog() //you can copy/paste this code to the bottom of your script
	{
		var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
		var type = assembly.GetType("UnityEditor.LogEntries");
		var method = type.GetMethod("Clear");
		method.Invoke(new object(), null);
	}


    private void Update()
    {
		temporizador();
    }

	private void Shoot(int NumberOfBullets)
	{
		IsAvailable = false;
		for(int i = 0; i < NumberOfBullets; i++){
			Instantiate(	objetoAInstanciar,
								new Vector3(	lugar.position.x-(plusBullets*i), 
												lugar.position.y-(plusBullets*i),
												lugar.position.z-(plusBullets*i)),
								transform.rotation	);	
		}
		// start the cooldown timer
        StartCoroutine(StartCooldown(CooldownDuration));			
	}	

	public IEnumerator StartCooldown(float CooldownDuration)
	{
		yield return new WaitForSeconds(CooldownDuration);
		IsAvailable = true;
	}

	void temporizador(){
		if(timeOfShot <= 0){
			Debug.LogWarning("Booom");
			Shoot(1);
			configReset();
			ClearLog();
		}
		timeOfShot -= Time.deltaTime;	
		if(timeOfShot < tempElapsedSeconds )
		{
			Debug.Log("Wait : " + timeOfShot.ToString("n0")+" ...");	
			tempElapsedSeconds =  Mathf.FloorToInt(timeOfShot);	
		}
	}


}
