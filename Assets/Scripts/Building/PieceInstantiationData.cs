using UnityEngine;

namespace Building
{
    public class PieceInstantiationData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public int Mastery;
        public int Index;
        public JengaPieceData JengaPieceData;
    }

    public class StackNameplateInstantiationData
    {
        public string Name;
        public Vector3 LocalPosition;
        public Vector3 LocalRotation;
        
    }
}