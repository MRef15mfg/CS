using System;
using System.Collections.Generic;

public class AppConfig
{
    public bool EnableStatistics { get; set; } = true;
    public bool EnableModeration { get; set; } = true;
    
    public string KeylogFilePath { get; set; } = "keylog.txt";
    public string AppLogFilePath { get; set; } = "apps_log.txt";
    public string ModerationFilePath { get; set; } = "moderation_log.txt";
    
    public List<string> ForbiddenWords { get; set; } = new List<string>();
    public List<string> ForbiddenApps { get; set; } = new List<string>();
}