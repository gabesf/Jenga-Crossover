using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Building
{
    public class JengaLevelBuilder
    {
        public void BuildLevel()
        {
        }
    }

    public class PieceInstantiationData
    {
        public GameObject PrefabToInstantiate;
        public Vector3 Position;
        public Quaternion Rotation;
        public int Index;
    }

    public class JengaPieceBuilder
    {
        public static void CreateJengaPiece(PieceInstantiationData pieceInstantiationData, Transform stackTransform)
        {
            var jengaPiece = Object.Instantiate(pieceInstantiationData.PrefabToInstantiate, stackTransform, false)
                .transform;
            jengaPiece.localPosition = pieceInstantiationData.Position;
            jengaPiece.localRotation = pieceInstantiationData.Rotation;
            jengaPiece.name = $"Piece {pieceInstantiationData.Index}";
        }
    }

    public class JengaStackBuilder : MonoBehaviour
    {
        public GameObject jengaPiecePrefab;
        public GameObject stonePiece;
        public GameObject woodPiecePrefab;
        public GameObject glassPiece;

        public Transform tableTop;

        public float xPositionIncrement = 0.25f;

        public float pieceWidth = 0.15f;
        public float pieceLength = 0.05f;
        public float pieceHeight = 0.02f;

        public void BuildStackAt(JengaStackData jengaStackData, Vector3 buildLocalPosition)
        {
            GameObject stack = new GameObject($"{jengaStackData.Name} Stack");
            Transform stackTransform = stack.transform;

            stackTransform.parent = tableTop;
            stackTransform.localPosition = buildLocalPosition;

            //StartCoroutine(CreateJengaPiecesCoroutine(jengaStackData, stackTransform));
            CreateJengaPieces(jengaStackData, stackTransform);


        }

        private void CreateJengaPieces(JengaStackData jengaStackData, Transform stackTransform)
        {
            for (int i = 0; i < jengaStackData.PiecesData.Count; i++)
            {
                CreateJengaPiece(jengaStackData, stackTransform, i);
            }
        }

        private IEnumerator CreateJengaPiecesCoroutine(JengaStackData jengaStackData, Transform stackTransform)
        {
            for (int i = 0; i < jengaStackData.PiecesData.Count; i++)
            {
                CreateJengaPiece(jengaStackData, stackTransform, i);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void CreateJengaPiece(JengaStackData jengaStackData, Transform stackTransform, int i)
        {
            var jengaPieceData = jengaStackData.PiecesData[i];

            PieceInstantiationData pieceInstantiationData = new PieceInstantiationData()
            {
                Position = GetPositionFromIndex(i),
                Rotation = GetRotationFromIndex(i),
                PrefabToInstantiate = GetPrefabFromMastery(jengaPieceData.mastery),
                Index = i
            };

            JengaPieceBuilder.CreateJengaPiece(pieceInstantiationData, stackTransform);
        }


        private Quaternion GetRotationFromIndex(int pieceIndex)
        {
            
            return IsPieceFlipped(pieceIndex) ? Quaternion.Euler(new Vector3(0f, 90f, 0f)) : Quaternion.identity ;
        }

        private static bool IsPieceFlipped(int pieceIndex)
        {
            return pieceIndex % 6 < 3;
        }

        private Vector3 GetPositionFromIndex(int pieceIndex)
        {
            
            
            int heightLevel = Mathf.FloorToInt(pieceIndex / 3.0f);
            var height = (heightLevel * (pieceHeight + 0.001f) + pieceHeight / 2f) ;
            //var height = (heightLevel *  pieceHeight+ pieceHeight / 2f) ;

            var lateralLevel = pieceIndex % 3;            float lateralOffset;
            if (IsPieceFlipped(pieceIndex))
            {
                //Debug.Break();
                lateralOffset = lateralLevel *  2f * pieceWidth - pieceWidth* 2f;
                return new Vector3(lateralOffset, height, 0f);
            }
            else
            {
                
                lateralOffset = lateralLevel *  2f * pieceWidth - pieceWidth* 2f;
                return new Vector3(0f, height, lateralOffset);
            }
            
            
            Debug.Log($"Piece {pieceIndex} Height {height} X {pieceIndex % 3}");
            
        }

        private GameObject GetPrefabFromMastery(int mastery)
        {
            return mastery switch
            {
                0 => glassPiece,
                1 => woodPiecePrefab,
                2 => stonePiece,
                _ => throw new UnityException($"Unexpected mastery value: {mastery}")
            };
        }

        public void BuildStacks(Dictionary<string, JengaStackData> jengaStacksData)
        {
            Vector3 buildLocalPosition = new Vector3(-xPositionIncrement, 0f, 0f);
            foreach (var key in jengaStacksData.Keys)
            {
                BuildStackAt(jengaStacksData[key], buildLocalPosition);
                buildLocalPosition += new Vector3(xPositionIncrement, 0f, 0f);
            }
        }
    }
}