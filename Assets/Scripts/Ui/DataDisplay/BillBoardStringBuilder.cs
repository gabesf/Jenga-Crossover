using API;

namespace Ui.DataDisplay
{
    public static class BillBoardStringBuilder
    {
        public static string GetTextFromPieceData(JengaPieceData jengaPieceData)
        {
            return $"{jengaPieceData.grade}: {jengaPieceData.domain}" +
                   $"\n\n" +
                   $"{jengaPieceData.cluster}" +
                   $"\n\n" +
                   $"{jengaPieceData.standardid}: {jengaPieceData.standarddescription}";
        }
    }
}