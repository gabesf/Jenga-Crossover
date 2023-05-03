using UnityEngine;

namespace Building
{
    public static class JengaPieceBuilder
    {
        public static void CreateJengaPiece(PieceInstantiationData pieceInstantiationData, GameObject jengaPiecePrefab,
            Transform stackTransform)
        {
            Transform jengaPiece = Object.Instantiate(jengaPiecePrefab, stackTransform, false).transform;
            jengaPiece.localPosition = pieceInstantiationData.Position;
            jengaPiece.localRotation = pieceInstantiationData.Rotation;
            jengaPiece.name = $"Piece {pieceInstantiationData.Index}";

            AddJengaBehavior(pieceInstantiationData.Mastery, jengaPiece.gameObject);
        }
        
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