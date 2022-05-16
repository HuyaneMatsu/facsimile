
## Android

When you want to use this 0 budget project, you might don't even have a camera, but you got a nice android device!

##### Install F-roid

Goto [here](https://f-droid.org/en/packages/com.termux/) and download and install f-droid.

> You will need to enable installing apps from you browser.

##### Install Termux

Open f-roid and install Termux with it.

> You will need to enable installing apps from f-roid.
>
> The google play version of Termux is deprecated, so do not use that.
>
> Dunno how to use terminal?
>
> List directory: **ls** <br>
> Open directory: **cd *{name}*** <br>
> Go to directory: above: **cd ..** <br>
> Create directory: **mkdir *{name}***

##### Update your system

You will need to update your system instantly, *Yayyy*!

```sh
apt-get update && apt-get upgrade
```

If asks anything, `Y` and enter. *It will ask many things.*

##### Install CMake

```sh
apt-get install cmake
```
##### Install wget

```sh
apt-get install wget
```

##### Install Python

```sh
wget https://github.com/Termux-pod/termux-pod/blob/main/aarch64/python/python-3.8.6/python_3.8.6_aarch64.deb


dpkg -i python_3.8.6_aarch64.deb                                                                                        ! this step fails
```

##### Install git

Install git for cloning the repo.

```sh
apt-get install git
```

##### Update pip

Because when you install python it is not the newest version.

```sh
python3 -m pip install --upgrade pip setuptools wheel
```

##### Clone the repo

```sh
git clone https://github.com/HuyaneMatsu/facsimile
```

##### Install requirements

```sh
cd facsimile
cd server
python3 -m pip install -r requirements.txt                                                                              ! Cannot complie opencv on python3.10, so need 3.8
```
