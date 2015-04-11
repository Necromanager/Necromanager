using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarricadeFlowers : MonoBehaviour 
{
	//private const int MIN_FLOWERS = 3;
	//private const int MAX_FLOWERS = 5;
	[SerializeField] private int minFlowers;
	[SerializeField] private int maxFlowers;
	
	private List<GameObject> flowers;

	void Start () 
	{
		CreateFlowers();
	}
	
	void Update () 
	{
	
	}
	
	void OnDestroy()
	{
		foreach (GameObject flower in flowers)
		{
			Destroy(flower);
		}
	}
	
	private void CreateFlowers()
	{
		flowers = new List<GameObject>();
		//Object[] flowerTexs = Resources.LoadAll("Textures/Flowers/");
		
		int numFlowers = Random.Range(minFlowers,maxFlowers+1);
		float xPos = Random.Range(-0.5f,0.5f);
		float zPos = Random.Range(-0.5f,0.5f);
		Vector3 pos = transform.position;
		
		for (int i=0; i<numFlowers; i++)
		{
			//GameObject flower = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GameObject flower = GameObject.Instantiate(Resources.Load("Prefabs/Flower")) as GameObject;
			
			xPos = Random.Range(-0.5f,0.5f);
			zPos = Random.Range(-0.5f,0.5f);
			flower.transform.position = new Vector3(pos.x+xPos,0.0f,pos.z+zPos);
			flower.transform.rotation = Quaternion.Euler(new Vector3(90.0f, Random.Range(0.0f, 360.0f), 0.0f));
            //flower.transform.localScale = new Vector3(0.07f,1,0.1f);
            //flower.transform.Rotate(-90,0,0);
            //flower.transform.Rotate(0,180,0);
			
			//flower.GetComponent<MeshCollider>().enabled = false;
			//flower.GetComponent<Renderer>().material.mainTexture = flowerTexs[Random.Range(0,flowerTexs.Length)] as Texture;
			//flower.GetComponent<Renderer>().material.shader = Shader.Find("Necromanager/Standard");
			//flower.GetComponent<Renderer>().material.color = new Color(1,1,1,1);
			
			flowers.Add(flower);
		}
	}
}
