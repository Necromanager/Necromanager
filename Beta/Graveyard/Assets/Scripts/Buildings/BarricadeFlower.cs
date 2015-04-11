using UnityEngine;
using System.Collections;

public class BarricadeFlower : MonoBehaviour {

    private ParticleSystem particles;

	void Start () {
        //Object[] flowerTexs = Resources.LoadAll("Textures/Flowers/");

        //Renderer rendererComp = GetComponent<Renderer>();

        //rendererComp.material.mainTexture = flowerTexs[Random.Range(0, flowerTexs.Length)] as Texture;

        particles = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            particles.Play();
        }

    }
}
