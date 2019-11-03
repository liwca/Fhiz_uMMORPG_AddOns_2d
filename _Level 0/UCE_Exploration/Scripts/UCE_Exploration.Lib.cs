#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ExplorationLib
{
    private const string define = "_iMMOEXPLORATION";

    static ExplorationLib()
    {
        AddLibrayDefineIfNeeded();
    }

    private static void AddLibrayDefineIfNeeded()
    {
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string definestring = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
        string[] defines = definestring.Split(';');

#if !_iMMOTOOLS
		Debug.LogWarning("<b>UCE Tools</b> are always required. Free download: https://indie-mmo.net");
#endif

        if (Contains(defines, define))
            return;

        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, (definestring + ";" + define));
        Debug.LogWarning("<b>AddOn imported!</b> - to complete installation please refer to the included README and follow instructions.");
        Debug.Log("<b>" + define + "</b> added to <i>Scripting Define Symbols</i> for selected build target (" + EditorUserBuildSettings.activeBuildTarget.ToString() + ").");
    }

    private static bool Contains(string[] defines, string define)
    {
        foreach (string def in defines)
        {
            if (def == define)
                return true;
        }
        return false;
    }
}

#endif