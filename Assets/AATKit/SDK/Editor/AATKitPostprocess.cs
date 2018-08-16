using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.Diagnostics;
using System.Collections;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Text;

public class AATKitPostprocess : MonoBehaviour
{
	private static readonly string LibPath = "usr/lib/";

	private static readonly string AatDirectoryRelativePath = "aat";

    private static readonly string AaTDirectoryInUnityRelativePath = "/AATKit/SDK/Editor/iOS/aat";

	private static PBXProject proj;

	private static string targetGuid;

	private static string projPath;

	[PostProcessBuildAttribute(999)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject)
	{        
		if (target == BuildTarget.iOS) 
		{
			ModifyPbxProject (pathToBuildProject);
			ModifyPlistForTemporaryAppTransportSecurityFix (pathToBuildProject);
		}
	}

	static void ModifyPbxProject (string pathToBuildProject)
	{
		projPath = pathToBuildProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
		proj = new PBXProject ();
		proj.ReadFromString (File.ReadAllText (projPath));
		targetGuid = proj.TargetGuidByName ("Unity-iPhone");

		AddSystemLibraries (projPath);
		CopyAATKitLibrariesAndResources(pathToBuildProject);

		UnityEngine.Debug.Log("[AATKitPostprocess] adding library search path");
		proj.AddBuildProperty(targetGuid, "LIBRARY_SEARCH_PATHS", "$(PROJECT_DIR)/aat/**");

		UnityEngine.Debug.Log("[AATKitPostprocess] adding linker flag -ObjC");
		proj.SetBuildProperty(targetGuid, "OTHER_LDFLAGS", "-ObjC");

		UnityEngine.Debug.Log("[AATKitPostprocess] disabling bitode.");
		proj.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "flase");

		File.WriteAllText(projPath, proj.WriteToString ());
	}

	static void ModifyPlistForTemporaryAppTransportSecurityFix (string pathToBuildProject)
	{
		string plistPath = pathToBuildProject + "/Info.plist";
		PlistDocument plist = new PlistDocument ();
		plist.ReadFromString (File.ReadAllText (plistPath));
		AddAppTransportSecurity (plist);
		File.WriteAllText (plistPath, plist.WriteToString ());
	}

	static void CopyAATKitLibrariesAndResources (string pathToBuildProject)
	{
		UnityEngine.Debug.Log ("[AATKitPostprocess] copy aatkit libraries and resources");

		string buildAatDirectoryAbsolutePath = pathToBuildProject + "/" + AatDirectoryRelativePath;
		DeleteOldFiles (pathToBuildProject, buildAatDirectoryAbsolutePath);

		Directory.CreateDirectory (buildAatDirectoryAbsolutePath);
		string unityAatPath = Application.dataPath + AaTDirectoryInUnityRelativePath;

		CopyFiles (buildAatDirectoryAbsolutePath, unityAatPath, unityAatPath);
		CreateDirectoriesAndCopyFiles (buildAatDirectoryAbsolutePath, unityAatPath);
	}

	static void DeleteOldFiles (string pathToBuildProject, string buildAatDirectoryAbsolutePath)
	{
		if (Directory.Exists (pathToBuildProject + "/" + AatDirectoryRelativePath)) {
			Directory.Delete (buildAatDirectoryAbsolutePath);
		}
	}

	static void CreateDirectoriesAndCopyFiles (string buildAatDirectoryAbsolutePath, string unityAatPath)
	{
		foreach (string directory in Directory.GetDirectories (unityAatPath, "*", SearchOption.AllDirectories)) {
			string directoryToCreate = directory.Replace (unityAatPath, buildAatDirectoryAbsolutePath);

			string directoryName = Path.GetFileName (directoryToCreate);
			if (directoryName.Contains ("_MRAID.bundle")) {
				directoryToCreate = directoryToCreate.Replace (directoryName, "MRAID.bundle");
			}

			Directory.CreateDirectory (directoryToCreate);
			CopyFiles (buildAatDirectoryAbsolutePath, unityAatPath, directory);

			if (directoryName.Contains (".bundle")) 
			{
				AddFileToBuild (buildAatDirectoryAbsolutePath, directoryToCreate);
			}
		}
	}

	static void CopyFiles (string buildAatDirectoryAbsolutePath, string unityAatPath, string directory)
	{
		foreach (string file in Directory.GetFiles (directory)) {

			if (!file.Contains (".DS_Store") && !file.Contains (".meta")) {
				string destination = file;
				if (destination.Contains (".ignorejs")) {
					destination = destination.Replace (".ignorejs", ".js");
				}
				if (destination.Contains ("1_MRAID.bundle")) {
					destination = destination.Replace ("1_MRAID.bundle", "MRAID.bundle");
				}

				destination = destination.Replace (unityAatPath, buildAatDirectoryAbsolutePath);
				File.Copy (file, destination);

				if (!file.Contains (".bundle")) 
				{
					AddFileToBuild (buildAatDirectoryAbsolutePath, destination);
				}
			}
		}
	}

	static void AddFileToBuild (string buildAatDirectoryAbsolutePath, string destination)
	{
		string projectPath = AatDirectoryRelativePath + destination.Replace (buildAatDirectoryAbsolutePath, string.Empty);
		string fileGuid = proj.AddFile (projectPath, projectPath, PBXSourceTree.Source);
		proj.AddFileToBuild (targetGuid, fileGuid);
	}

	static void AddSystemLibraries (string projPath)
	{
		UnityEngine.Debug.Log ("[AATKitPostprocess] adding system libraries");
		StreamReader streamReader = new StreamReader ("Assets/AATKit/SDK/Editor/frameworks.cfg", Encoding.Default);
		string line;
		do {
			line = streamReader.ReadLine ();
			if (line == null) {
				continue;
			}
			if (line.Contains (".framework")) {
				proj.AddFrameworkToProject (targetGuid, line, false);
			}
			else
				if (line.Contains (".dylib")) {
					string fileGuid = proj.AddFile (LibPath + line, "Frameworks/" + line, PBXSourceTree.Sdk);
					proj.AddFileToBuild (targetGuid, fileGuid);
				}
		}
		while (line != null);
		streamReader.Close ();
	}

	static void AddAppTransportSecurity (PlistDocument plist)
	{
		UnityEngine.Debug.Log ("[AATKitPostprocess] adding AppTransportSecurity");
		PlistElementDict rootDict = plist.root;
		var atsKey = "NSAppTransportSecurity";
		PlistElementDict atsDict = rootDict.CreateDict (atsKey);
		var aalKey = "NSAllowsArbitraryLoads";
		atsDict.SetBoolean (aalKey, true);
	}
}