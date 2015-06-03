using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


public class TweakingManager : MonoBehaviour {

    public const int NULL_INT = -666666;
    public const float NULL_FLOAT = -666666.66f;
    public const string NULL_STRING = "NULL_STRING";

    StreamWriter writer;

    public TeamNames TweakerName;
    public DateTime currentDate;

    public TextAsset data;
    public List<TweakedVariable> variables;

	public void Init()
    {
        variables = new List<TweakedVariable>();
        #if UNITY_EDITOR
        WriteTweakingFile();
        #endif


    }

    public void LoadTweakingFile(TextAsset data)
    {

    }



    public void WriteTweakingFile()
    {
        #if UNITY_EDITOR
        JSONObject tmpObj = new JSONObject();

        currentDate = new DateTime();
        currentDate = DateTime.Now;

        tmpObj.AddField("TweakerName", TweakerName.ToString());
        tmpObj.AddField("Date", currentDate.ToString());

        string fileName = "";
        string fileData = tmpObj.ToString(true);
        fileName += "TweakingValues_" + TweakerName.ToString() + "_" + currentDate.Year + "_" + currentDate.Month + "_" + currentDate.Day + "_" + currentDate.Hour + "_" + currentDate.Minute + "_" + currentDate.Second+".JSON";

        writer = new StreamWriter("Assets/Resources/Data/"+fileName);
       
        
        using(StringReader sr = new StringReader(fileData))
        {
            string line;
            while((line = sr.ReadLine()) != null)
            {
                Debug.Log(line);
                writer.WriteLine(line);
            }
        }

        writer.Close();
        Debug.Log("printed");
        AssetDatabase.Refresh();
    #endif
    }

    public static string returnVariableName<T>(T variable)
    {

        System.Reflection.MemberInfo[] m = variable.GetType().GetMembers();
        for (int i = 0; i < m.Length; i ++ )
        {
            Debug.Log(m[i].ToString());
        }
        
        return m.ToString();
    }

    public int getValue(Type OriginClassType, string variableName, int currentValue)
    {
        foreach(TweakedVariable tv in variables)
        {
            if(tv.VariableName == variableName && tv.VariableOwnerClass == OriginClassType.ToString() && tv.VariableType == TweakableVariableTypes.INT)
            {
                return int.Parse(tv.VariableValue);
            }
        }

        AddToTweakingValues(OriginClassType, TweakableVariableTypes.INT, currentValue.ToString(), variableName);

        return NULL_INT;
    }

    public float getValue(Type OriginClassType, string variableName, float currentValue)
    {
        foreach (TweakedVariable tv in variables)
        {
            if (tv.VariableName == variableName && tv.VariableOwnerClass == OriginClassType.ToString() && tv.VariableType == TweakableVariableTypes.FLOAT)
            {
                return float.Parse(tv.VariableValue);
            }
        }

        AddToTweakingValues(OriginClassType, TweakableVariableTypes.FLOAT, currentValue.ToString(), variableName);

        return NULL_FLOAT;
    }

    public string getValue(Type OriginClassType, string variableName, string currentValue)
    {
        foreach (TweakedVariable tv in variables)
        {
            if (tv.VariableName == variableName && tv.VariableOwnerClass == OriginClassType.ToString() && tv.VariableType == TweakableVariableTypes.STRING)
            {
                return tv.VariableValue;
            }
        }

        AddToTweakingValues(OriginClassType, TweakableVariableTypes.STRING, currentValue.ToString(), variableName);

        return NULL_STRING;
    }

    public void AddToTweakingValues(Type _OwnerClass, TweakableVariableTypes variableType, string variableValue, string variableName)
    {
        variables.Add(new TweakedVariable(variableName, variableType, variableValue, _OwnerClass.ToString()));
    }
}

public class TweakedVariable
{
    private string _variableName;
    private TweakableVariableTypes _variableType;
    private string _variableValue;
    private string _variableOwnerClass;


    public TweakedVariable(string vname, TweakableVariableTypes vtype, string vvalue, string vowner)
    {
        _variableName = vname;
        _variableType = vtype;
        _variableValue = vvalue;
        _variableOwnerClass = vowner;
    }

    public string VariableName
    {
        get { return _variableName; }
        set { _variableName = value; }
    }

    public TweakableVariableTypes VariableType
    {
        get { return _variableType; }
        set { _variableType = value; }
    }
    
    public string VariableValue
    {
        get { return _variableValue; }
        set { _variableValue = value; }
    }
    
    public string VariableOwnerClass
    {
        get { return _variableOwnerClass; }
        set { _variableOwnerClass = value; }
    }

    
}

public enum TeamNames
{
    PhilippeThomazie,
    CrysGontier,
    EstebanSanchez,
    BenjaminDenis,
    OlivierAllouard,
    VelentinGrente,
    GuillaumeClaus
}

public enum TweakableVariableTypes
{
    INT,
    FLOAT,
    STRING
}

public static class MemberInfoGetting
{
    public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
    {
        MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
        return expressionBody.Member.Name;
    }
}
