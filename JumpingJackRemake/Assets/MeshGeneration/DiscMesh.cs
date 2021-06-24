using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscMesh : MonoBehaviour
{
	[SerializeField] [Range(0.0F, 1.0F)] private float _innerRadiusPercent = 0.5F;
	[SerializeField] [Range(0.0F, 2.0F * Mathf.PI)] private float _arcLength = 2.0F * Mathf.PI;
	[SerializeField] [Range(0.0F, 2.0F * Mathf.PI)] private float _startingRadians = 0.0F;
	[SerializeField] [Range(4, 100)] private int _segments = 8;

	private readonly List<Vector3> _outterVertices = new List<Vector3>();
	private readonly List<Vector3> _innerVertices = new List<Vector3>();
	private Vector3[] _vertices;
	private MeshFilter _meshFilter;

    private void Start()
    {
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
        _meshFilter = gameObject.AddComponent<MeshFilter>();
    }

	private void Update()
	{
		Redraw();
	}

	private void Redraw()
	{
		_meshFilter.mesh = new Mesh()
		{
			vertices = GenerateVertices(),
			triangles = GenerateTriangles(),
		};
	}

	private Vector3[] GenerateVertices()
	{
		int newVerticesLength = 2 * _segments + 2;

		if(_vertices == null || _vertices.Length != newVerticesLength)
		{
			_vertices = new Vector3[newVerticesLength];
		}

		_innerVertices.Clear();
		_outterVertices.Clear();
		float arcRadians = _arcLength / _segments;

		for(int i = 0; i <= _segments; i++)
		{
			float radians = _startingRadians + i * arcRadians;
			Vector3 outterVertex = new Vector3(Mathf.Cos(radians), 0.0F, Mathf.Sin(radians));
			Vector3 innerVertex = new Vector3(_innerRadiusPercent * Mathf.Cos(radians), 0.0F, _innerRadiusPercent * Mathf.Sin(radians));
			_outterVertices.Add(outterVertex);
			_innerVertices.Add(innerVertex);
			_vertices[2 * i] = outterVertex;
			_vertices[2 * i + 1] = innerVertex;
		}

		return _vertices;
	}

	private int[] GenerateTriangles()
	{
		int[] result = new int[3 * (_innerVertices.Count - 1 + _outterVertices.Count - 1)];

		for(int i = 0; i + 2 < result.Length; i += 3)
		{
			int value1 = i / 3 + 0;
			int value2 = i / 3 + 1;
			int value3 = i / 3 + 2;

			if(i % 2 == 0)
			{
				result[i] = value1;
				result[i + 1] = value2;
				result[i + 2] = value3;
			}
			else
			{
				result[i] = value3;
				result[i + 1] = value2;
				result[i + 2] = value1;
			}
		}

		return result;
	}
}
