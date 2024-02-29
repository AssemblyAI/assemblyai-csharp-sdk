# Transcribe Twilio Voice call in real-time using AssemblyAI C# SDK

## Steps to run

1. Configure your AssemblyAI API key:
    ```bash
    dotnet user-secrets set AssemblyAI:ApiKey "<YOUR_API_KEY>"
    ```

2. Create a tunnel using [ngrok](https://ngrok.com/) or alternative tunnel service:
    ```bash
     ngrok http 5179
    ```
3. Configure the ngrok Forwarding URL + _/voice_ path as the webhook URL for incoming phone calls on your Twilio phone number.

4. Run the .NET project:
    ```bash
    dotnet run 
    ```