using System;

namespace IssueTrackerLib
{
    public class IssueTracker
    {
        private const String TrackerURLFormat = "http://code.google.com/p/{0}";
        private const String TrackerListFormat = "{0}/issues/list";
        private String trackerURL;

        public IssueTracker(String p_projectName)
        {
            trackerURL = String.Format(IssueTracker.TrackerURLFormat, p_projectName);
        }

        public bool SubmitIssue(String version, String title, String description)
        {
            throw new NotImplementedException();
        }

        public void OpenTrackerLink()
        {
            System.Diagnostics.Process.Start(String.Format(IssueTracker.TrackerListFormat, trackerURL));
        }
    }
}
