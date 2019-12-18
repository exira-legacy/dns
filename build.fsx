#r "paket:
version 5.241.2
framework: netstandard20
source https://api.nuget.org/v3/index.json
nuget Be.Vlaanderen.Basisregisters.Build.Pipeline 3.2.0 //"

#load "packages/Be.Vlaanderen.Basisregisters.Build.Pipeline/Content/build-generic.fsx"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.IO.FileSystemOperators
open ``Build-generic``

// The buildserver passes in `BITBUCKET_BUILD_NUMBER` as an integer to version the results
// and `BUILD_DOCKER_REGISTRY` to point to a Docker registry to push the resulting Docker images.

// NpmInstall
// Run an `npm install` to setup Commitizen and Semantic Release.

// DotNetCli
// Checks if the requested .NET Core SDK and runtime version defined in global.json are available.
// We are pedantic about these being the exact versions to have identical builds everywhere.

// Clean
// Make sure we have a clean build directory to start with.

// Restore
// Restore dependencies for debian.8-x64 and win10-x64 using dotnet restore and Paket.

// Build
// Builds the solution in Release mode with the .NET Core SDK and runtime specified in global.json
// It builds it platform-neutral, debian.8-x64 and win10-x64 version.

// Test
// Runs `dotnet test` against the test projects.

// Publish
// Runs a `dotnet publish` for the debian.8-x64 and win10-x64 version as a self-contained application.
// It does this using the Release configuration.

// Pack
// Packs the solution using Paket in Release mode and places the result in the dist folder.
// This is usually used to build documentation NuGet packages.

// Containerize
// Executes a `docker build` to package the application as a docker image. It does not use a Docker cache.
// The result is tagged as latest and with the current version number.

// DockerLogin
// Executes `ci-docker-login.sh`, which does an aws ecr login to login to Amazon Elastic Container Registry.
// This uses the local aws settings, make sure they are working!

// Push
// Executes `docker push` to push the built images to the registry.

let dockerRepository = "dns"
let assemblyVersionNumber = (sprintf "2.%s")
let nugetVersionNumber = (sprintf "%s")

let build = buildSolution assemblyVersionNumber
let test = testSolution
let publish = publish assemblyVersionNumber
let pack = pack nugetVersionNumber
let push = push dockerRepository
let containerize = containerize dockerRepository

// Solution -----------------------------------------------------------------------

Target.create "Restore_Solution" (fun _ -> restore "Dns")

Target.create "Build_Solution" (fun _ -> build "Dns")

Target.create "Test_Solution" (fun _ -> test "Dns")

Target.create "Publish_Solution" (fun _ ->
  [
    "Dns.Api"
  ] |> List.iter publish)

Target.create "Pack_Solution" (fun _ ->
  [
    "Dns.Api"
  ] |> List.iter pack)

Target.create "Containerize_Api" (fun _ -> containerize "Dns.Api" "api")
Target.create "PushContainer_Api" (fun _ -> push "api")

Target.create "Containerize_Projector" (fun _ -> containerize "Dns.Projector" "projector")
Target.create "PushContainer_Projector" (fun _ -> push "projector")

// --------------------------------------------------------------------------------

Target.create "Build" ignore
Target.create "Test" ignore
Target.create "Publish" ignore
Target.create "Pack" ignore
Target.create "Containerize" ignore
Target.create "Push" ignore

"NpmInstall" ==> "DotNetCli" ==> "Clean" ==> "Restore_Solution" ==> "Build_Solution" ==> "Build"
"Build" ==> "Test_Solution" ==> "Test"
"Test" ==> "Publish_Solution" ==> "Publish"
"Publish" ==> "Pack_Solution" ==> "Pack"
"Pack" ==> "Containerize_Api" ==> "Containerize_Projector" ==> "Containerize"
"Containerize" ==> "DockerLogin" ==> "PushContainer_Api" ==> "PushContainer_Projector" ==> "Push"

Target.runOrDefault "Test"
