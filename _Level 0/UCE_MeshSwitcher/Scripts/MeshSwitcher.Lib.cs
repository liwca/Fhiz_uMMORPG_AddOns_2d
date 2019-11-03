#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class MeshSwitcherLib
{
    private string definestring;

    private const string define = "_iMMOMESHSWITCHER";

    static MeshSwitcherLib()
    {
        AddLibrayDefineIfNeeded();
    }

    private static void AddLibrayDefineIfNeeded()
    {
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string definestring = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
        string[] defines = definestring.Split(';');

        if (Contains(defines, define))
            return;

        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, (definestring + ";" + define));
        Debug.LogWarning("<b>" + define + "</b> added to <i>Scripting Define Symbols</i> for selected build target (" + EditorUserBuildSettings.activeBuildTarget.ToString() + ").");
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

namespace nuCode
{
    public struct MeshSwitcher
    {
        public const int versionNumber = 101;
        public const string versionString = "v1.01";
    }
}