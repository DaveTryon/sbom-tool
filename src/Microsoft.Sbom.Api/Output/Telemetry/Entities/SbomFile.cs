// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Sbom.Extensions.Entities;

namespace Microsoft.Sbom.Api.Output.Telemetry.Entities;

/// <summary>
/// Represents a SBOM file object and contains additional properties
/// related to the file.
/// </summary>
public class SbomFile
{
    /// <summary>
    /// Gets or sets the name and version of the format of the generated SBOM.
    /// </summary>
    public ManifestInfo SbomFormatName { get; set; }

    /// <summary>
    /// Gets or sets the path where the final generated SBOM is placed.
    /// </summary>
    public string SbomFilePath { get; set; }

    /// <summary>
    /// Gets or sets the size of the SBOM file in bytes.
    /// </summary>
    public long FileSizeInBytes { get; set; }

    /// <summary>
    /// Gets or sets the total number of packages that were detected during the execution of the SBOM tool.
    /// </summary>
    public int TotalNumberOfPackages { get; set; }
}
