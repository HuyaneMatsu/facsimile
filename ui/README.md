# Getting started

> This file containst detailed steps of what I did. Might change as I go along and write better software.

#### Create model

1. First create an model with **VRoid studio**. Install it from Stream it is free & works on Linux.
2. After you created it, **export it as VRM**.

#### Import the model

1. Install [**Unity Hub**](https://unity3d.com/get-unity/download).

2. Install an editor with the *Unity Hub*.
    
    > It downloads the editor to a temporary file on Linux. Can be problem if you did not allocate enough memory for
    > the operation system.

3. Open the `ui` directory as a project.

4. For importing `.vrm` files to unity you will need the [**UniVRM**](https://github.com/vrm-c/UniVRM) plugin.
   
    If the editor has a `VRM0` tab you are all good! If not:
        
        1. Go to the release the page and download the `.unitypackage` file.
        2. Drag the file into the editor. Will ask a fewt hings, just accept everything!

5. Import the model with `VRM0` -> `Import from VRM 0.x`.

# TODO
