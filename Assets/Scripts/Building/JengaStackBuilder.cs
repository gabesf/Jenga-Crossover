using System;
using System.Collections;
using System.Collections.Generic;
using API;
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

        private int maxStackCount = 3;

        // Build a stack of Jenga pieces at a given position
        public Transform BuildStackAt(JengaStackData jengaStackData, Vector3 buildLocalPosition)
        {
            Transform stackTransform = CreateStack(jengaStackData, buildLocalPosition);
            CreateJengaPieces(jengaStackData, stackTransform);
            CreateStackNamePlate(jengaStackData, stackTransform);

            return stackTransform;
        }

        // Create a new stack object
        private Transform CreateStack(JengaStackData jengaStackData, Vector3 buildLocalPosition)
        {
            GameObject stack = new GameObject($"{jengaStackData.Name} Stack");
            Transform stackTransform = stack.transform;

            stackTransform.parent = tableTop;
            stackTransform.localPosition = buildLocalPosition;

            return stackTransform;
        }

        // Create the nameplate for the stack
        private void CreateStackNamePlate(JengaStackData jengaStackData, Transform stackTransform)
        {
            StackNameplateInstantiationData stackNameplateInstantiationData = new StackNameplateInstantiationData()
            {
                Name = jengaStackData.Name,
                LocalPosition = nameplateLocalPosition,
                LocalRotation = nameplateLocalRotation
            };

            StackNamePlateBuilder.CreateNameplate(stackNameplateInstantiationData, stackNameplatePrefab,
                stackTransform);
        }

        // Create Jenga pieces for the stack
        private void CreateJengaPieces(JengaStackData jengaStackData, Transform stackTransform)
        {
            for (int i = 0; i < jengaStackData.PiecesData.Count; i++)
            {
                CreateJengaPiece(jengaStackData, stackTransform, i);
            }
        }

        // Create a single Jenga piece
        private void CreateJengaPiece(JengaStackData jengaStackData, Transform stackTransform, int i)
        {
            var jengaPieceData = jengaStackData.PiecesData[i];

            PieceInstantiationData pieceInstantiationData = new PieceInstantiationData()
            {
                Position = GetPositionFromIndex(i),
                Rotation = GetRotationFromIndex(i),
                Mastery = jengaPieceData.mastery,
                JengaPieceData = jengaPieceData,
                Index = i
            };

            JengaPieceBuilder.CreateJengaPiece(pieceInstantiationData, jengaPiecePrefab, stackTransform);
        }

        // Get the rotation for the Jenga piece based on its index
        private Quaternion GetRotationFromIndex(int pieceIndex)
        {
            return IsPieceFlipped(pieceIndex) ? Quaternion.Euler(new Vector3(0f, 90f, 0f)) : Quaternion.identity;
        }

        // Determine if the Jenga piece is flipped based on its index
        private static bool IsPieceFlipped(int pieceIndex)
        {
            return pieceIndex % 6 < 3;
        }

        // Get the position for the Jenga piece based on its index
        private Vector3 GetPositionFromIndex(int pieceIndex)
        {
            int heightLevel = Mathf.FloorToInt(pieceIndex / 3.0f);
            var height = (heightLevel * (pieceHeight + 0.001f) + pieceHeight / 2f);
            var lateralLevel = pieceIndex % 3;
            float lateralOffset;
            if (IsPieceFlipped(pieceIndex))
            {
                lateralOffset = lateralLevel * 2f * pieceWidth - pieceWidth * 2f;
                return new Vector3(lateralOffset, height, 0f);
            }

            lateralOffset = lateralLevel * 2f * pieceWidth - pieceWidth * 2f;
            return new Vector3(0f, height, lateralOffset);
        }

        // Build multiple Jenga stacks
        public List<Transform> BuildStacks(Dictionary<string, JengaStackData> jengaStacksData)
        {
            Vector3 buildLocalPosition = new Vector3(-xPositionIncrement, 0f, 0f);
            var stackCount = 0;

            List<Transform> stacks = new List<Transform>();
            foreach (var key in jengaStacksData.Keys)
            {
                if (++stackCount > maxStackCount) break;

                stacks.Add(BuildStackAt(jengaStacksData[key], buildLocalPosition));
                buildLocalPosition += new Vector3(xPositionIncrement, 0f, 0f);
            }

            return stacks;
        }
    }
}