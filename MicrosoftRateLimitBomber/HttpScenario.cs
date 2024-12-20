﻿using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Http.CSharp;
using System;
using System.Net.Http;

namespace MicrosoftRateLimitBomber
{
    public class HttpScenario
    {
        public void Run()
        {
            using var httpClient = new HttpClient();

            var scenario = Scenario.Create("http_scenario", async context =>
            {
                var request = Http.CreateRequest("GET", "https://localhost:7164/order")
                        .WithHeader("Content-Type", "application/json");

                var response = await Http.Send(httpClient, request);

                return response;
            })
                .WithoutWarmUp()
                .WithLoadSimulations(
                    Simulation.RampingInject(rate: 10, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(15)),
                    Simulation.Inject(rate: 1000, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(15)),
                    Simulation.RampingInject(rate: 0, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(15))
                );

            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFormats(ReportFormat.Html)
                .Run();
        }
    }
}