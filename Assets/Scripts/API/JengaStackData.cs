using System.Collections.Generic;
using System.Linq;

namespace API
{
    // JengaStackData represents a stack of Jenga pieces for a specific grade
    public class JengaStackData
    {
        public List<JengaPieceData> PiecesData;
        public string Name;
        
        // Constructor initializes the stack with a given name and an empty list of JengaPieceData
        public JengaStackData(string name)
        {
            Name = name;
            PiecesData = new List<JengaPieceData>();
        }
        
        // SortToSpecification sorts the Jenga pieces in the stack based on the given criteria:
        // 1. By domain name (ascending)
        // 2. By cluster name (ascending)
        // 3. By standard ID (ascending)
        public void SortToSpecification()
        {
            PiecesData = PiecesData.OrderBy(piece => piece.domain)
                .ThenBy(piece => piece.cluster)
                .ThenBy(piece => piece.standardid)
                .ToList();
        }
    }
}