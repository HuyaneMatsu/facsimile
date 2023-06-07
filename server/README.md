# Getting Started

## Linux

To start up the server you will need Python3.8 & cloned project from Github.
You can just download the repo as zip & unpack.

When acquired, move with terminal to the `server` directory and run:

```sh
python3 -m pip install -r requirements.txt
```

You can start the server from its directory, like:
```sh
python3 ""
```
Or from one directory above, like:
```py
python3 server
```

## Android cam & Linux

DroidCam hosts a server on your device allowing your PC to connect to it.

Install [DroidCam](https://www.dev47apps.com/) on your android device.

- Note: You don't need the PC client of DroidCam, just the mobile server is needed.

Start up the server with `--to` parameter. Put the displayed address by DroidCam after it.

`--to` is for the "IP CAM address" with "/video" suffix. Example:

```sh
python3 server --read-from http://0.0.0.0:4747/video
```

- Note: If you run it as above without having Unity set&open then nothing much will happen.
- If you just want to see some output and if the DroidCam is working, without having Unity set, then see  [Debugging][#debugging] section

## Debugging

- `--draw` parameter will show back what the camera sees.

- `--no-connect` parameter will connect to your camera even if the GUI (Unity) is inactive.
