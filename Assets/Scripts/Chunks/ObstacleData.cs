using System;
using UnityEngine;

public enum ObstacleId
{
    Calibr, Drone, Helicopter, Rocket, Soldier, SoldierJetpack, PotatoKing, Pukin,
    Energy
}

/// <summary>
///Struct 3 that holds only x, y and z coordinate values.
/// </summary>
[Serializable]
public struct SimplifiedVector3 : IComparable<SimplifiedVector3>, IEquatable<SimplifiedVector3>
{
    [SerializeField]
    private float _x;
    [SerializeField]
    private float _y;
    [SerializeField]
    private float _z;
    
    public float X
    {
        get => _x;
        set => _x = value;
    }

    public float Y
    {
        get => _y;
        set => _y = value;
    }

    public float Z
    {
        get => _z;
        set => _z = value;
    }

    public SimplifiedVector3(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
    
    public static SimplifiedVector3 Default => new SimplifiedVector3(0, 0, 0);

    public int CompareTo(SimplifiedVector3 other)
    {
        if (other == null) return 1;

        return CompareTo(other);
    }

    public bool Equals(SimplifiedVector3 other)
    {
        if (other == null) return false;

        return X == other.X && Y == other.Y && Z == other.Z;
    }
    
    public static bool operator ==(SimplifiedVector3 a, SimplifiedVector3 b)
    {
        return a.Equals(b);
    }

    // Override the default inequality operator
    public static bool operator !=(SimplifiedVector3 a, SimplifiedVector3 b)
    {
        return !(a == b);
    }

    public SimplifiedVector3(Vector3 otherVector)
    {
        _x = otherVector.x;
        _y = otherVector.y;
        _z = otherVector.z;
    }
    
    public Vector3 ConvertToVector3() => new(X, Y, Z);

}
/// <summary>
///Struct 3 that holds only x, y, z and w coordinate values.
/// </summary>
[Serializable]
public struct SimplifiedQuaternion : IComparable<SimplifiedQuaternion>, IEquatable<SimplifiedQuaternion>
{
    [SerializeField]
    private float _x;
    [SerializeField]
    private float _y;
    [SerializeField]
    private float _z;
    [SerializeField]
    private float _w;
    
    public float X
    {
        get => _x;
        set => _x = value;
    }

    public float Y
    {
        get => _y;
        set => _y = value;
    }

    public float Z
    {
        get => _z;
        set => _z = value;
    }
    public float W
    {
        get => _w;
        set => _w = value;
    }

    public SimplifiedQuaternion(float x, float y, float z, float w)
    {
        _x = x;
        _y = y;
        _z = z;
        _w = w;
    }
    
    public static SimplifiedQuaternion Default => new SimplifiedQuaternion(0, 0, 0, 0);

    public int CompareTo(SimplifiedQuaternion other)
    {
        if (other == null) return 1;

        return CompareTo(other);
    }

    public bool Equals(SimplifiedQuaternion other)
    {
        if (other == null) return false;

        return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
    }
    
    public static bool operator ==(SimplifiedQuaternion a, SimplifiedQuaternion b)
    {
        return a.Equals(b);
    }

    // Override the default inequality operator
    public static bool operator !=(SimplifiedQuaternion a, SimplifiedQuaternion b)
    {
        return !(a == b);
    }

    public  SimplifiedQuaternion(Quaternion otherQuaternion)
    {
        _x = otherQuaternion.x;
        _y = otherQuaternion.y;
        _z = otherQuaternion.z;
        _w = otherQuaternion.w;
    }
    
    public Quaternion ConvertToQuaternion() => new(X, Y, Z, W);

}

namespace Chunks
{
    [Serializable]
    public struct ObstacleData
    {
        
        [SerializeField, Header("[Have to choose the obstacle type ID to correct data saving!]")] private ObstacleId _obstacleId;

        [Header("[Readonly debug information below!]")]

        [SerializeField]
        private SimplifiedVector3 _localPosition;

        [SerializeField]
        private SimplifiedQuaternion _localRotation;

        [SerializeField]
        private SimplifiedVector3 _scale;

        public ObstacleData(SimplifiedVector3 scale, SimplifiedQuaternion localRotation, SimplifiedVector3 localPosition, ObstacleId obstacleId) : this()
        {
            _scale = scale;
            _localRotation = localRotation;
            _localPosition = localPosition;
            _obstacleId = obstacleId;
        }

        public SimplifiedVector3 Scale { get => _scale; set => _scale = value; }
        public SimplifiedQuaternion LocalRotation { get => _localRotation; set => _localRotation = value; }
        public SimplifiedVector3 LocalPosition { get => _localPosition; set => _localPosition = value; }
        public ObstacleId ObstacleId { get => _obstacleId; set => _obstacleId = value; }
    }
}
