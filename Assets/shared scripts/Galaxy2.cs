using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy2 : MonoBehaviour
{
	List<Star> mStars = new List<Star>();

	// Load a material by name and assign to the givne game object
	static void LoadMaterialByName(string name, GameObject o)
    {
		Material newMat = Resources.Load(name, typeof(Material)) as Material;
		o.GetComponent<Renderer>().material = newMat;
	}

	static Color ChooseRandomColor()
    {
		return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
	}

	// An interface for planets and suns to define a center of orbital rotation
	public interface OrbitalCenter
	{
		Transform GetTransform();
	}
	
	public class Planet : OrbitalCenter
	{
		List<Planet> mMoons = new List<Planet>();
		OrbitalCenter mOrbitalCenter;
		public GameObject mGameObject;
		float mOrbitalSpeed;
		float mOrbitalDistance;
		Vector3 mAxis;

		public Planet(OrbitalCenter c, float distance, bool spawnMoons)
		{
			mOrbitalCenter = c;
			mGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

			LoadMaterialByName("PlanetMat", mGameObject);

			Vector3 posVector = new Vector3(distance * Random.Range(0.0f, 1.0f), distance * Random.Range(0.0f, 1.0f), distance * Random.Range(0.0f, 1.0f));
			mGameObject.transform.position = mOrbitalCenter.GetTransform().position;
			mGameObject.transform.position += posVector;
			mOrbitalDistance = distance;
			mOrbitalSpeed = Random.Range(30f, 100f);
			mOrbitalSpeed /= mOrbitalDistance;
			mOrbitalSpeed *= 20;
			float scale = Random.Range(.5f, 1.5f);
			mGameObject.transform.localScale = new Vector3(scale, scale, scale);

			if (spawnMoons == true)
			{
				int numMoons = Random.Range(0, 4);
				float moonDistance = Random.Range(2f, 5f);
				if (numMoons > 1) numMoons = 0;
				for (int i = 0; i < numMoons; i++)
				{
					mMoons.Add(new Planet(this, moonDistance, false));
					distance += Random.Range(5f, 10f);
				}
			}
			addTrail();
			Vector3 randomVec = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)).normalized;
			mAxis = Vector3.Cross(posVector, randomVec);
		}

		private void addTrail()
		{
			GameObject trail = GameObject.Find("Trail");
			GameObject clone = Instantiate(trail);
			clone.transform.parent = mGameObject.transform;
			clone.transform.position = mGameObject.transform.position;
			clone.GetComponent<TrailRenderer>().startColor = ChooseRandomColor();
		}

		public void update(float t)
		{
			mGameObject.transform.RotateAround(mOrbitalCenter.GetTransform().position, mAxis, mOrbitalSpeed * t);
			foreach (Planet m in mMoons)
			{
				m.update(t);
			}
		}

		public Transform GetTransform()
		{
			return mGameObject.transform;
		}
	}

	public class Star : OrbitalCenter
	{
		public GameObject mGameObject;
		List<Planet> mPlanets = new List<Planet>();
		float mOrbitalSpeed;
		public Star()
		{
			mGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			LoadMaterialByName("PlanetMat", mGameObject);
			mGameObject.GetComponent<Renderer>().material.SetColor("_Color", ChooseRandomColor());

			mGameObject.transform.position =
			new Vector3(
					Random.Range(-400f, 400f),
					Random.Range(-400f, 400f),
					Random.Range(-400f, 400f));
			float scale = Random.Range(5f, 10f);
			mGameObject.transform.localScale = new Vector3(scale, scale, scale);
			int numPlanets = Random.Range(0, 7);
			float distance = Random.Range(5f, 10f);
			for (int i = 0; i < numPlanets; i++)
			{
				mPlanets.Add(new Planet(this, distance, false));
				distance += Random.Range(5f, 10f);
			}
			mOrbitalSpeed = Random.Range(1f, 10f);
		}
		public void update(float t)
		{
			foreach (Planet p in mPlanets)
			{
				p.update(t);
			}
	//		mGameObject.transform.Rotate(0,0, mOrbitalSpeed * t);
		}

		public Transform GetTransform()
		{
			return mGameObject.transform;
		}
	}




	public int rotationSpeed = 5;
	// Start is called before the first frame update

	void Start()
	{
		for (int i = 0; i < 200; i++)
		{
			mStars.Add(new Star());
		}
	}

	// Update is called once per frame
	void Update()
	{
		foreach (Star star in mStars)
		{
			star.update(Time.deltaTime/1);
		}
	}
}
