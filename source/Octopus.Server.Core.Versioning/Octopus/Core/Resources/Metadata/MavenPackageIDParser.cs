﻿using System;
using Octopus.Core.Constants;

namespace Octopus.Core.Resources.Metadata
{
    /// <summary>
    /// Maven package IDs come in the format: group#artifact
    /// </summary>
    public class MavenPackageIDParser : IPackageIDParser
    {       
        public BasePackageMetadata GetMetadataFromPackageID(string packageID)
        {
            var idAndVersionSplit = packageID.Split(JavaConstants.JAVA_FILENAME_DELIMITER);

            if (idAndVersionSplit.Length != 2)
            {
                throw new Exception(
                    $"Unable to extract the package ID from \"{packageID}\"");
            }

            return new BasePackageMetadata()
            {
                Id = packageID,
                FeedType = FeedType.Maven
            };
        }

        public PackageMetadata GetMetadataFromPackageName(string packageFile, string[] extensions)
        {
            var metadataAndExtension =
                PackageIdentifier.ExtractPackageExtensionAndMetadata(packageFile, extensions);

            var idAndVersion = metadataAndExtension.Item1;
            var pkg = new PackageMetadata {FileExtension = metadataAndExtension.Item2};

            if (string.IsNullOrEmpty(pkg.FileExtension))
            {
                throw new Exception($"Unable to determine filetype of file \"{packageFile}\"");
            }

            var idAndVersionSplit = idAndVersion.Split(JavaConstants.JAVA_FILENAME_DELIMITER);

            if (idAndVersionSplit.Length != 3)
            {
                throw new Exception(
                    $"Unable to extract the package ID and version from file \"{packageFile}\"");
            }

            pkg.Id = idAndVersionSplit[0] + JavaConstants.JAVA_FILENAME_DELIMITER + idAndVersionSplit[1];
            pkg.Version = idAndVersionSplit[2];
            pkg.FeedType = FeedType.Maven;
            pkg.PackageSearchPattern = pkg.Id + JavaConstants.JAVA_FILENAME_DELIMITER + pkg.Version + "*";
            pkg.PackageFileName = pkg.Id + JavaConstants.JAVA_FILENAME_DELIMITER + pkg.Version + "_";
            return pkg;
        }
    }
}