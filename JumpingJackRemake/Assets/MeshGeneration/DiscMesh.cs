using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class DiscMesh : MonoBehaviour
{
	[SerializeField] [Range(0.0F, 1.0F)] private float _innerRadiusPercent = 0.5F;
	[SerializeField] [Range(0.0F, 2.0F * Mathf.PI)] private float _arcLength = 2.0F * Mathf.PI;
	[SerializeField] [Range(0.0F, 2.0F * Mathf.PI)] private float _startingRadians = 0.0F;
	[SerializeField] [Range(0.0F, 1.0F)] private float _heightPercent = 1.0F;
	[SerializeField] [Range(4, 100)] private int _segments = 8;

	private readonly List<Vector3> _upperOutterVertices = new List<Vector3>();
	private readonly List<Vector3> _upperInnerVertices = new List<Vector3>();
	private readonly List<Vector3> _lowerOutterVertices = new List<Vector3>();
	private readonly List<Vector3> _lowerInnerVertices = new List<Vector3>();
	private Vector3[] _vertices;
	private int[] _triangles;
	private MeshFilter _meshFilter;

    private void Start()
    {
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
			triangles = GenerateTriangles()
		};

		_meshFilter.mesh.RecalculateNormals();
	}

	private Vector3[] GenerateVertices()
	{
		int newVerticesLength = 8 * (_segments + 1);

		if(_vertices == null || _vertices.Length != newVerticesLength)
		{
			_vertices = new Vector3[newVerticesLength];
		}

		_upperInnerVertices.Clear();
		_upperOutterVertices.Clear();
		_lowerInnerVertices.Clear();
		_lowerOutterVertices.Clear();
		float arcRadians = _arcLength / _segments;

		for(int i = 0; i <= _segments; i++)
		{
			float radians = _startingRadians + i * arcRadians;
			float cosRadians = Mathf.Cos(radians);
			float sinRadians = Mathf.Sin(radians);
			Vector3 upperOutterVertex = new Vector3(cosRadians, 0.0F, sinRadians);
			Vector3 upperInnerVertex = new Vector3(_innerRadiusPercent * cosRadians, 0.0F, _innerRadiusPercent * sinRadians);
			Vector3 lowerOutterVertex = new Vector3(cosRadians, -_heightPercent, sinRadians);
			Vector3 lowerInnerVertex = new Vector3(_innerRadiusPercent * cosRadians, -_heightPercent, _innerRadiusPercent * sinRadians);
			_upperOutterVertices.Add(upperOutterVertex);
			_upperInnerVertices.Add(upperInnerVertex);
			_lowerOutterVertices.Add(lowerOutterVertex);
			_lowerInnerVertices.Add(lowerInnerVertex);
			_vertices[8 * i + 0] = upperOutterVertex;
			_vertices[8 * i + 1] = upperOutterVertex;
			_vertices[8 * i + 2] = upperInnerVertex;
			_vertices[8 * i + 3] = upperInnerVertex;
			_vertices[8 * i + 4] = lowerOutterVertex;
			_vertices[8 * i + 5] = lowerOutterVertex;
			_vertices[8 * i + 6] = lowerInnerVertex;
			_vertices[8 * i + 7] = lowerInnerVertex;
		}

		return _vertices;
	}

	private int[] GenerateTriangles()
	{
		int newTrianglesLength = 3 * _vertices.Length;

		if(_triangles == null || _triangles.Length != newTrianglesLength)
		{
			_triangles = new int[newTrianglesLength];
		}

		for(int i = 0; i + 23 < _triangles.Length - 12; i += 24)
		{
			int offset = i / 3;
			_triangles[i +  0] = offset + 0;
			_triangles[i +  1] = offset + 2;
			_triangles[i +  2] = offset + 8;

			_triangles[i +  3] = offset + 6;
			_triangles[i +  4] = offset + 12;
			_triangles[i +  5] = offset + 14;

			_triangles[i +  6] = offset + 1;
			_triangles[i +  7] = offset + 9;
			_triangles[i +  8] = offset + 13;

			_triangles[i +  9] = offset + 3;
			_triangles[i + 10] = offset + 7;
			_triangles[i + 11] = offset + 15;

			_triangles[i + 12] = offset + 10;
			_triangles[i + 13] = offset + 8;
			_triangles[i + 14] = offset + 2;

			_triangles[i + 15] = offset + 12;
			_triangles[i + 16] = offset + 6;
			_triangles[i + 17] = offset + 4;

			_triangles[i + 18] = offset + 13;
			_triangles[i + 19] = offset + 5;
			_triangles[i + 20] = offset + 1;

			_triangles[i + 21] = offset + 15;
			_triangles[i + 22] = offset + 11;
			_triangles[i + 23] = offset + 3;
		}

		_triangles[_triangles.Length - 12] = 4;
		_triangles[_triangles.Length - 11] = 6;
		_triangles[_triangles.Length - 10] = 0;

		_triangles[_triangles.Length -  9] = 2;
		_triangles[_triangles.Length -  8] = 0;
		_triangles[_triangles.Length -  7] = 6;

		_triangles[_triangles.Length - 6] = _vertices.Length - 8 + 0;
		_triangles[_triangles.Length - 5] = _vertices.Length - 8 + 6;
		_triangles[_triangles.Length - 4] = _vertices.Length - 8 + 4;

		_triangles[_triangles.Length - 3] = _vertices.Length - 8 + 6;
		_triangles[_triangles.Length - 2] = _vertices.Length - 8 + 0;
		_triangles[_triangles.Length - 1] = _vertices.Length - 8 + 2;

		return _triangles;
	}
}
