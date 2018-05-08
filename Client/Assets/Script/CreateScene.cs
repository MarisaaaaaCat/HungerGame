using System.Collections.Generic;
using Script;
using UnityEngine;

public class CreateScene : MonoBehaviour
{
	private List<Vector2> positions;
	private GameObject root;
	private const int scale = 100;
	
	void Start () {
		this.InitScene();
		this.CreatRoad(300);
		this.CreatPlayer();
		this.CreatGiant();
	}


	void InitScene()
	{
		if (root==null)
		{
			root=new GameObject("Root");
			root.transform.position=new Vector3(0f,0f,100f);
		}
	}
	
	void CreatRoad(int nums)
	{
		for (int i = 0; i < nums; i++)
		{
			CreatCube();
		}
	}
	
	void CreatCube()
	{	
		positions=new List<Vector2>();
		var pos = Random.insideUnitCircle;
		if (!positions.Contains(pos))
		{
			var go=GameObject.CreatePrimitive(PrimitiveType.Cube);
			var targetPos = pos * scale;
			go.transform.position = new Vector3(targetPos.x,0f,targetPos.y);	
			this.positions.Add(pos);
			go.transform.SetParent(root.transform,false);
		}	
	}

	void CreatPlayer()
	{
		Vector2 playerPos = this.positions[Random.Range(0, this.positions.Count)];
		var go=GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.name = "Player";
		var targetPos = playerPos * scale;
		go.transform.position = new  Vector3(targetPos.x,1f,targetPos.y);
		go.transform.localScale=new Vector3(.5f,.5f,.5f);
		go.transform.SetParent(root.transform,false);
		go.AddComponent<Rigidbody>();
		go.GetComponent<MeshRenderer>().material.color=Color.red;
		go.AddComponent<Player>();
	}

	void CreatGiant()
	{
		var go=GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.name = "Giant";
		go.transform.localScale=new Vector3(3,10,3);
		go.transform.position = Vector3.zero;
		go.transform.SetParent(root.transform,false);
		go.GetComponent<MeshRenderer>().material.color=Color.black;
	}

	private const string GUIContent = "Press Space To Jump \n W front \n S back \n A left  \n D right";
	void OnGUI()
	{
		GUI.color=Color.black;
		GUI.TextArea(new Rect(10f,0f,150f,150f),GUIContent);
	}
	
}
