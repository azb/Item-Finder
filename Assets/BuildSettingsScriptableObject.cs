using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using static UnityEditor.PlayerSettings;
#endif

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BuildSettingsScriptableObject", order = 1)]
public class BuildSettingsScriptableObject : ScriptableObject
{
    public enum BuildMode { HeadmountedDisplay, NonHeadmountedDisplay };

    public BuildMode buildMode;

    BuildMode buildModePrev;

    void CreateFilesFromTemplates(
        string ManifestTemplateName,
        string OVRGradleGenerationTemplateName,
        string OVRManifestTemplateName,
        string NonHeadmountedManifestName,
        string NonHeadmountedOVRGradleGenerationName,
        string NonHeadmountedOVRManifestName,
        string VirtualRealityManifestName,
        string VirtualRealityOVRGradleGenerationName,
        string VirtualRealityOVRManifestName
        )
    {
        //copy android manifest from template
        if (!File.Exists(NonHeadmountedManifestName) && !File.Exists(VirtualRealityManifestName))
        {
            Debug.Log("Manifests don't exist yet, creating from template.");

            if (buildMode == BuildMode.HeadmountedDisplay)
            {
                File.Copy(ManifestTemplateName, VirtualRealityManifestName);
            }
            else
            {
                File.Copy(ManifestTemplateName, NonHeadmountedManifestName);
            }
        }

        //copy gradle generation script from template
        if (!File.Exists(NonHeadmountedOVRGradleGenerationName) && !File.Exists(VirtualRealityOVRGradleGenerationName))
        {
            Debug.Log("OVR Gradle Generation Script doesn't exist yet, creating from template.");

            if (buildMode == BuildMode.HeadmountedDisplay)
            {
                File.Copy(OVRGradleGenerationTemplateName, VirtualRealityOVRGradleGenerationName);
            }
            else
            {
                File.Copy(OVRGradleGenerationTemplateName, NonHeadmountedOVRGradleGenerationName);
            }
        }

        //copy ovr manifest from template
        /*
        if (!File.Exists(NonHeadmountedOVRManifestName) && !File.Exists(VirtualRealityOVRManifestName))
        {
            Debug.Log("OVR Manifests don't exist yet, creating from template.");

            if (buildMode == BuildMode.HeadmountedDisplay)
            {
                File.Copy(OVRManifestTemplateName, VirtualRealityOVRManifestName);
            }
            else
            {
                File.Copy(OVRManifestTemplateName, NonHeadmountedOVRManifestName);
            }
        }*/

    }

    void OnValidate()
    {
        if (buildModePrev == buildMode)
        {
            return;
        }
        else
        {
            buildModePrev = buildMode;
        }
        //return;
        string ManifestTemplateName = UnityEngine.Application.dataPath + "/Plugins/Android/AndroidManifestTemplate.xml";
        string NonHeadmountedManifestName = UnityEngine.Application.dataPath + "/Plugins/Android/AndroidManifestDontUse.xml";
        string VirtualRealityManifestName = UnityEngine.Application.dataPath + "/Plugins/Android/AndroidManifest.xml";

        string OVRGradleGenerationTemplateName = UnityEngine.Application.dataPath + "/Oculus/VR/Editor/OVRGradleGenerationTemplateDontUse.dontUse";
        string NonHeadmountedOVRGradleGenerationName = UnityEngine.Application.dataPath + "/Oculus/VR/Editor/OVRGradleGenerationDontUse.dontUse";
        string VirtualRealityOVRGradleGenerationName = UnityEngine.Application.dataPath + "/Oculus/VR/Editor/OVRGradleGeneration.cs";

        string OVRManifestTemplateName = UnityEngine.Application.dataPath + "/Oculus/VR/Editor/OVRGradleGenerationTemplate.dontUse";
        string NonHeadmountedOVRManifestName = UnityEngine.Application.dataPath + "/Plugins/Android/AndroidManifest.xml";
        string VirtualRealityOVRManifestName = UnityEngine.Application.dataPath + "/Oculus/VR/Editor/OVRGradleGeneration.cs";

        //CreateFilesFromTemplates(
        //     ManifestTemplateName,
        //     OVRGradleGenerationTemplateName,
        //     OVRManifestTemplateName,
        //     NonHeadmountedManifestName,
        //     NonHeadmountedOVRGradleGenerationName,
        //     NonHeadmountedOVRManifestName,
        //     VirtualRealityManifestName,
        //     VirtualRealityOVRGradleGenerationName,
        //     VirtualRealityOVRManifestName
        //);

        Debug.Log("OnValidate");
        
        
#if UNITY_EDITOR
        if (buildMode == BuildMode.NonHeadmountedDisplay)
        {
            //NOTE: deleting the VR android manifest is necessary to make the Android Icon show up on the spectator device (otherwise it is hidden by default for oculus vr apps)
            RenameFile(VirtualRealityManifestName, NonHeadmountedManifestName);
            RenameFile(VirtualRealityOVRGradleGenerationName, NonHeadmountedOVRGradleGenerationName);
            //RenameFile(VirtualRealityOVRManifestName, NonHeadmountedOVRManifestName);

            File.Delete(VirtualRealityManifestName);
            File.Delete(VirtualRealityOVRGradleGenerationName);
            //File.Delete(VirtualRealityOVRManifestName);

            PlayerSettings.virtualRealitySupported = false;
        }
        else
        if (buildMode == BuildMode.HeadmountedDisplay)
        {
            RenameFile(NonHeadmountedManifestName, VirtualRealityManifestName);
            RenameFile(NonHeadmountedOVRGradleGenerationName, VirtualRealityOVRGradleGenerationName);
            //RenameFile(NonHeadmountedOVRManifestName, VirtualRealityOVRManifestName);

            File.Delete(NonHeadmountedManifestName);
            File.Delete(NonHeadmountedOVRGradleGenerationName);
            //File.Delete(NonHeadmountedOVRManifestName);

            PlayerSettings.virtualRealitySupported = true;
        }
#endif

    }

    void RenameFile(string oldfilename, string newfilename)
    {
        if (File.Exists(oldfilename))
        {
            if (!File.Exists(newfilename))
            {
                Debug.Log("renaming " + oldfilename + " to " + newfilename);
                System.IO.File.Move(oldfilename, newfilename);
            }
            else
            {
                Debug.Log("File already exists " + newfilename);
            }
        }
    }
}
