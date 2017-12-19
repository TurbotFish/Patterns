using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternObject{
	public GameObject obj;
	public float probability;
	public float space;
}

public class Reference{
	public GameObject obj;
	public Vector2 refPos;
}

public class Generator : MonoBehaviour {
	public Vector2 patternOrigin;
	public float patternDensity;
	public float patternSize;
	public float randomness;
	public List<PatternObject> patternObjects = new List<PatternObject>();
	List<Reference> references = new List<Reference>();

	// Use this for initialization
	void Start () {
		CreatePattern(patternOrigin, patternDensity, patternSize);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreatePattern(Vector2 origin, float density, float size)
	{
		density = patternDensity;
		float x;
		float y;
		float maxX;
		float maxY;
		x = origin.x;
		y = origin.y;
		maxX = x + size;
		maxY = y + size;

		int rows = Mathf.CeilToInt(maxX / density);
		//int columns = Mathf.CeilToInt(maxY / density);

		for (int i = 0; i<= rows; i++)
		{
			for (int j = 0; j<= rows;j++)
			{
				Vector2 offset = new Vector2(i*density,j*density);
				InstantiateObject(origin+offset);
			}
		}



	}

	Vector2 PosRandom(Vector2 basePos)
	{
		Vector2 pos;
		pos = basePos + new Vector2 (Random.Range(-randomness,randomness), Random.Range(-randomness,randomness));
		return pos;
	}

	void InstantiateObject (Vector2 pos)
	{
		float totalProb = 0;
		foreach(PatternObject po in patternObjects)
		{
			totalProb += po.probability;
		}
		float p = Random.Range(0,totalProb);

		int index = 0;
		for (int i = 0; i<patternObjects.Count ; i++)
		{
			float test = 0;

			for (int j = 0; i<j; j++)
			{
				test += patternObjects[j].probability;
			}

			if (p<=test)
			{
				index = i;
				break;
			}
		}

		GameObject go;
		go = Instantiate (patternObjects[index].obj,PosRandom(pos),Quaternion.identity) as GameObject;
	}
}

