using UnityEditor;
using UnityEngine;
using System.IO;

public class ImportUnityPackage : Editor
{
    [InitializeOnLoadMethod]
    static void ImportPackages()
    {
        // Ensure the directory path is correct relative to the project's root
        string[] packages = Directory.GetFiles("Packages/com.hppedeaf.single-url/Runtime", "*.unitypackage", SearchOption.AllDirectories);

        foreach (string package in packages)
        {
            AssetDatabase.ImportPackage(package, false); // The 'false' parameter avoids the import dialog
            Debug.Log("Imported package: " + package);
        }

        // Refresh the AssetDatabase after all packages are imported
        AssetDatabase.Refresh();

        // Schedule the deletion of this script file
        DeleteThisScript();
    }

    static void DeleteThisScript()
    {
        // Get the path to this script file
        string scriptFilePath = "Packages/com.hppedeaf.single-url/Editor/import.cs"; // Adjust the path to match your setup
        
        if (File.Exists(scriptFilePath))
        {
            File.Delete(scriptFilePath);
            Debug.Log("Deleted script: " + scriptFilePath);

            // After deletion, force Unity to refresh the AssetDatabase to remove the script from the project
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.LogWarning("Script file not found for deletion: " + scriptFilePath);
        }
    }
}
