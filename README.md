# Project aim
This project aims to create a .Net client library capable of communicating with a [FOP2](http://fop2.com) server. It provides an event-based library to send/receive messages to/from the FOP2 server. A simple application demonstrating some of the capabilities is also provided.

The client library is the main focus; this library needs to abstract all the (complicated) protocol-specific details away and keep required FOP2-specific implementation details to a minimum. This project is open sourced in the hopes that the OSS community is willing to improve this library and build great .Net (capable) applications based on this library. The library itself requires the .Net framework v3.5 at a minimum and the aim is to keep external dependencies to a minimum (currently only the (awesome) [Json.Net](https://www.nuget.org/packages/Newtonsoft.Json/) framework). The Fop2Client library is also available as [Nuget package](http://nuget.org/packages/fop2clientlib).

Ultimately we want to end up with a fully-featured Desktop Dialer (hence the Fop2*DD* name) based on a complete implementation of the Fop2Client library implementing the entire FOP2 protocol.

## Documentation
The library is well-documented and Helpfile builder-compatible. A [Sandcastle Help File Builder](http://shfb.codeplex.com) project is included. Class-level documentation can be generated with this project. Other documentation (for example: how to use the library) will be made available in the [Documentation] section. Also, the provided sample application will demonstrate _a possible_ usage of the library.

## Current status
Although the basis for the library has been created and implemented, there is still a lot to be done and to be implemented. The architectural basis of the project will remain, mostly, the same but some deprecating, moving around etc. is still an option. The current status of the library is "pretty stable" but it is recommended you do not use this library (yet) for production purposes until it hits v1.0. Work is still in progress.

## Help welcome
If you're interested in improving this project please contact us. Help is very much welcome and appreciated. Also, please feel free to report issues and join the discussions! More on how to join: [How to join FOP2DD project]
