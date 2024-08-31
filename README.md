# Microsoft Flight Simulator 2020 Controller Profiles Utility

This utility was born out of frustration that Asobo have given users no easy way to
export and compare what bindings have been set up in different controller profiles.

You can use it to select multiple controller profiles (e.g. Mouse, Flight Stick, 
Flight Throttle and Rudder Pedals), and display an HTML formatted document showing
each available control binding, and the controller & input bound to it.

So far, the utility has only been tested with a native Windows desktop install of
FS2020. Some work has also been done to support Steam-based installs, but it is
untested by actual Steam users.

**Example ways to use:**
* Select a single profile to see all its bindings.
* Select two different profiles for a controller to compare bindings.
* Select a set of controller profiles to get a list of all current bindings.

## To Install
To install the utility, perform the following steps:
* Download the installer from the latest project release (or from [Flightsim.to](https://flightsim.to/file/79474/fsprofiles)).
* Run the installer. You may have to click through a security warning.
* Accept the defaults and install.
* When you first run the program, you may get a message to install the .NET 8 runtime. If you get the
  warning, you *must* install the runtime for the utility to work.

The installer will currently set up a desktop shortcut and a menu folder and item.

## To Run
To run the program:
* Launch the program using either the menu item or desktop shurtcut.
* On first use, you may get a message to install the .NET 8 runtime. If you get the
  warning, you must install the runtime for the utility to work.
* Click either the `Native Windows` or `Steam` buttons at the top to choose which
type of install you have.
*  If you did a standard install, the program should detect the correct path to the
profile files. If it does so, it will automatically process the available profiles and
display them in the list.
* If the profiles are not automatically displayed, you will need to:
  * Either enter or paste the actual path, or use the `Select Profile Path` button
    to browse to it. This is different for `Native Windows` and `Steam` installs.
  * Once the path has been chosen, click the `Process Folders` button to detect any
    profiles. This will populate the list of detected profiles.
* Now tick the profiles you wish to include in the report.
* Click the `Generate Binding Report` button to generate the report.

### Program UI
See the screenshot below for an example of the program UI, showing a `Native Windows`
install was selected, all the detected controller profiles, with a set selected to
generate a report. Note the `All` list content is selected. This will output a report
showing all known bindings, rather than just those for which the selected controller
profiles have a binding.

If the `Include Uncategorised` checkbox is ticked, then an additional section will be
included on the report showing controller bindings which could not be matched to an 
entry in the control options list.

![Sample report](images/main-form.png)

### Command Line Arguments
The program does support two command line options, both of which are aimed at development use and not needed for normal operation.

| Short | Long Name | Description                                                                                                                                       |
|:-----:|:---------:|---------------------------------------------------------------------------------------------------------------------------------------------------|
| -d    | -debug    | Outputs the selected bindings data as an XML file                                                                                                 |
| -p    | -profiles | Space delimited list (e.g. `-p 0 2 5`) of profiles to select by number, starting at 0. Only useful if you are running the program multiple times. |


## Flight Sim Platform Support
| Platform Name  | Status        | Comment                                                                                                                |
|----------------|---------------|------------------------------------------------------------------------------------------------------------------------|
| Windows Native | Full Support  | Utility should automatically detect the controller profile path for a default install                                  |
| Windows Steam  | Supported     | Untested, but should work automatically, or you may need to manually choose the path to the parent folder of profiles. |
| XBox           | Not Supported |                                                                                                                        |

## Sample Output
![Sample report](images/sample-report.png)

## More Information
Please see the project [wiki](https://github.com/iadarroch/FSProfiles/wiki) for additional information.
