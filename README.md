
# CIStatusAggregator

Checks continous integration endpoints to determine whether any project is being built or is broken.

The result is then saved to a file that can be used by [HueUpdater](https://github.com/jorgeyanesdiez/HueUpdater) or
[TrayLamp](https://github.com/jorgeyanesdiez/TrayLamp) when exposed with a web server.






## Build status

AppVeyor status:  [![AppVeyor status](https://ci.appveyor.com/api/projects/status/q5kb8c19wk27f1n8/branch/main?svg=true)](https://ci.appveyor.com/project/jorgeyanesdiez/CIStatusAggregator)

Sonarcloud status:  [![Sonarcloud status](https://sonarcloud.io/api/project_badges/measure?project=jorgeyanesdiez_CIStatusAggregator&metric=alert_status)](https://sonarcloud.io/dashboard?id=jorgeyanesdiez_CIStatusAggregator)






## Motivation

I use lamps at work to give my teams instant feedback about the status of multiple projects tracked by our CI systems.

This application provides the information consumed by the programs that update those lamps.
See also [HueUpdater](https://github.com/jorgeyanesdiez/HueUpdater) and [TrayLamp](https://github.com/jorgeyanesdiez/TrayLamp).

The current version supports Jenkins endpoints only. It connects to each one and aggregates the status values.






## Usage prerequisites

* Operational CI systems and related networking equipment.

* Basic JSON and Regex knowledge to edit the settings file.

* Write permission on a local folder to save the status files.

* A web server to expose the status files.
  Note: If you run this program on the same machine as a Jenkins controller node, you can use its */userContent*
  directory to serve the files without having to setup a dedicated web server.






## Deployment

Unpack the release file wherever you want on the target system. I suggest *C:\CIStatusAggregator*

Open the *appsettings.json* file with a plain text editor and carefully tweak the values to match your needs.

Here's an attempt to explain each one, although I hope most are self explanatory from the provided sample file.



* ***Endpoints***

  A list of endpoint definitions. Each entry defines a CI system to check and a file to write the status to.



* **Endpoint** -> **Meta** -> ***Description***

  Friendly name for the entry. Currently only used for identification in logs.



* **Endpoint** -> **Remote** -> ***BaseUrl***

  Base URL of the CI system to check.



* **Endpoint** -> **Remote** -> ***JobNameFilterRegex***

  Optional regular expression that can be used to filter the jobs that are aggregated.



* **Endpoint** -> **Remote** -> ***JobNameFilterMode***

  Only applicable when *JobNameFilterRegex* is defined. May be "Blacklist" or "Whitelist".

  When "Blacklist", all jobs that match *JobNameFilterRegex* are excluded from the result.

  When "Whitelist", only jobs that match *JobNameFilterRegex* are included in the result.



* **Endpoint** -> **Local** -> ***StatusFilePath***

  Full or relative path used to write out the status for the endpoint.
  
  Write permission will be required to write the file, and it's usually desirable to locate the file in a folder that is shared with a web server.






Finally, use your preferred scheduling method to run this application frequently.
I usually run it every minute with the Windows Task Scheduler or with a systemd timer.






## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
