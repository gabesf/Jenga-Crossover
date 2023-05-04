using UnityEngine;

namespace Building
{
    public static class JengaPieceBuilder
    {
        // Create a Jenga piece based on the instantiation data
        public static void CreateJengaPiece(PieceInstantiationData pieceInstantiationData, GameObject jengaPiecePrefab,
            Transform stackTransform)
        {
            Transform jengaPiece = InstantiateJengaPiece(jengaPiecePrefab, stackTransform);
            SetJengaPieceProperties(pieceInstantiationData, jengaPiece);
            AddJengaBehavior(pieceInstantiationData.Mastery, jengaPiece.gameObject);
            jengaPiece.GetComponent<JengaPiece>().JengaPieceData = pieceInstantiationData.JengaPieceData;
        }

        // Instantiate the Jenga piece
        private static Transform InstantiateJengaPiece(GameObject jengaPiecePrefab, Transform stackTransform)
        {
            Transform jengaPiece = Object.Instantiate(jengaPiecePrefab, stackTransform, false).transform;
            return jengaPiece;
        }

        // Set the properties of the Jenga piece
        private static void SetJengaPieceProperties(PieceInstantiationData pieceInstantiationData, Transform jengaPiece)
        {
            jengaPiece.localPosition = pieceInstantiationData.Position;
            jengaPiece.localRotation = pieceInstantiationData.Rotation;
            jengaPiece.name = $"Piece {pieceInstantiationData.Index}";
        }

        // Add Jenga behavior based on the mastery level
        private static void AddJengaBehavior(int mastery, GameObject jengaPiece)
        {
            switch (mastery)
            {
                case 0:
                    jengaPiece.AddComponent<GlassJengaPiece>();
                    break;

                case 1:
                    jengaPiece.AddComponent<WoodJengaPiece>();
                    break;

                case 2:
                    jengaPiece.AddComponent<StoneJengaPiece>();
                    break;
            }
        }
    }
}