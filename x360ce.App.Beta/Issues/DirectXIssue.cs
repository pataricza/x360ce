﻿using System;
using Microsoft.Win32;
using System.Linq;
using x360ce.Engine;
using JocysCom.ClassLibrary.Controls.IssuesControl;

namespace x360ce.App.Issues
{
    public class DirectXIssue : IssueItem
    {
        public DirectXIssue() : base()
        {
            Name = "DirectX";
            FixName = "Download";
        }

        public override void CheckTask()
        {
            var xiFi = EngineHelper.GetMsXInputLocation();
            // If required file is missing then error will be critical.
            if (xiFi.Exists)
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\DirectX"))
                {
                    string versionString = key.GetValue("Version") as string;
                    Version version;
                    // If DirectX 9 was found then...
                    if (Version.TryParse(versionString, out version) && version.Minor == 9)
                    {
                        SetSeverity(IssueSeverity.None);
                        return;
                    }
                }
                SetSeverity(
                    IssueSeverity.Critical, 0,
                    "Microsoft DirectX 9 not found.\r\n" +
                    "You can click the link below to download Microsoft DirectX 9.0c:\r\n" +
                    "http://www.microsoft.com/en-us/download/details.aspx?id=8109"
                );
            }
            else
            {
                SetSeverity(
                    IssueSeverity.Critical, 0,
                    "Microsoft DirectX 9 not found (XInput).\r\n"+
                    "You can click the link below to download Microsoft DirectX 9.0c:\r\n" +
                    "http://www.microsoft.com/en-us/download/details.aspx?id=8109"
                );
            }
        }

        public override void FixTask()
        {
            EngineHelper.OpenUrl("http://www.microsoft.com/en-us/download/details.aspx?id=8109");
        }

    }
}
