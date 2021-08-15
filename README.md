# Simple Timer
Just a timer which logs results in a persistent way.

## Prerequisites

If you're using one of the executables included in a release, you will need the [.NET 6.0 runtime](https://dotnet.microsoft.com/download/dotnet/6.0) installed. 

However, it may just be easier to clone the repo and use the `dotnet run` command instead which would require you to have the [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) installed. I would highly recommend going this route on macOS as running unsigned code as part of an executable is a nightmare currently.

## Usage

I will assume you have cloned the repo. 

`cd` into the root folder and use `dotnet run` which will start the timer with a default file name and default interval (1 minute).
  
To customise the file name and interval time (in ms), add environment variables e.g.

```
dotnet run custom-file-name 10000
```

In this case, the current time will be logged every 10 seconds to a file called `custom-file-name.txt`.
