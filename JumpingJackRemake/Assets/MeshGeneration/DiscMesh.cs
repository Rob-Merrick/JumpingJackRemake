using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class DiscMesh : MonoBehaviour
{
	[SerializeField] [Range(0.0F, 1.0F)] private float _innerRadiusPercent = 0.5F;
	[SerializeField] [Range(0.0F, 2.0F * Mathf.PI + 0.1F)] private float _arcLength = 2.0F * Mathf.PI;
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
	private MeshCollider _meshCollider;

	private float _actualArcLength;

	public float ArcLength
	{
		get => _actualArcLength;
		set
		{
			_actualArcLength = value;
			_arcLength = Mathf.Repeat(value, 2.0F * Mathf.PI);
		}
	}

	public float StartingRadians { get => _startingRadians; set => _startingRadians = Mathf.Repeat(value, 2.0F * Mathf.PI); }

	private void Update()
	{
		Redraw();
	}

	private void Redraw()
	{
		if(_meshFilter == null)
		{
			_meshFilter = gameObject.GetComponent<MeshFilter>();
		}

		if(_meshCollider == null)
		{
			_meshCollider = gameObject.GetComponent<MeshCollider>();
		}

		if(Application.isPlaying)
		{
			_meshFilter.mesh = new Mesh()
			{
				vertices = GenerateVertices(),
				triangles = GenerateTriangles()
			};

			_meshFilter.mesh.RecalculateNormals();
			_meshFilter.mesh.RecalculateBounds();
			_meshCollider.sharedMesh = _meshFilter.mesh;
		}
		else
		{
			_meshFilter.sharedMesh = new Mesh()
			{
				vertices = GenerateVertices(),
				triangles = GenerateTriangles()
			};

			_meshFilter.sharedMesh.RecalculateNormals();
			_meshFilter.sharedMesh.RecalculateBounds();
			_meshCollider.sharedMesh = _meshFilter.sharedMesh;
		}
	}

	private Vector3[] GenerateVertices()
	{
		int newVerticesLength = 8 * (_segments + 2);

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

			if(i == 0)
			{
				_vertices[0] = upperOutterVertex;
				_vertices[1] = upperInnerVertex;
				_vertices[2] = lowerOutterVertex;
				_vertices[3] = lowerInnerVertex;
			}

			_upperOutterVertices.Add(upperOutterVertex);
			_upperInnerVertices.Add(upperInnerVertex);
			_lowerOutterVertices.Add(lowerOutterVertex);
			_lowerInnerVertices.Add(lowerInnerVertex);
			_vertices[8 * i +  4] = upperOutterVertex;
			_vertices[8 * i +  5] = upperOutterVertex;
			_vertices[8 * i +  6] = upperInnerVertex;
			_vertices[8 * i +  7] = upperInnerVertex;
			_vertices[8 * i +  8] = lowerOutterVertex;
			_vertices[8 * i +  9] = lowerOutterVertex;
			_vertices[8 * i + 10] = lowerInnerVertex;
			_vertices[8 * i + 11] = lowerInnerVertex;

			if(i == _segments)
			{
				_vertices[_vertices.Length - 4] = upperOutterVertex;
				_vertices[_vertices.Length - 3] = upperInnerVertex;
				_vertices[_vertices.Length - 2] = lowerOutterVertex;
				_vertices[_vertices.Length - 1] = lowerInnerVertex;
			}
		}

		return _vertices;
	}

	private int[] GenerateTriangles()
	{
		int newTrianglesLength = 3 * (_vertices.Length - 12);

		if(_triangles == null || _triangles.Length != newTrianglesLength)
		{
			_triangles = new int[newTrianglesLength];
		}

		int triangleIndexOffset = 6;

		for(int i = 0; i + 12 + 23 < _triangles.Length; i += 24)
		{
			if(i == 0)
			{
				_triangles[0] = 1;
				_triangles[1] = 0;
				_triangles[2] = 3;

				_triangles[3] = 0;
				_triangles[4] = 2;
				_triangles[5] = 3;
			}

			int vertexIndexOffset = i / 3 + 4;
			_triangles[i + triangleIndexOffset +  0] = vertexIndexOffset + 0;
			_triangles[i + triangleIndexOffset +  1] = vertexIndexOffset + 2;
			_triangles[i + triangleIndexOffset +  2] = vertexIndexOffset + 8;

			_triangles[i + triangleIndexOffset +  3] = vertexIndexOffset + 6;
			_triangles[i + triangleIndexOffset +  4] = vertexIndexOffset + 12;
			_triangles[i + triangleIndexOffset +  5] = vertexIndexOffset + 14;

			_triangles[i + triangleIndexOffset +  6] = vertexIndexOffset + 1;
			_triangles[i + triangleIndexOffset +  7] = vertexIndexOffset + 9;
			_triangles[i + triangleIndexOffset +  8] = vertexIndexOffset + 13;

			_triangles[i + triangleIndexOffset +  9] = vertexIndexOffset + 3;
			_triangles[i + triangleIndexOffset + 10] = vertexIndexOffset + 7;
			_triangles[i + triangleIndexOffset + 11] = vertexIndexOffset + 15;

			_triangles[i + triangleIndexOffset + 12] = vertexIndexOffset + 10;
			_triangles[i + triangleIndexOffset + 13] = vertexIndexOffset + 8;
			_triangles[i + triangleIndexOffset + 14] = vertexIndexOffset + 2;

			_triangles[i + triangleIndexOffset + 15] = vertexIndexOffset + 12;
			_triangles[i + triangleIndexOffset + 16] = vertexIndexOffset + 6;
			_triangles[i + triangleIndexOffset + 17] = vertexIndexOffset + 4;

			_triangles[i + triangleIndexOffset + 18] = vertexIndexOffset + 13;
			_triangles[i + triangleIndexOffset + 19] = vertexIndexOffset + 5;
			_triangles[i + triangleIndexOffset + 20] = vertexIndexOffset + 1;

			_triangles[i + triangleIndexOffset + 21] = vertexIndexOffset + 15;
			_triangles[i + triangleIndexOffset + 22] = vertexIndexOffset + 11;
			_triangles[i + triangleIndexOffset + 23] = vertexIndexOffset + 3;

			if(i + 12 + 23 == _triangles.Length - 1)
			{
				_triangles[_triangles.Length - 6] = _vertices.Length - 4;
				_triangles[_triangles.Length - 5] = _vertices.Length - 1;
				_triangles[_triangles.Length - 4] = _vertices.Length - 2;

				_triangles[_triangles.Length - 3] = _vertices.Length - 4;
				_triangles[_triangles.Length - 2] = _vertices.Length - 3;
				_triangles[_triangles.Length - 1] = _vertices.Length - 1;
			}
		}

		return _triangles;
	}
}
