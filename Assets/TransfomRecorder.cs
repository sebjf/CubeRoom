using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class TransfomRecorder : MonoBehaviour {

	private string Directory = @"C:\HeadLogs\";
	private string NameFormat = "Log_{0}.csv";
	private int LogNumber = 0;

	struct transformRecord
	{
		public float timestamp;
		public Quaternion orientation;
		public Vector3 position;
		public Vector3 lookat;
		public Vector3 eulerAngles;

		public string ToString()
		{
			return (timestamp + "," + orientation.ToString() + "," + position.ToString() + "," + lookat.ToString() + "," + eulerAngles.ToString());
		}
	}

	List<transformRecord> db = new List<transformRecord>();

	// Use this for initialization
	void Start () {
		
	}

	protected string Filename
	{
		get
		{
			return Directory + string.Format(NameFormat,LogNumber);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transformRecord r;
		r.timestamp = Time.time;
		r.orientation = transform.rotation;
		r.position = transform.position;
		r.lookat = transform.forward;
		r.eulerAngles = transform.rotation.eulerAngles;
		db.Add(r);

		if(Input.GetKeyDown(KeyCode.S))
		{
			while(File.Exists(Filename))
			{
				LogNumber++;
			}

			//write log
			var fo = File.CreateText(Filename);
			foreach(var record in db)
			{
				fo.WriteLine(record.ToString());
			}

			fo.Close();

			db = new List<transformRecord>();
		}

	}


}
