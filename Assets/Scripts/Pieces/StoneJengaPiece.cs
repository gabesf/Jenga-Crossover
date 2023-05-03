using UnityEngine;

public class StoneJengaPiece : JengaPiece
{
    protected override string MaterialName => "Stone";
    protected override string PieceLabel => "Mastered";
    protected override Color LabelColor => new Color(0.33f, 0.33f, 0.33f);
}