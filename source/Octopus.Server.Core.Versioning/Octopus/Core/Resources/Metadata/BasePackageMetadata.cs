﻿namespace Octopus.Core.Resources
{
    /// <summary>
    /// Represents the metadata that can be extracted from the package id alone
    /// </summary>
    public class BasePackageMetadata
    {
        public string Id { get; set; }
        public FeedType FeedType { get; set; }
    }
}