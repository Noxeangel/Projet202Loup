using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("LevelData")]
public class LevelData  {

    
    private int levelNumber;
    
    private string levelName;

    private Vector3 character1BeginPos;
    private Vector3 character2BeginPos;
    private Vector3 endLevelPosition;
    private string audioPath;
    private List<BGElementData> elements;
    private List<BlockElementData> lane1Blocks;
    private List<CollectibleData> lane1Collectibles;
    private List<LDOElementData> lane1LDOData;
    private List<BlockElementData> lane2Blocks;
    private List<CollectibleData> lane2Collectibles;
    private List<LDOElementData> lane2LDOData;

    
    public LevelData()
    {
        character1BeginPos = character2BeginPos = endLevelPosition = Vector3.zero;
        levelName = audioPath = "TEST";
        levelNumber = -1;
        elements = new List<BGElementData>();
        lane1Blocks = lane2Blocks = new List<BlockElementData>();
        lane1Collectibles = lane2Collectibles = new List<CollectibleData>();
        lane1LDOData = lane2LDOData = new List<LDOElementData>();
    }

    public bool WriteData(string path)
    {
#if UNITY_EDITOR
        XmlSerializer serializer = new XmlSerializer(typeof(LevelData));

        TextWriter textWriter = new StreamWriter(@path);

        serializer.Serialize(textWriter, this);

        UnityEditor.AssetDatabase.Refresh();
#endif
        return true;
    }


    #region Getters/Setters

    [XmlElement("LEVEL_NUMBER")]
    public int LevelNumber
    {
        get { return levelNumber; }
        set { levelNumber = value; }
    }

    [XmlElement("LEVEL_NAME")]
    public string LevelName
    {
        get { return levelName; }
        set { levelName = value; }
    }
    public Vector3 Character1BeginPos
    {
        get { return character1BeginPos; }
        set { character1BeginPos = value; }
    }
    public Vector3 Character2BeginPos
    {
        get { return character2BeginPos; }
        set { character2BeginPos = value; }
    }
    public Vector3 EndLevelPosition
    {
        get { return endLevelPosition; }
        set { endLevelPosition = value; }
    }
    public string AudioPath
    {
        get { return audioPath; }
        set { audioPath = value; }
    }
    [XmlArray()]
    public List<BGElementData> Elements
    {
        get { return elements; }
        set { elements = value; }
    }
    [XmlArray()]
    public List<BlockElementData> Lane1Blocks
    {
        get { return lane1Blocks; }
        set { lane1Blocks = value; }
    }
    [XmlArray()]
    public List<CollectibleData> Lane1Collectibles
    {
        get { return lane1Collectibles; }
        set { lane1Collectibles = value; }
    }
    [XmlArray()]
    public List<LDOElementData> Lane1LDOData
    {
        get { return lane1LDOData; }
        set { lane1LDOData = value; }
    }
    [XmlArray()]
    public List<BlockElementData> Lane2Blocks
    {
        get { return lane2Blocks; }
        set { lane2Blocks = value; }
    }
    [XmlArray()]
    public List<CollectibleData> Lane2Collectibles
    {
        get { return lane2Collectibles; }
        set { lane2Collectibles = value; }
    }
    [XmlArray()]
    public List<LDOElementData> Lane2LDOData
    {
        get { return lane2LDOData; }
        set { lane2LDOData = value; }
    }

    #endregion
}

[XmlRoot("BGElementData")]
public class BGElementData
{
    private Vector3 position;
    private Vector3 rotation;
    private string texturePath;

    public BGElementData(Vector3 _position, Vector3 _rotation, string _texturePath)
    {
        position = _position;
        rotation = _rotation;
        texturePath = _texturePath;
    }

    public BGElementData()
    {

    }


    #region Getters/Setters

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    public Vector3 Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public string TexturePath
    {
        get { return texturePath; }
        set { texturePath = value; }
    }

    #endregion
}

[XmlRoot("BlockElementData")]
public class BlockElementData
{
    private Vector3 position;
    private string texturePath;

    public BlockElementData(Vector3 _position, string _texturePath)
    {
        position = _position;
        texturePath = _texturePath;
    }

    public BlockElementData()
    {

    }


    #region Getters/Setters
    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }


    public string TexturePath
    {
        get { return texturePath; }
        set { texturePath = value; }
    }
    #endregion
}

[XmlRoot("CollectibleData")]
public class CollectibleData
{
    private Vector3 position;
    private int type;
    private int ammount;
    
    public CollectibleData(Vector3 _position, int _type, int _ammount)
    {
        position = _position;
        type = _type;
        ammount = _ammount;
    }

    public CollectibleData()
    {

    }


    #region Getters/Setters
    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    public int Type
    {
        get { return type; }
        set { type = value; }
    }
    public int Ammount
    {
        get { return ammount; }
        set { ammount = value; }
    }
    #endregion
}

[XmlRoot("LDOElementData")]
public class LDOElementData
{
    private Vector3 position;
    private Vector3 beginMarkerPosition;
    private Vector3 endMarkerPosition;

    private int actionType;
    private int scoreMax;
    private int scoreMin;
    private int lifeLoss;

    private Vector2 beginTangent;
    private Vector2 endTangent;

    private float firstZone1;
    private float firstZone2;
    private float firstZone3;
    private float secondZone3;
    private float secondZone2;
    private float secondZone1;
    private float markerHeight;

    private Vector3 damageZonePosition;
    private Vector2 damageZoneColliderOffset;
    private Vector2 damageZoneColliderSize;
    private Vector3 ldoSpritePosition;
    private string ldoSpriteTexturePath;
    
    public LDOElementData()
    {

    }

    #region Getters/Setters


    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    public Vector3 BeginMarkerPosition
    {
        get { return beginMarkerPosition; }
        set { beginMarkerPosition = value; }
    }
    public Vector3 EndMarkerPosition
    {
        get { return endMarkerPosition; }
        set { endMarkerPosition = value; }
    }
    public int ActionType
    {
        get { return actionType; }
        set { actionType = value; }
    }
    public int ScoreMax
    {
        get { return scoreMax; }
        set { scoreMax = value; }
    }
    public int ScoreMin
    {
        get { return scoreMin; }
        set { scoreMin = value; }
    }
    public int LifeLoss
    {
        get { return lifeLoss; }
        set { lifeLoss = value; }
    }
    public Vector2 BeginTangent
    {
        get { return beginTangent; }
        set { beginTangent = value; }
    }
    public Vector2 EndTangent
    {
        get { return endTangent; }
        set { endTangent = value; }
    }
    public float FirstZone1
    {
        get { return firstZone1; }
        set { firstZone1 = value; }
    }
    public float FirstZone2
    {
        get { return firstZone2; }
        set { firstZone2 = value; }
    }
    public float FirstZone3
    {
        get { return firstZone3; }
        set { firstZone3 = value; }
    }
    public float SecondZone3
    {
        get { return secondZone3; }
        set { secondZone3 = value; }
    }
    public float SecondZone2
    {
        get { return secondZone2; }
        set { secondZone2 = value; }
    }
    public float SecondZone1
    {
        get { return secondZone1; }
        set { secondZone1 = value; }
    }
    public float MarkerHeight
    {
        get { return markerHeight; }
        set { markerHeight = value; }
    }
    public Vector3 DamageZonePosition
    {
        get { return damageZonePosition; }
        set { damageZonePosition = value; }
    }
    public Vector2 DamageZoneColliderOffset
    {
        get { return damageZoneColliderOffset; }
        set { damageZoneColliderOffset = value; }
    }
    public Vector2 DamageZoneColliderSize
    {
        get { return damageZoneColliderSize; }
        set { damageZoneColliderSize = value; }
    }

    public Vector3 LdoSpritePosition
    {
        get { return ldoSpritePosition; }
        set { ldoSpritePosition = value; }
    }
    public string LdoSpriteTexturePath
    {
        get { return ldoSpriteTexturePath; }
        set { ldoSpriteTexturePath = value; }
    }

    #endregion
}
