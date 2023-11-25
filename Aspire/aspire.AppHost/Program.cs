var builder = DistributedApplication.CreateBuilder(args);

var apiservice = builder.AddProject<Projects.aspire_ApiService>("apiservice");

builder.AddProject<Projects.aspire_Web>("webfrontend")
    .WithReference(apiservice);

builder.Build().Run();
