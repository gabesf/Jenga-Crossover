using System;
using System.Collections.Generic;

[Serializable]
public class JengaPieceData
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
}
    
[Serializable]
public class JengaPieceDataList
{
    public JengaPieceData[] data;
}

public class JengaStackData
{
    public JengaStackData(string name)
    {
        Name = name;
        PiecesData = new List<JengaPieceData>();
    }

    public List<JengaPieceData> PiecesData;
    public string Name;
}