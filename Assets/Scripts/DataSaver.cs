using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataSaver : ScriptableObject
{
	private int _score;
	private int _level;

	public int Score
	{
		get { return _score; }
		set { _score = value; }
	}

	public int Level
	{
		get { return _level; }
		set { _level = value; }
	}

}
