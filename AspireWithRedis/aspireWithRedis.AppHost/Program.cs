var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedisContainer("cache");

var apiservice = builder.AddProject<Projects.aspireWithRedis_ApiService>("apiservice");

builder.AddProject<Projects.aspireWithRedis_Web>("webfrontend")
    .WithReference(apiservice)
    .WithReference(cache);

builder.Build().Run();
