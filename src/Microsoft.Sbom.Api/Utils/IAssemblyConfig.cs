// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Sbom.Extensions.Entities;

namespace Microsoft.Sbom.Api.Utils;

/// <summary>
/// Contans configuration items defined in the assembly.
/// </summary>
public interface IAssemblyConfig
{
    /// <summary>
    /// Gets the namespace base URI as defined in the assembly.
    /// </summary>
    public string DefaultSbomNamespaceBaseUri { get; }

    /// <summary>
    /// Gets the default value to use for ManifestInfo for validation action in case the user doesn't provide a
    /// value.
    /// </summary>
    public ManifestInfo DefaultManifestInfoForValidationAction { get; }

    /// <summary>
    /// Gets the default value to use for ManifestInfo for generation action in case the user doesn't provide a
    /// value.
    /// </summary>
    public ManifestInfo DefaultManifestInfoForGenerationAction { get; }

    /// <summary>
    /// Gets the directory where the current executing assembly is located.
    /// </summary>
    public string AssemblyDirectory { get; }

    /// <summary>
    /// Gets the package supplier derived from current assembly.
    /// </summary>
    public string DefaultPackageSupplier { get; }
}
