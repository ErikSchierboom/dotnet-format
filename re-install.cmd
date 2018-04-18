pushd src\dotnet-format
dotnet pack -c Release
dotnet tool uninstall -g dotnet-format
dotnet tool install -g --source-feed %cd%\bin\Release dotnet-format
popd