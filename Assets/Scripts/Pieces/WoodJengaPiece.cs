using UnityEngine;

public class WoodJengaPiece : JengaPiece
{
    protected override string MaterialName => "Wood";
    protected override string PieceLabel => "Learned";
    protected override Color LabelColor => new Color(0.54f, 0.2f, 0f );
}