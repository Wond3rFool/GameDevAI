using System.Collections.Generic;
using UnityEngine;


public class Blackboard : MonoBehaviour
{
	private Dictionary<string, object> data;

	public Blackboard()
	{
		data = new Dictionary<string, object>();
	}

	public bool HasData(string key)
	{
		if (data.ContainsKey(key))
		{
			return true;
		}
		return false;
	}

	public T GetData<T>(string key)
	{
		if (data.TryGetValue(key, out object value))
		{
			return (T)value;
		}
		return default;
	}

	public void SetData<T>(string key, T value)
	{
		if (!data.ContainsKey(key))
		{
			data.Add(key, value);
		}
		else
		{
			data[key] = value;
		}
	}

	public void RemoveData(string key)
	{
		data.Remove(key);
	}
}

