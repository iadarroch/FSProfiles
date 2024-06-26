﻿namespace FSProfiles.Common.Models
{
    public class ProgressEvent
    {
        public string? Message { get; set; }
        public int? Progress { get; set; }

        public ProgressEvent(string? message = null, int? progress = null)
        {
            Message = message;
            Progress = progress;
        }
    }
}