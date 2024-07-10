# Project Feature & Sample
- [x] Sample Initiate Project ( supporting multiple sdk and Manage Package Centrally across the project for easier update in the future) PR #10
- [x] Sample Controller , Validation , DTO and Service PR #11
- [x] How to Logging to file PR #12
- [x] Sample Rate Limiter PR #12
- [x] How to Handle Exception PR #13
- [ ] Sample Database Connection , Migration , Seeder and Sample how to use
- [ ] Sample Redis Connection for Cache
- [ ] Sample Generate Docker Image For Production
- [ ] Sample Github Action ( CI/CD )
- [ ] Sample Task Scheduling / Queue
- [ ] Sample Sending Email
- [ ] Sample Server Sent Events ( SSE ) - realtime feature only server can send data
- [ ] Sample Real time connection ( a.k.a socket but in .NET known as SignalR ) - realtime feature server <-> client send data
- [ ] Sample Authentication / Authorization
- [ ] Sample Implement Apache Kafka

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


## Required & Optional
- .NET 7 SDK & Runtime
- .NET 8 SDK & Runtime ( Optional , for some application is mandatory in the future )
- .NET 9 SDK & Runtime ( Optional , for some application is mandatory in the future )

## Swagger ( For WebApi in Development Environment )
- http://localhost:5000/swagger/index.html

## For Debuging Postman file Environment & Collections
- [Postman Environment](Postman/My%20.NET%20Sample%20&%20Note%20-%20Env.postman_environment.json)
- [Postman Collections](Postman/My%20.NET%20Sample%20&%20Note.postman_collection.json)