# Project Feature & Sample (WebApi)
- [x] Sample Initiate Project ( supporting multiple sdk and Manage Package Centrally across the project for easier update in the future) [PR #10](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/10)
- [x] Sample Controller , Validation , DTO and Service [PR #11](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/11)
- [x] Sample Controller , Validation , DTO and Service Minimal Api .NET [PR #14](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/14)
- [x] How to Logging to file [PR #12](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/12)
- [x] Sample Rate Limiter [PR #12](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/12)
- [x] How to Handle Exception [PR #13](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/13)
- [ ] Sample Database Connection , Migration , Seeder and Sample how to use
    - [ ] Sqlite Database
    - [ ] MS-SQL Database
    - [ ] Postgre-SQL Database
    - [ ] In-Memory Database
    
- [x] Sample Hybrid Cache ( in-app memory & optional Redis Connection ) [PR #14](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/14)
- [x] Sample Generate Docker Image For Production [PR #27](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/27)
- [x] Sample Github Action ( CI/CD ) [PR #26](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/26)
- [x] Sample Task Scheduling / Queue [PR #22](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/22)
- [x] Sample How to Use Secret Configuration [PR #22](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/22)
- [ ] Sample Sending Email
- [x] Sample Server Sent Events ( SSE ) - realtime feature only server can send data [PR #24](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/24)
- [x] Sample Real time connection ( a.k.a socket but in .NET known as SignalR ) - realtime feature server <-> client send data [PR #25](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/25)
- [ ] Sample Authentication / Authorization
- [ ] Sample Implement Apache Kafka
- [x] Sample How to Handle App Cancel Token & Linked Api Cance Token [PR #24](https://github.com/fearlessforever/my-dotnet-sample-and-note/pull/24)

# Project Feature & Sample (FE Blazor)
- [ ] How to make the same Blazor app optional can be full client rendering (webassembly/pwa) or server interactive (SSR) + client (CSR)

## How To Run
```bash
$ # default .net 7
$ ./run Api
$ ./run Api -lp http
$ # run Api in .net 8
$ ./run Api -lp http -p:NetCoreTargetFramework=net8.0
$ # run Api in .net 9
$ ./run Api -lp http -p:NetCoreTargetFramework=net9.0

$ # Hot Reload
$ ./run Api watch -lp http
$ ./run Api watch -lp http --property:NetCoreTargetFramework=net8.0
$ ./run Api watch -lp http --property:NetCoreTargetFramework=net9.0

$ # Build
$ ./run Api build
$ ./run Api build -p:NetCoreTargetFramework=net8.0
$ ./run Api build -p:NetCoreTargetFramework=net9.0
$
$ # Generate binlog for debugging
$ ./run Api build -bl:output.binlog 
$ ./run Api build -bl:output.binlog -p:NetCoreTargetFramework=net8.0
$ ./run Api build -bl:output.binlog -p:NetCoreTargetFramework=net9.0
```

## Build & Running the app in docker [Production]
```bash
$ ./run build-docker
$ ./run run-docker
$ ./run stop-docker
```

## Required & Optional
- .NET 7 SDK & Runtime
- .NET 8 SDK & Runtime ( Optional , for some application is mandatory in the future )
- .NET 9 SDK & Runtime ( Optional , for some application is mandatory in the future )
- Redis ( Optional , for some feature is mandatory like Queue / Scheduler )

## Swagger ( For WebApi in Development Environment )
- http://localhost:5000/swagger/index.html

## Docker Image ( production ready )
```bash
$ # download compiled app docker image
$ docker pull ghcr.io/fearlessforever/my-dotnet-sample-and-note:latest
$
$ # run the image and send to background
$ docker run -d \
  --name dotnet-web-api \
  -v $(pwd)/../Logs:/app/webapi/Logs \
  -e 'ASPNETCORE_URLS=http://+:5000;' \
  -e 'Features:UseSignalR=true' \
  -e 'ASPNETCORE_ENVIRONMENT=Development' \
  -w /app/webapi \
  -p 5000:5000 \
  --expose 5000 \
  ghcr.io/fearlessforever/my-dotnet-sample-and-note:latest \
  dotnet WebApi.dll
$
$ #stop / kill it process
$ docker kill dotnet-web-api
$ #remove unused container
$ docker rm dotnet-web-api
```

## For Debuging Postman file Environment & Collections
- [Postman Environment](Postman/My%20.NET%20Sample%20&%20Note%20-%20Env.postman_environment.json)
- [Postman Collections](Postman/My%20.NET%20Sample%20&%20Note.postman_collection.json)