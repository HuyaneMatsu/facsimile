# Getting Started

## Linux

To start up the server you will need python3.8 probably & clone the project from github.
You can just download it as zip & unpack, which is a pretty simple way of doing it.

When acquired, move with terminal to the `server` directory and run

```sh
python3 -m pip install -r requirements.txt
```

You can start the server from it's directory, like:
```sh
python3 ""
```
Or from one directory above, like:
```py
python3 server
```

## Android cam & Linux

Install [DroidCam](https://www.dev47apps.com/) on your android device.

DroidCam hosts a server on your device allowing your PC to connect to it.

Start up the server with `--to` parameter. Put the displayed address by DroidCam after it. Example:

```sh
python3 server --to http://0.0.0.0:4747/video
```

## Debugging

- `--draw` parameter will show back what the camera sees.

- `--no-connect` parameter will connect to your camera even if the GUI is inactive.
