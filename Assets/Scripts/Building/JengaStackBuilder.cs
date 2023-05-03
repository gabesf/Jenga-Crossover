using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Building
{
    public class JengaStackBuilder : MonoBehaviour
    {
        public GameObject jengaPiecePrefab;
        public GameObject stackNameplatePrefab;

        public Transform tableTop;

        public float xPositionIncrement = 0.25f;

        public float pieceWidth = 0.15f;
        public float pieceLength = 0.05f;
        public float pieceHeight = 0.02f;

        public Vector3 nameplateLocalPosition;
        public Vector3 nameplateLocalRotation;

        public void BuildStackAt(JengaStackData jengaStackData, Vector3 buildLocalPosition)
        {
            GameObject stack = new GameObject($"{jengaStackData.Name} Stack");
            Transform stackTransform = stack.transform;

            stackTransform.parent = tableTop;
            stackTransform.localPosition = buildLocalPosition;

            //StartCoroutine(CreateJengaPiecesCoroutine(jengaStackData, stackTransform));
            CreateJengaPieces(jengaStackData, stackTransform);
            CreateStackNamePlate(jengaStackData, stackTransform);
        }

        private void CreateStackNamePlate(JengaStackData jengaStackData, Transform stackTransform)
        {
            StackNameplateInstantiationData stackStackNameplateInstantiationData = new StackNameplateInstantiationData()
            {
                Name = jengaStackData.Name,
                LocalPosition = nameplateLocalPosition,
                LocalRotation = nameplateLocalRotation
            };
            
            StackNamePlateBuilder.CreateNameplate(stackStackNameplateInstantiationData, stackNameplatePrefab, stackTransform);
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
                Mastery =  jengaPieceData.mastery,
                Index = i
            };

            JengaPieceBuilder.CreateJengaPiece(pieceInstantiationData, jengaPiecePrefab, stackTransform);
        }


        private Quaternion GetRotationFromIndex(int pieceIndex)
        {
            return IsPieceFlipped(pieceIndex) ? Quaternion.Euler(new Vector3(0f, 90f, 0f)) : Quaternion.identity;
        }

        private static bool IsPieceFlipped(int pieceIndex)
        {
            return pieceIndex % 6 < 3;
        }

        private Vector3 GetPositionFromIndex(int pieceIndex)
        {
            int heightLevel = Mathf.FloorToInt(pieceIndex / 3.0f);
            var height = (heightLevel * (pieceHeight + 0.001f) + pieceHeight / 2f);

            var lateralLevel = pieceIndex % 3;
            float lateralOffset;
            if (IsPieceFlipped(pieceIndex))
            {
                //Debug.Break();
                lateralOffset = lateralLevel * 2f * pieceWidth - pieceWidth * 2f;
                return new Vector3(lateralOffset, height, 0f);
            }

            lateralOffset = lateralLevel * 2f * pieceWidth - pieceWidth * 2f;
            return new Vector3(0f, height, lateralOffset);

        }

        private int maxStackCount = 3;

        public void BuildStacks(Dictionary<string, JengaStackData> jengaStacksData)
        {
            
            Vector3 buildLocalPosition = new Vector3(-xPositionIncrement, 0f, 0f);
            var stackCount = 0;
            foreach (var key in jengaStacksData.Keys)
            {
                if(++stackCount > maxStackCount) break;
                
                BuildStackAt(jengaStacksData[key], buildLocalPosition);
                buildLocalPosition += new Vector3(xPositionIncrement, 0f, 0f);
            }
        }
    }
}