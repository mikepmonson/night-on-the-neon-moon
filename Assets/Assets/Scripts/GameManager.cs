using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public enum GameState
	{
		InProgress,
		Paused
	}
	public GameObject braindeadObject;
	public GameObject shooterObject;
	public GameObject player;
	public List<GameObject> enemies;
	public GameObject[] buildings = new GameObject[4];
	public ushort PowerupProgress = 0;
	public ushort PowerupStock = 0;
	public GameState gameState = GameState.InProgress;

	public long Score = 0;

	private ushort PROGRESS_NEEDED_FOR_POWERUP = 3;
	private ushort MAXIMUM_POWERUP_STOCK = 3;

	// Wave variables
	public uint waveNo = 0;
	public uint enemiesToSpawn = 20;
	public float timeBetweenSpawns = 1f;
	public float chanceForShooter = 1f; // starts at one in ten

	private float _nextSpawnTime;
	/// <summary>
	/// This rectangle defines the spots in the game area where enemies will spawn.
	/// They will spawn on the edges of the rectangle.
	/// </summary>
	private Rect spawnRect;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		// TODO: later will be populated individually via inspector so we can determine which health bar to show on which corner of the UI
		buildings = GameObject.FindGameObjectsWithTag("Building");

		// TODO: better spawn area definitions
		Camera c = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		Vector2 bottomLeft = c.ViewportToWorldPoint(new Vector3(0, 0));
		Vector2 upperRight = c.ViewportToWorldPoint(new Vector3(1f, 1f));
		bottomLeft -= new Vector2(1f, 1f);
		upperRight += new Vector2(1f, 1f);
		var width = upperRight.x - bottomLeft.x;
		var height = upperRight.y - bottomLeft.y;
		spawnRect = new Rect(bottomLeft, new Vector2(width, height));

		waveNo = 1;
	}
	
	void Update ()
	{
		if (enemiesToSpawn > 0 && _nextSpawnTime <= Time.time)
		{
			enemiesToSpawn--;
			SpawnEnemy();
		}
		else if (enemies.Count == 0 && enemiesToSpawn == 0)
		{
			// wave clear
		}
	}

	void SpawnEnemy()
	{
		GameObject toSpawn;
		toSpawn = UnityEngine.Random.value < chanceForShooter ? shooterObject : braindeadObject;
		//Vector2 point = GetPointOnEdgeOfRectangle(spawnRect, UnityEngine.Random.value * 360);
		//Vector3 spawnPoint = new Vector3(point.x, point.y, 0);
		var spawnPoint = PickSpawnPoint();

		var newEnemy = Instantiate(toSpawn, spawnPoint, Quaternion.identity);
		enemies.Add(newEnemy);
		_nextSpawnTime = Time.time + timeBetweenSpawns;
	}

	Vector2 PickSpawnPoint()
	{
		byte edge = Convert.ToByte(Math.Ceiling(UnityEngine.Random.value * 4));
		Vector3 point;
		float x, y;
		float rngResult;
		switch (edge)
		{
			case 1: // top
				rngResult = UnityEngine.Random.value * spawnRect.width;
				point = new Vector3(spawnRect.xMin + rngResult, spawnRect.yMax, 0);
				break;
			case 2: // bottom
				rngResult = UnityEngine.Random.value * spawnRect.width;
				point = new Vector3(spawnRect.xMin + rngResult, spawnRect.yMin, 0);
				break;
			case 3: // left
				rngResult = UnityEngine.Random.value * spawnRect.height;
				point = new Vector3(spawnRect.xMin, spawnRect.yMin + rngResult, 0);
				break;
			case 4: // right
				rngResult = rngResult = UnityEngine.Random.value * spawnRect.height;
				point = new Vector3(spawnRect.xMax, spawnRect.yMin + rngResult, 0);
				break;
			default:
				throw new IndexOutOfRangeException();
		}
		return point;
	}

	void PowerupIncrement()
	{
		PowerupProgress++;
		if (PowerupProgress >= PROGRESS_NEEDED_FOR_POWERUP && PowerupStock != MAXIMUM_POWERUP_STOCK)
		{
			PowerupProgress = 0;
			PowerupStock++;
		}
	}

	void GameOver()
	{
		throw new NotImplementedException();
	}
}
