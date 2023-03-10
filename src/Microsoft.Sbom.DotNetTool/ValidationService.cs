﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Sbom.Api;
using Microsoft.Sbom.Api.Output.Telemetry;
using Microsoft.Sbom.Api.Workflows;
using IConfiguration = Microsoft.Sbom.Common.Config.IConfiguration;

namespace Microsoft.Sbom.Tool
{
    public class ValidationService : IHostedService
    {
        private readonly IWorkflow<SbomValidationWorkflow> validationWorkflow;

        private readonly IWorkflow<SbomParserBasedValidationWorkflow> parserValidationWorkflow;

        private readonly IConfiguration configuration;

        private readonly IRecorder recorder;

        private readonly IHostApplicationLifetime hostApplicationLifetime;

        public ValidationService(
            IConfiguration configuration,
            IWorkflow<SbomValidationWorkflow> validationWorkflow,
            IWorkflow<SbomParserBasedValidationWorkflow> parserValidationWorkflow,
            IRecorder recorder,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            this.validationWorkflow = validationWorkflow;
            this.parserValidationWorkflow = parserValidationWorkflow;
            this.configuration = configuration;
            this.recorder = recorder;
            this.hostApplicationLifetime = hostApplicationLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            bool result;
            try
            {
                if (configuration.ManifestInfo.Value.Contains(Api.Utils.Constants.SPDX22ManifestInfo))
                {
                    result = await parserValidationWorkflow.RunAsync();
                }
                else
                {
                    // On deprecation path.
                    Console.WriteLine($"This validation workflow is soon going to be deprecated. Please switch to the SPDX validation.");
                    result = await validationWorkflow.RunAsync();
                }

                await recorder.FinalizeAndLogTelemetryAsync();
                Environment.ExitCode = result ? (int)ExitCode.Success : (int)ExitCode.GeneralError;
            }
            catch (Exception e)
            {
                var message = e.InnerException != null ? e.InnerException.Message : e.Message;
                Console.WriteLine($"Encountered error while running ManifestTool validation workflow. Error: {message}");
                Environment.ExitCode = (int)ExitCode.GeneralError;
            }

            hostApplicationLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}