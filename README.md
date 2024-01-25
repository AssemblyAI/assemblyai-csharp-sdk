# AssemblyAI C# SDK

[![NuGet](https://img.shields.io/nuget/v/AssemblyAI.svg)](https://www.nuget.org/packages/AssemblyAI.net/)
[![fern shield](https://img.shields.io/badge/%F0%9F%8C%BF-SDK%20generated%20by%20Fern-brightgreen)](https://buildwithfern.com/?utm_source=assemblyai/assemblyai-java-sdk/readme)
[![GitHub License](https://img.shields.io/github/license/AssemblyAI/assemblyai-java-sdk)](https://github.com/AssemblyAI/assemblyai-java-sdk/blob/main/LICENSE)
[![AssemblyAI Twitter](https://img.shields.io/twitter/follow/AssemblyAI?label=%40AssemblyAI&style=social)](https://twitter.com/AssemblyAI)
[![AssemblyAI YouTube](https://img.shields.io/youtube/channel/subscribers/UCtatfZMf-8EkIwASXM4ts0A)](https://www.youtube.com/@AssemblyAI)
[![Discord](https://img.shields.io/discord/875120158014853141?logo=discord&label=Discord&link=https%3A%2F%2Fdiscord.com%2Fchannels%2F875120158014853141&style=social)
](https://assemblyai.com/discord)

## Documentation

API reference documentation is available [here](https://www.assemblyai.com/docs/).

## Installation

Using the [.NET Core command-line interface (CLI) tools](https://learn.microsoft.com/en-us/dotnet/core/tools/):

```sh
dotnet add package Stripe.net
```

Using the [NuGet Command Line Interface (CLI)](https://learn.microsoft.com/en-us/nuget/reference/nuget-exe-cli-reference?tabs=macos):

```sh
nuget install Stripe.net
```

Using the [Package Manager Console](https://learn.microsoft.com/en-us/nuget/consume-packages/install-use-packages-powershell):

```powershell
Install-Package Stripe.net
```

## Usage 
Instantiate the SDK using the `AssemblyAI` class. 

```csharp
  using AssemblyaAI;

  aai = new AssemblyAI("YOUR_API_KEY")
  
  TranscriptResponse transcript = 
      aai.Transcript.get("transcript_id");
  
  System.Console.WriteLine("Received transcript!", transcript);
```

## Client Options
You can specify a variety of options for the SDK by using
the client options class. 

```csharp
aai = new AssemblyAI("YOUR_API_KEY", new ClientOptions{
    HttpClient = ... // Override the Http Client
    MaxRetries = ... // Override the Max Retries
    BaseURL = ... // Override the Base URL
})
```

## Exception Handling
When the API returns a non-zero status code, (4xx or 5xx response), 
a subclass of AssemblyAIException will be thrown:

```csharp
try {
  aai.transcript.create(...);    
} catch (AssemblyAIException e) {
  System.Console.WriteLine(e.Message) 
  System.Console.WriteLine(e.StatusCode) 
}
```

## Contributing
While we value open-source contributions to this SDK, this library
is generated programmatically. Additions made directly to this library
would have to be moved over to our generation code, otherwise they would
be overwritten upon the next generated release. Feel free to open a PR as a
proof of concept, but know that we will not be able to merge it as-is.
We suggest opening an issue first to discuss with us!

On the other hand, contributions to the README are always very welcome!