using System.Collections.Generic;
using System.Collections;
using System.Linq;

using UnityEngine;

using ContextMenus;
using Projects;
using Utility;

namespace Header.RecentProjects
{
    public class RecentProjectRegisterer : MonoBehaviour
    {
        public static RecentProjectRegisterer instance { get; private set; }

        public List<string> recentProjects = new List<string>();
        public ContextMenuItem recentProjectPrefab;
        public Transform recentProjectsContainer;
        public ContextMenus.ContextMenu contextMenu;
        public RectTransform contextMenuTransform;
        public ContextMenus.ContextMenu fileContextMenu;

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

        private void Start()
        {
            recentProjects = AdvancedPlayerPrefs.GetStringArray("recentProjects", 20).ToList();

            if (recentProjects.Count > 0)
            {
                for (int i = 0; i < recentProjects.Count; i++)
                {
                    ContextMenuItem item = Instantiate(recentProjectPrefab, recentProjectsContainer);
                    item.title = recentProjects[i];
                    item.menu = contextMenu;

                    item.action.AddListener(() =>
                    {
                        ProjectManager.instance.OpenProjectFromURL(item.title);
                        fileContextMenu.Close();
                    });

                    item.UpdateGraphics();

                    float width = recentProjects[i].Length * 9;
                    if (contextMenuTransform.sizeDelta.x < width)
                    {
                        contextMenuTransform.sizeDelta = new Vector2(width, contextMenuTransform.sizeDelta.y);
                    }
                }
            }

            float height = 36 + 32 * recentProjects.Count;
            contextMenuTransform.sizeDelta = new Vector2(contextMenuTransform.sizeDelta.x, height);
        }

        private void OnApplicationQuit()
        {
            AdvancedPlayerPrefs.SetStringArray("recentProjects", recentProjects.ToArray());
        }

        public void RegisterProject(Project project)
        {
            if (project == null)
            {
                Debug.LogError("Cannot register null project!");
                return;
            }

            if (recentProjects.Contains(project.path))
            {
                Debug.LogWarning("Recent projects already contains this project!");
                recentProjects.Remove(project.path);
            }

            if (recentProjects.Count > 0)
            {
                recentProjects.Insert(0, project.path);
                return;
            }

            recentProjects.Add(project.path);
        }
    }
}