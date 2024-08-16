# Transcribe audio from the microphone in real-time

The following things are required to run the sample:
* [.NET 8 or up](https://dotnet.microsoft.com/en-us/download)
* [Install SoX and add it to the PATH environment variable](https://sourceforge.net/projects/sox/)
* An [AssemblyAI](https://www.assemblyai.com/dashboard/signup) account with credit card set up
* Configure [your AssemblyAI API key](https://www.assemblyai.com/app/account) using .NET user-secrets:
  ```bash
  dotnet user-secrets set AssemblyAI:ApiKey [YOUR_API_KEY]
  ```
  
Now run the sample:

```bash
dotnet run
```

Speak into your microphone to see your speech transcribed in real-time.