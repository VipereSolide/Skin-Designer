using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;

using UnityEngine;

using Header.RecentProjects;
using Inspector;
using Texture;
using SFB;

namespace Projects
{
    public class ProjectManager : MonoBehaviour
    {
        public static ProjectManager instance { get; private set; }

        public Project currentProject;

        [Header("Resources")]

        public Animator savedProjectPopupAnimator;

        private string storedLastLocation;

        private void RegisterSingleton()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }

            Destroy(this);
        }

        private void Awake()
        {
            RegisterSingleton();
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetString(nameof(storedLastLocation), storedLastLocation);
        }

        private void Start()
        {
            currentProject = null;

            if (PlayerPrefs.HasKey(nameof(storedLastLocation)))
            {
                storedLastLocation = PlayerPrefs.GetString(nameof(storedLastLocation));
                return;
            }

            storedLastLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public void OpenProject(Project project)
        {
            currentProject = project;

            // load channel textures
            ChannelManager manager = ChannelManager.instance;

            for (int i = 0; i < manager.channels.Count; i++)
            {
                bool isActivated = false;
                string texture = currentProject.texturePaths[Mathf.Min(i, currentProject.texturePaths.Length - 1)];

                if (texture == "default")
                {
                    texture = String.Empty;
                }
                else
                {
                    isActivated = true;
                }

                manager.channels[i].texture = new TextureHolder(texture, false);
                manager.channels[i].enabled = isActivated;
                ChannelViewer.instance.InitChannel();
            }

            RecentProjectRegisterer.instance.RegisterProject(currentProject);
            manager.UpdateTargetTextureFromChannelTextures();
        }

        public void OpenProjectFromFile()
        {
            string[] paths = StandaloneFileBrowser.OpenFilePanel("Open Project", storedLastLocation, "json", false);

            if (paths == null || paths.Length == 0)
            {
                Debug.LogWarning("No project were selected to be opened.");
                return;
            }

            OpenProjectFromURL(paths[0]);
        }

        public void OpenProjectFromURL(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogError("Cannot open project for null or empty url!");
                return;
            }

            string projectContent = File.ReadAllText(url);
            Project project = FromSavedProject(projectContent);
            project.path = url;

            OpenProject(project);
        }

        public void CreateProject()
        {
            string path = StandaloneFileBrowser.SaveFilePanel("Save Project", storedLastLocation, "new project", "json");
            if (path == string.Empty)
            {
                Debug.LogWarning("No path were selected to save this project.");
                return;
            }

            storedLastLocation = Path.GetDirectoryName(path);
            
            currentProject = new Project();
            currentProject.path = path;
            UpdateProjectTextures(currentProject);
            string projectContent = ToSavedProject(currentProject);
            
            File.WriteAllText(path, projectContent);
        }

        private void WriteProjectChanges()
        {
            File.WriteAllText(currentProject.path, ToSavedProject(currentProject));
        }

        private void UpdateProjectTextures(Project project)
        {
            ChannelManager manager = ChannelManager.instance;

            for (int i = 0; i < manager.channels.Count; i++)
            {
                string treated = manager.channels[i].texture.path;

                if (treated == string.Empty)
                {
                    treated = "default";
                }

                project.texturePaths[Mathf.Min(i, project.texturePaths.Length - 1)] = treated;
            }
        }

        public void SaveCurrentProject()
        {
            if (currentProject == null)
            {
                CreateProject();

                return;
            }

            UpdateProjectTextures(currentProject);
            WriteProjectChanges();

            savedProjectPopupAnimator.Play("call");
        }

        public void SaveProjectAt(string projectContent)
        {

        }

        public static string ToSavedProject(Project project)
        {
            return JsonUtility.ToJson(project);
        }

        public static Project FromSavedProject(string projectContent)
        {
            return JsonUtility.FromJson<Project>(projectContent);
        }
    }
}