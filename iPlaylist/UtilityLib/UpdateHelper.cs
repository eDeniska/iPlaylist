using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace UtilityLib
{
    public class UpdateHelper
    {
        private String currentVersionFile = String.Empty;
        private String version = String.Empty;
        private String newVersion = String.Empty;
        private String updateURL = String.Empty;
        private String downloadedFile = String.Empty;
        private WebClient client = null;

        
        /// <summary>
        /// Update Helper creation
        /// </summary>
        /// <param name="fileUrl">URL of current-release.txt file</param>
        /// <param name="currentVersion">Current version of application</param>
        public UpdateHelper(String fileUrl, String currentVersion)
        {
            currentVersionFile = fileUrl;
            version = currentVersion;
            client = new WebClient();
            client.Proxy = WebRequest.DefaultWebProxy;
            if (client.Proxy != null)
            {
                client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
        }

        /// <summary>
        /// Check, if update is available 
        /// (could throw exceptions)
        /// </summary>
        /// <returns>Is update available?</returns>
        public bool IsUpdateAvailable()
        {
            List<String> infoList = new List<String>();
            
            infoList.AddRange(client.DownloadString(currentVersionFile).Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries));

            newVersion = infoList[0];
            updateURL = infoList[1];

            return (!(version.Equals(newVersion)));
        }

        /// <summary>
        /// Start background update process
        /// (could throw expections)
        /// </summary>
        /// <param name="mainForm">Main application form - to close application</param>
        public void StartUpdate(Form mainForm)
        {
            client.DownloadFileCompleted += UpdateDownloaded;

            downloadedFile = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())).FullName + 
                Path.DirectorySeparatorChar + updateURL.Substring(updateURL.LastIndexOf("/") + 1);
            
            client.DownloadFileAsync(new Uri(updateURL), downloadedFile, mainForm);
        }

        /// <summary>
        /// New version available on server
        /// </summary>
        /// <returns>Version available</returns>
        public String AvailableVersion()
        {
            return newVersion;
        }

        /// <summary>
        /// When update is downloaded, starts installation and closes application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDownloaded(object sender, AsyncCompletedEventArgs e)
        {
            if ((!(e.Cancelled)) && (e.Error == null))
            {
                try
                {
                    Process.Start(downloadedFile);
                    Form mainForm = e.UserState as Form;
                    mainForm.Close();
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    Log.Write(String.Format("UpdateHelper.UpdateDownloaded: URL=[{0}], LocalFile=[{1}]", updateURL, downloadedFile));
                    Log.Write("UpdateHelper.UpdateDownloaded: AsyncCompletedEventArgs object contents:", e);
                }
            }
        }
    }
}
