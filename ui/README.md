# Getting started

> This file contains detailed steps of what I did. Might change as I go along and write better software.

### Create model

1. First create a model with **VRoid studio**. Install it from Steam as it is free & works on Linux.
   
2. After you created it, **export it as VRM**.

> If your model is not great, do not worry, it might take around 10 iterations to make it good!

### Import the model

1. Install [**Unity Hub**](https://unity3d.com/get-unity/download).

2. Install an editor with the *Unity Hub*.
    
    > It downloads the editor to a temporary file on Linux. Can be problem if you did not allocate enough memory for
    > the operating system.

3. Open the `ui` directory as a project.

4. For importing `.vrm` files to unity you will need the [**UniVRM**](https://github.com/vrm-c/UniVRM) plugin.
   
    If the editor has a `VRM0` tab you are all good! If not:
        
        1. Go to the release page and download the `.unitypackage` file.
        2. Drag the file into the editor 'Assets' tab. Will ask a few things, just accept everything!

5. Import the model with `VRM0` -> `Import from VRM 0.x`.

### Animate the model

1. Go to the directory of your model. Drag your model into the field.
   
    > If you do not find what exactly to drag. It is (probably) the last item in the directory you imported to the
    > model. You will find it easily, because it has a preview, meanwhile all the other files are just folders.
    >
    > Please reset coordinates of the model to all `0`.
    >
    > For now set the camera position to `0, 1.4, 0.8` as well and its `y` axis rotation to `180`.
    > If you can't find camera coordinates look at top left, there are the entities click on the camera one

2. Copy & paste the [**ModelControl.cs**](./Assets/ModelControl.cs) file to your project.
   
> If you do not know where, put it into the `Assets` directory.

3. Select your model, **Add Component**.

> Hide (Script)(s) section if it takes too much space because Add Component might be not visible unless you scroll down

5. Select the **ModelControl.cs** file.

6. Assign the model to the **Avatar** field.

### Post words

At this point you are basically done, if you have the tracker setup, just click on the play button in the Unity editor.

By learning more about Unity you can customize many things, like inserting something behind you, creating an
application from the project & many more!

## Configuration

In Unity editor Model controls provide you with various settings.

##### Camera

`camera_angle_vertical` (Defaults to `-15` degrees) refers to your camera's position. Negative numbers are under
your screen, meanwhile positive ones are on top of it.

It is the best to keep it as close to `0` as you can. Handles negative numbers better than positive.

##### Eyes

When blinking too much, because of "asian" eyes, try to reduce `open_eye_at` value (Defaults to `50`).
Occasionally you also might want to reduce `close_eye_at` (Defaults to `20`).
