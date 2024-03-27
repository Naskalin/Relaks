### Build win-x64
`dotnet publish -c Release -r win-x64 -p:SelfContained=true -p:PublishSingleFile=true --output ./bin/Release/net8.0/win-x64/publish`

### Build linux-x64
`dotnet publish -c Release -r linux-x64 -p:SelfContained=true -p:PublishSingleFile=true --output ./bin/Release/net8.0/linux-x64/publish`