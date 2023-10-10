# ReolinkRestart

A tool that restarts the Reolink client, in full screen mode, on a schedule to reset out-of-sync camera streams.

## Installation

1. Download the latest executable from the [releases](https://github.com/codemunkie15/ReolinkRestart/releases).
2. Extract the executable.
3. Run the executable.
4. Optionally, add the executable to your Startup folder.

## Options

Options can be changed by modifying the generated `settings.json` file and restarting the application.

* **ReolinkExecutableFilePath** - The path where your Reolink client executable is stored.
* **RestartIntervalMinutes** - The restart interval in minutes.
* **UseFullscreen** - Whether to automatically make the client fullscreen on startup.
* **FullscreenDelaySeconds** - How long to wait for the client to start before clicking the fullscreen button, in seconds. Set this to a higher value if your client takes a while to load up.
* **FullscreenXOffset** - This value is subtracted from the right of the client window location to find the fullscreen button.
* **FullscreenYOffset** - This value is subtracted from the bottom of the client window location to find the fullscreen button.
